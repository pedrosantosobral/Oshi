using UnityEngine;

public class JumpEnemy : MonoBehaviour
{
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private Animator animator;
    private Transform target;
    private Rigidbody2D myRigidbody;

    private Vector3 finalDirection;
    private float jumpTime = 1f;
    [SerializeField]
    private float timeBetweenJumps;
    [SerializeField]
    private bool isGrounded;

    //increased speed timers and bool
    private bool _collidedWithPlayer;

    //speed variables
    [SerializeField]
    private float speed;

    //ground and wall detection references
    [SerializeField]
    private LayerMask groundLayerMask;
    [SerializeField]
    private LayerMask targetLayerMask;
    [SerializeField]
    private Vector2 ofsetFloorCheck;
    [SerializeField]
    private float floorCheckSize;
    [SerializeField]
    private Vector2 ofsetTargetCheck;
    [SerializeField]
    private int jumpTimer = 5;
    
    private float timer = 0;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isGrounded && timer >= jumpTimer && other.tag != null)
        {
            if (other.CompareTag("FlyEnemyInside"))
            {
                timer = 0f;
                target = other.transform;

                Invoke("JumpToTarget", 0.000001f);
                Invoke("Wait", timeBetweenJumps);

            }
            else if (other.CompareTag("Player"))
            {
                target = other.transform;

                Invoke("JumpToTarget", 0.000001f);
                Invoke("Wait", timeBetweenJumps);
            }

            else if (other.CompareTag("light") && other.gameObject.GetComponent<CheckCollisionsForLight>()!= null )
            {
                target = other.transform;
                
                if(target != null)
                {
                    Invoke("JumpToTarget", 0.000001f);
                    Invoke("Wait", timeBetweenJumps);
                }
                
            }
        }
    }

    private void JumpToTarget()
    {
        float deltaY = 0;
        float deltaX = 0;

        animator.SetTrigger("Atack");
        animator.SetTrigger("Stop");
        
        
       
        if(target != null)
        {
            deltaY = target.position.y - transform.position.y;
            deltaX = target.position.x - transform.position.x;
        }
        
        float throwAngle = Mathf.Atan((deltaY + 4.803f * (jumpTime * jumpTime)) / deltaX);
        float totalVelocity = deltaX / Mathf.Cos(throwAngle);
        finalDirection = new Vector2(totalVelocity * Mathf.Cos(throwAngle), totalVelocity * Mathf.Sin(throwAngle)) * speed;
        myRigidbody.velocity = finalDirection;

        if(myRigidbody.velocity != null)
        {
            AudioManager.Instance.PlaySFX(jumpSound);
        }
    }
    private void Wait()
    {
       
    }

    private void FixedUpdate()
    {


        timer += Time.deltaTime;

        IsGrounded();


        //if enemy collided with the player, call hit
        if (_collidedWithPlayer == true)
        {

        }

    }
    private void IsGrounded()
    {
        Vector2 position = new Vector2(transform.position.x + ofsetFloorCheck.x, transform.position.y + ofsetFloorCheck.y);
        Vector2 position2 = new Vector2(transform.position.x - ofsetFloorCheck.x, transform.position.y + ofsetFloorCheck.y);
        isGrounded = (Physics2D.OverlapCircle(position, floorCheckSize, groundLayerMask) != null ||
            Physics2D.OverlapCircle(position2, floorCheckSize, groundLayerMask) != null);

    }


    private void OnDrawGizmosSelected()
    {
        Vector2 position = new Vector2(transform.position.x + ofsetFloorCheck.x, transform.position.y + ofsetFloorCheck.y);
        Vector2 position2 = new Vector2(transform.position.x - ofsetFloorCheck.x, transform.position.y + ofsetFloorCheck.y);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(position, floorCheckSize);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(position2, floorCheckSize);
    }


    public void Atack()
    {

    }

}