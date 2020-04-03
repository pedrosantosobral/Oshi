using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputManager _inputManagerReference;

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
    private bool        _isGrounded;

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
    }

    private void FixedUpdate()
    {

        FindInputManager();

        if ((_inputManagerReference.doubleTap == true || _inputManagerReference.swipeUp == true) && (_inputManagerReference.touchingRight == true || _inputManagerReference.touchingLeft == true)) 
        {
            verticalmove = 1;
        }
        else
        {
            verticalmove = 0;
            ParticlesPlay();
        }

        //TODO STOP PLAYER WHEN TOUCH ENDS AND ADD JUMP SWIPE
        if (_inputManagerReference.touchingLeft == true)
        {
            _horizontalmove = -1 * runspeed;
           
        }
        else if (_inputManagerReference.touchingRight == true)
        {
            _horizontalmove = 1 * runspeed;
            
        }
        else
        {
            _horizontalmove = 0;
            ParticlesPlay();

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
        }
        
    }

    private void FindInputManager()
    {
        if (_inputManagerReference == null)
        {
            _inputManagerReference = GameObject.FindGameObjectWithTag("InputManager").GetComponent<InputManager>();
        }

    }

    public void ParticlesPlay()
    {
        playerDust.Play();
    }


}