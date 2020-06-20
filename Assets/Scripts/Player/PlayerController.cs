using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float timeBetweenFootsteps;
    [SerializeField] private AudioClip[] runSounds;
    [SerializeField] private AudioClip[] jumpSounds;
    [SerializeField] private Animator animator;
    private bool animRunning;

    private bool playingfootSteps = false;

    private InputManager _inputManagerReference;

    private float timeToLongIdleAnim = 7f;
    private float actualTimeToLongAnim;

    public GameObject spriteToFlip;

    public ParticleSystem playerDust;

    //speed variables
    public float        runspeed;
    public float        jumpforce;

    //for ground check
    public Transform    groundCheck;
    //radius to check for ground from player feet
    public float        checkRadius;
    //layer mask instance to get what is ground from the game
    public LayerMask    whatIsGround;

    //player rigidbody instance
    private Rigidbody2D rb;
    //move input variable
    public float        _horizontalmove, verticalmove;
    //ground bool
    [SerializeField] private bool        _isGrounded;
    private bool        lookingRight;

    //Variables for groundCheckTimer, even if the player is not grounded
    //he is alowed to jump for a little time
    private float       _groundedRemember = 0;
    [SerializeField]
    private float       _groundedRememberTime = 0.25f;


    private void Start()
    {
        //get object to check for ground in radius
        groundCheck = GetComponentInChildren<Transform>();
        //get player rigidbody
        rb = GetComponent<Rigidbody2D>();
        actualTimeToLongAnim = timeToLongIdleAnim;
    }
    private void FixedUpdate()
    {
       
        animator.SetBool ("isGrounded", _isGrounded);
        animator.SetFloat("isJumping", verticalmove);
        animator.SetBool ("isRunning", animRunning);
        
        FindInputManager();
        TurnPlayerSprite();

        if((_inputManagerReference.touchingLeft == true || _inputManagerReference.touchingRight == true) && (_isGrounded == true))
        {
            animRunning = true;
            playerDust.Play();
            if(!playingfootSteps)
            {
                StartCoroutine("PlayFootstepsSound");
            }

        }

        if ((_inputManagerReference.doubleTap == true || _inputManagerReference.swipeUp == true) && (_inputManagerReference.touchingRight == true || _inputManagerReference.touchingLeft == true)) 
        {
            verticalmove = 1;
            actualTimeToLongAnim = timeToLongIdleAnim;
            playerDust.Play();

        }
        else
        {
            verticalmove = 0;
            
            actualTimeToLongAnim -= Time.deltaTime;
            if(actualTimeToLongAnim <= 0)
            {
                animator.SetTrigger("longIdle");
                actualTimeToLongAnim = timeToLongIdleAnim;
            }
        }

        //TODO STOP PLAYER WHEN TOUCH ENDS AND ADD JUMP SWIPE
        if (_inputManagerReference.touchingLeft == true)
        {
            _horizontalmove = -1 * runspeed;
            lookingRight = false;
            //animRunning = true;

        }
        else if (_inputManagerReference.touchingRight == true)
        {
            _horizontalmove = 1 * runspeed;
            lookingRight = true;
            //animRunning = true;
           

        }
        else
        {
            _horizontalmove = 0;
            animRunning = false;
        }

        //define groundcheck radius from the player feet
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        //_horizontalmove = CrossPlatformInputManager.GetAxis("Horizontal") * runspeed;

        //add velocity to the player rigidbody, move player
        rb.velocity = new Vector2(_horizontalmove, rb.velocity.y);

        //get jump vertical input from joysick
       // float verticalmove = CrossPlatformInputManager.GetAxis("Vertical") * runspeed;

        //timer to groundcheck
        _groundedRemember -= Time.deltaTime;
        //check if player is touching the ground
        if (_isGrounded == true)
        {
            //set fixed timer value to be decreased
            _groundedRemember = _groundedRememberTime;
           
        }
        
        //if the player jumped in the time gap make him jump
        if ((verticalmove >= .5f) && (_groundedRemember > 0))
        {
            //restart groundcheck timer
            _groundedRemember = 0;
            //add velocity to player rigidbody, make player jump
            rb.velocity = Vector2.up * jumpforce;
            AudioManager.Instance.PlaySFX(jumpSounds[Random.Range(0, jumpSounds.Length)]);
        }
        
    }

    private void FindInputManager()
    {
        if (_inputManagerReference == null)
        {
            _inputManagerReference = GameObject.FindGameObjectWithTag("InputManager").GetComponent<InputManager>();
        }

    }

    private void TurnPlayerSprite()
    {

        if(lookingRight == true)
        {
             spriteToFlip.transform.rotation = Quaternion.Euler(0, 0, 0);
            // playerDust.Play();
        }
        else
        {
             spriteToFlip.transform.rotation = Quaternion.Euler(0, 180, 0);
             //playerDust.Play();
        }


    }

    IEnumerator PlayFootstepsSound()
    {
        playingfootSteps = true;
        AudioManager.Instance.PlaySFX(runSounds[Random.Range(0, runSounds.Length)],0.8f);
        yield return new WaitForSeconds(timeBetweenFootsteps);
        playingfootSteps = false;
    }


    
        
    



}