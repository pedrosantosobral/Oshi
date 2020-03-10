using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    //public float hitDistance = 3;

    public LayerMask whatIsLight;
    public LayerMask maskToCollide;
    public bool _isActive;
    public GameObject activactedTrap;
    public GameObject shotFeedBack;

    private bool _isWasted = false;
    public Collider2D triggerToBeActivated;
    public Collider2D triggerToBeActivated2;
    private float _pickLightRadius = 1f;

    private bool doOnce = false;

    private LevelGenerator     lvlGeneratorReference;
    private PlayerInteractions playerReference;

    private RaycastHit2D _hitInfo;
    public float hitDistance = 3.5f;

    private void Start()
    {
        
        shotFeedBack.SetActive(false);
        activactedTrap.SetActive(false);
        lvlGeneratorReference = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();
        triggerToBeActivated.enabled = false;
       // triggerToBeActivated2.enabled = false;
    }

    private void FixedUpdate()
    {

        if (lvlGeneratorReference.readyToPlayer == true && doOnce == false)
        {
            Invoke("GetPlayerCollider", 0.1f);
            doOnce = true;
        }


        _isActive = Physics2D.OverlapCircle(gameObject.transform.position, _pickLightRadius, whatIsLight);

        if (_isActive == true && _isWasted == false)
        {
            //REMOVE COMENTSS TO GO BACK
            //triggerToBeActivated.enabled = true;
            // triggerToBeActivated2.enabled = true;
            activactedTrap.SetActive(true);


            //to ignore triggers i went to project setings, and disabled querry trigger detections
            _hitInfo = Physics2D.Raycast(transform.position, transform.right,hitDistance,maskToCollide);

            Vector2 forward = transform.TransformDirection(transform.right) * 3.5f;

            if (_hitInfo.collider != null)
            {
                
                    if (_hitInfo.transform.tag == "Player")
                    {
                        print("kek3");
                        //SEND EVENT NOTIFICATION TO PLAYER SAYING HE WAS HIT
                        shotFeedBack.SetActive(true);
                        _isWasted = true;
                        Invoke("DelayedAnim", 1f);
                        
                    }

                    if (_hitInfo.collider.gameObject.tag == "FlyEnemy")
                    {
                        shotFeedBack.SetActive(true);
                        _isWasted = true;
                        Invoke("DelayedAnim", 1f);
                        Destroy(_hitInfo.collider.gameObject);
                    }
                

                if (_hitInfo.collider.tag == "PatrolEnemy")
                {
                    shotFeedBack.SetActive(true);
                    _isWasted = true;
                    Invoke("DelayedAnim", 1f);
                    Destroy(_hitInfo.collider.gameObject);
                }

            }
      

        }
        else 
        {   //REMOVE COMENT TO FIX
            //triggerToBeActivated.enabled = false;
            // triggerToBeActivated2.enabled = false;
            activactedTrap.SetActive(false);
        }

    }

    //change to raycast
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("PatrolEnemy"))
        {
            Debug.Log("sdfsed");
            shotFeedBack.SetActive(true);
            _isWasted = true;
            Invoke("DelayedAnim", 1f);
            Destroy(other.gameObject);
        }
        

        if (!other.isTrigger) 
        {

            if (other.CompareTag("FlyEnemy"))
            {
                shotFeedBack.SetActive(true);
                _isWasted = true;
                Invoke("DelayedAnim", 1f);
                Destroy(other.gameObject);
            }

          
            if (other.CompareTag("Player"))
            {
                shotFeedBack.SetActive(true);
                _isWasted = true;
                Invoke("DelayedAnim", 1f);
            }
        }

        /*
        if(other == playerReference.lightCollider)
        {
            _isActive = true;
        }
        */
    }
    /*
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == playerReference.lightCollider)
        {
            _isActive = false;
        }
    }
    */
    private void GetPlayerCollider()
    {
        playerReference = GameObject.Find("Player(Clone)").GetComponent<PlayerInteractions>();
    }

    private void DelayedAnim()
    {
        shotFeedBack.SetActive(false);
    }


}
