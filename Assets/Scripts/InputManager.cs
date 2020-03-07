using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    
    #region Instance

    private static InputManager _instance;
    public static InputManager Instance
    {
        get
        {
            _instance = FindObjectOfType<InputManager>();

            if (_instance == null)
            {
                _instance = new GameObject("InputManager", typeof(InputManager)).GetComponent<InputManager>();
            }

            return _instance;
        }
        set
        {
            _instance = value;
        }
    }

    #endregion

    [Header("Tweaks")]

    [SerializeField] private float _deadZone = 100.0f;
    [SerializeField] private float _doubleTapDelta = 0.5f;
    private static int sideAreasSize = 5;

    [Header("Logic")]

    public bool tap, doubleTap, swipeLeft, swipeRight, swipeUp, swipeDown,touchingRight,touchingLeft,allowLightPaint,jump;
    private Vector2 swipeDelta, startTouch;
    private float lastTap;
    private float sqrDeadzone;

    #region Public Properties

    public bool Tap { get { return tap; } }
    public bool DoubleTap { get { return doubleTap; } }
    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeLeft { get { return swipeLeft; } }

    //vars to check touch position
    private Vector2 touchPos = Vector2.zero;
    private Vector2 touchPos2 = Vector2.zero;

    private Rect _leftSide = new Rect(0, 0, Screen.width / sideAreasSize, Screen.height);
    private Rect _rightSide = new Rect(Screen.width - Screen.width / sideAreasSize, 0, Screen.width / sideAreasSize, Screen.height);
    private Rect _centerArea = new Rect(Screen.width / (sideAreasSize * 2), 0, Screen.width - Screen.width / sideAreasSize, Screen.height);

    #endregion

    private void Start()
    {
        sqrDeadzone = _deadZone * _deadZone;
    }

    private void Update()
    {
        if(doubleTap == true)
        {
            Debug.Log("doubletap");
        }
        tap = false;
        doubleTap = false;
        //ORIGINAL CODE


        swipeRight = false;
        swipeLeft = false;
        swipeDown = false;
        swipeUp = false;

        CheckTouchArea();

        /*//Reset bools
        if (Input.touches.Length != 0)
        {
            if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                tap = false;
                doubleTap = false;
                swipeRight = false;
                swipeLeft = false;
                swipeDown = false;
                swipeUp = false;
            }

        }
        */
       


        UpdateMobile();

#if UNITY_EDITOR

#else
        
#endif

    }

    private void UpdateStandalone()
    {
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            startTouch = Input.mousePosition;
            doubleTap = Time.time - lastTap < _doubleTapDelta;
            lastTap = Time.time;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            startTouch = swipeDelta = Vector2.zero;
        }

        //Reset distance, gett the new swipe delta
        swipeDelta = Vector2.zero;

        if (startTouch != Vector2.zero && Input.GetMouseButton(0))
        {
            swipeDelta = (Vector2)Input.mousePosition - startTouch;
        }

        //check if delta is bigger than deadzone, validate the swipe as big enough
        if (swipeDelta.sqrMagnitude > sqrDeadzone)
        {
            //if yes confirm swipe and get the direction

            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                //left or right
                if (x < 0)
                {
                    swipeLeft = true;
                }
                else
                {
                    swipeRight = true;
                }
            }
            else
            {
                //left or right
                if (y < 0)
                {
                    swipeDown = true;
                }
                else
                {
                    swipeUp = true;
                }
            }

            //reset values to avoid more than 1 swipe
            startTouch = swipeDelta = Vector2.zero;
        }

    }

    private void UpdateMobile()
    {
        if (Input.touches.Length != 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                startTouch = Input.mousePosition;
                doubleTap = Time.time - lastTap < _doubleTapDelta;
                lastTap = Time.time;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                startTouch = swipeDelta = Vector2.zero;
            }

            if(Input.touches.Length > 1)
            {
                if (Input.touches[1].phase == TouchPhase.Began)
                {
                    tap = true;
                    startTouch = Input.mousePosition;
                    doubleTap = Time.time - lastTap < _doubleTapDelta;
                    lastTap = Time.time;
                }
                else if (Input.touches[1].phase == TouchPhase.Ended || Input.touches[1].phase == TouchPhase.Canceled)
                {
                    startTouch = swipeDelta = Vector2.zero;
                }
            }
        }

        //Reset distance, gett the new swipe delta
        swipeDelta = Vector2.zero;

        if (startTouch != Vector2.zero && Input.touches.Length != 0)
        {
            swipeDelta = Input.touches[0].position - startTouch;
        }

        //check if delta is bigger than deadzone, validate the swipe as big enough
        if (swipeDelta.sqrMagnitude > sqrDeadzone)
        {
            //if yes confirm swipe and get the direction

            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                //left or right
                if (x < 0)
                {
                    swipeLeft = true;
                    Debug.Log("swipeLeft");
                }
                else
                {
                    swipeRight = true;
                    Debug.Log("swipeRight");
                }
            }
            else
            {
                //left or right
                if (y < 0)
                {
                    swipeDown = true;
                    Debug.Log("swipeDown");
                }
                else
                {
                    swipeUp = true;
                    Debug.Log("swipeUP");
                }
            }

            //reset values to avoid more than 1 swipe
            startTouch = swipeDelta = Vector2.zero;
        }
    }

    private void CheckTouchArea()
    {

        if (Input.touchCount > 0)
        {
            touchPos = Input.GetTouch(0).position;
            
            if(Input.touchCount > 1)
            {
                touchPos2 = Input.GetTouch(1).position;
            }
            
        }
        else
        {
            touchPos = new Vector2(99999999,999999999);
            touchPos2 = new Vector2(99999999, 999999999);
        }

        if (_leftSide.Contains(touchPos))
        {
            touchingLeft = true;
        }
        else
        {
            touchingLeft = false;
        }


        if (_rightSide.Contains(touchPos))
        {
            touchingRight = true;
        }
        else
        {
            touchingRight = false;
        }



        if(_centerArea.Contains(touchPos))
        {
            allowLightPaint = true;
            _leftSide = new Rect(0, 0, 0, 0);
            _rightSide = new Rect(0, 0, 0, 0);
            _centerArea = new Rect(0, 0, Screen.width, Screen.height);
        }
        else
        {
            allowLightPaint = false;
             _leftSide = new Rect(0, 0, Screen.width / sideAreasSize, Screen.height);
            _rightSide = new Rect(Screen.width - Screen.width / sideAreasSize, 0, Screen.width / sideAreasSize, Screen.height);
            _centerArea = new Rect(Screen.width / (sideAreasSize * 2), 0, Screen.width - Screen.width / sideAreasSize, Screen.height);

}





        if((touchingRight == true && _leftSide.Contains(touchPos2)) || (touchingLeft == true && _rightSide.Contains(touchPos2)))
        {
            jump = true;
        }
        else
        {
            jump = false;
        }













    }
}
