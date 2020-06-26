using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventSystem;
using UnityEngine.Experimental.Rendering.LWRP;

public class Trap : MonoBehaviour
{
    [SerializeField] private AudioClip shotSound;
    [SerializeField] private GameObject lightToDisable;
    [SerializeField] private GameObject JUMP_DFB;
    [SerializeField] private GameObject FLY_DFB;
    [SerializeField] private GameObject PATROL_DFB;
    [SerializeField] private GameObject PLAYER_DFB;

    [SerializeField] private VoidEvent onPlayerDamage;

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
    private RaycastHit2D _hitInfo;
    public float hitDistance = 3.5f;

    private void Start()
    {
        shotFeedBack.SetActive(false);
        activactedTrap.SetActive(false);
       // triggerToBeActivated.enabled = false;
       // triggerToBeActivated2.enabled = false;
    }

    private void FixedUpdate()
    {
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
                        
                        //SEND EVENT NOTIFICATION TO PLAYER SAYING HE WAS HIT
                        onPlayerDamage.Raise();
                        shotFeedBack.SetActive(true);
                        AudioManager.Instance.PlaySFX(shotSound,0.3f);
                        _isWasted = true;
                        lightToDisable.GetComponent<Light2D>().enabled = false;
                        Invoke("DelayedAnim", 1f);
                        
                    }

                    //NEED FIX HERE
                    if (_hitInfo.collider.gameObject.tag == "FlyEnemyInside")
                    {
                        AudioManager.Instance.PlaySFX(shotSound,0.3f);
                        shotFeedBack.SetActive(true);

                        _isWasted = true;
                        lightToDisable.GetComponent<Light2D>().enabled = false;
                        Invoke("DelayedAnim", 1f);
                        Instantiate(FLY_DFB, new Vector3(_hitInfo.collider.transform.position.x, _hitInfo.collider.transform.position.y, _hitInfo.collider.transform.position.z), Quaternion.identity);
                        Destroy(_hitInfo.collider.transform.parent.gameObject);
         
                    }

                if (_hitInfo.transform.tag == "JumpEnemy")
                {
                    print("kek3");
                    AudioManager.Instance.PlaySFX(shotSound, 0.3f);
                    shotFeedBack.SetActive(true);

                    _isWasted = true;
                    lightToDisable.GetComponent<Light2D>().enabled = false;
                    Invoke("DelayedAnim", 1f);
                    Instantiate(JUMP_DFB, new Vector3(_hitInfo.collider.transform.position.x, _hitInfo.collider.transform.position.y, _hitInfo.collider.transform.position.z), Quaternion.identity);
                    Destroy(_hitInfo.collider.transform.parent.gameObject);

                }


                if (_hitInfo.collider.tag == "PatrolEnemy")
                {
                    AudioManager.Instance.PlaySFX(shotSound,0.3f);
                    shotFeedBack.SetActive(true);
                    _isWasted = true;
                    lightToDisable.GetComponent<Light2D>().enabled = false;
                    Invoke("DelayedAnim", 1f);
                    Instantiate(PATROL_DFB, new Vector3(_hitInfo.collider.transform.position.x, _hitInfo.collider.transform.position.y, _hitInfo.collider.transform.position.z), Quaternion.identity);
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

    /*/change to raycast
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

        
        if(other == playerReference.lightCollider)
        {
            _isActive = true;
        }
        */
    
    /*
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == playerReference.lightCollider)
        {
            _isActive = false;
        }
    }
    */
    private void DelayedAnim()
    {
        shotFeedBack.SetActive(false);
    }


}
