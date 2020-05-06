using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.Rendering.LWRP;

public class PaintLight : MonoBehaviour
{
    private InputManager _inputManagerReference;

    //size of player light size variables;
    public float maxPlayerLight;         //recomended as default 1f
    public float minPlayerLight;         //recomended as default 0.4f

    public float lightFactor;

    public float _actualMaxPointLightOuterRadius;

    // LOR = Light Outer Radius
    internal float maxLOR = 2.6f;
    internal float damagedMaxLOR = 1f;
    internal float minLOR = 0.6f;

   public float _actualMaxLight;

    public float damagedMaxLight;        //recomended as default 0.7f
 
    public float normalRechargeRate;     //recomended as default 0.12f
    public float slowerRechargeRate;     //recomended as default 0.020f

    public float ammountOfLightToPaint;  //recomended as default 0.020f THE BIGGER THE VALUE THE MOST LIGHT WILL BE AVALIABLE

    //check if player is damaged
    public bool playerIsDamaged;

    //just to do once
    private bool _hitFeedback;

    private GameObject _playerMaskReference;
    private GameObject _savedLight;

    private float _nextActionTime;

    public float paintFadeTimePeriod;
    public float noPaintLightTime;

    public GameObject lightToSpawn;
    public Queue<GameObject> lightQueue = new Queue<GameObject>();
    public List<GameObject> listToEnemies = new List<GameObject>();

    private void Start()
    {
        _actualMaxLight = maxPlayerLight;
        playerIsDamaged = false;
        _hitFeedback = false;
    }
    void FixedUpdate()
    {
        FindInputManager();
        FindPlayer();

        if (_playerMaskReference != null)
        {
            if (playerIsDamaged == true)
            {
                //change max player light to damaged light state
                _actualMaxLight = damagedMaxLight;
                _actualMaxPointLightOuterRadius = damagedMaxLOR;

                //hit feedback(do once in each loop) 
                if (_hitFeedback == false)
                {
                    _playerMaskReference.transform.localScale = new Vector3(0.2f, 0.2f, 0);
                    _playerMaskReference.GetComponent<Light2D>().pointLightOuterRadius = _actualMaxLight;
                    _hitFeedback = true;
                }

            }
            else if (playerIsDamaged == false)
            {
                _actualMaxPointLightOuterRadius = maxLOR;
                _actualMaxLight = maxPlayerLight;
                _hitFeedback = false;
            }

            if (_inputManagerReference.allowLightPaint == true)
            {
                //if player mask is bigger then the minimum light size
                if (_playerMaskReference.transform.localScale.x > minPlayerLight && _playerMaskReference.transform.localScale.y > minPlayerLight)
                {

                    if (Input.touchCount > 0)
                    {

                        Touch touch = Input.GetTouch(0);
                        Vector3 touchPosition = new Vector3(touch.position.x, touch.position.y, Camera.main.nearClipPlane);
                        touchPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                        touchPosition.z = 0f;

                        if (!EventSystem.current.currentSelectedGameObject)
                        {
                            //Add new spawned light to the queue and instantiate it
                            lightQueue.Enqueue(_savedLight = Instantiate(lightToSpawn, touchPosition, Quaternion.identity));
                            //wait for light to check his colisions and then add it after check
                            Invoke("Wait", 0.001f);

                            //decrease player mask size
                            _playerMaskReference.transform.localScale -= new Vector3(ammountOfLightToPaint, ammountOfLightToPaint, 0);
                            _playerMaskReference.GetComponent<Light2D>().pointLightOuterRadius -= ammountOfLightToPaint * lightFactor;
                            _playerMaskReference.GetComponent<Light2D>().pointLightOuterRadius =
                                _playerMaskReference.GetComponent<Light2D>().pointLightOuterRadius < 0.6f ?
                                0.6f : _playerMaskReference.GetComponent<Light2D>().pointLightOuterRadius;
                        }
                    }
                }
            }

            //if its painted light left
            if (lightQueue.Count > 0)
            {
                if (Time.time > _nextActionTime)
                {
                    //destroy the last light
                    _nextActionTime = Time.time + paintFadeTimePeriod;

                    //TEST REMOVE FROM ENEMY LIST DESTROYED LIGHTS
                    if (listToEnemies.Contains(lightQueue.Peek()))
                    {
                        listToEnemies.Remove(lightQueue.Peek());
                    }
                    Destroy(lightQueue.Dequeue());
                }
            }

            //if timer is 0 and size player light is decreased, increase player light
            if (_playerMaskReference.transform.localScale.x < _actualMaxLight && _playerMaskReference.transform.localScale.y < _actualMaxLight)
            {
                //only increase player light normaly if there is no more painted light
                if (lightQueue.Count <= 0)
                {
                    //increase player light
                    _playerMaskReference.transform.localScale += new Vector3(normalRechargeRate, normalRechargeRate, 0) * Time.deltaTime;
                    _playerMaskReference.GetComponent<Light2D>().pointLightOuterRadius += normalRechargeRate * lightFactor * Time.deltaTime;
                }
                //if is there still light increase player light slowly
                else if (lightQueue.Count > 0)
                {
                    _playerMaskReference.transform.localScale += new Vector3(slowerRechargeRate, slowerRechargeRate, 0) * Time.deltaTime;
                    _playerMaskReference.GetComponent<Light2D>().pointLightOuterRadius += slowerRechargeRate * lightFactor * Time.deltaTime;
                }
                _playerMaskReference.GetComponent<Light2D>().pointLightOuterRadius =
                                _playerMaskReference.GetComponent<Light2D>().pointLightOuterRadius > 2.6f ?
                                2.6f : _playerMaskReference.GetComponent<Light2D>().pointLightOuterRadius;

            }
        }
    }

    private void FindPlayer()
    {
        if (_playerMaskReference == null)
        {
            _playerMaskReference = GameObject.Find("PlayerLightMask");
        }

    }

    private void FindInputManager()
    {
        if (_inputManagerReference == null)
        {
            _inputManagerReference = GameObject.Find("InputManager").GetComponent<InputManager>();
        }

    }

    private void Wait()
    {

        //TEST MAKE HERE THE LIST OF LIGHT TO THE ENEMIES
        if (_savedLight != null)
        {
            if (_savedLight.GetComponent<CheckCollisionsForLight>().notInsideWall == true)
            {
                if (!listToEnemies.Contains(_savedLight))
                {
                    listToEnemies.Add(_savedLight);
                }

            }
        }

    }


}
