using UnityEngine;

public class JumpEnemy : MonoBehaviour
{

    private Transform target;
    private Rigidbody2D myRigidbody;

    private Vector3 finalDirection;
    private float height;
    private float distance;
    private float jumpTime = 1f;
    [SerializeField]
    private bool isGrounded;

    //increased speed timers and bool
    private bool _collidedWithPlayer;
    private bool _disablePlayerColisionForSomeTime;

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
    private Vector2 floorCheckSize;
    [SerializeField]
    private Vector2 ofsetTargetCheck;
    [SerializeField]
    private float targetCheckRadius;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isGrounded)
        {
            if (other.CompareTag("Player"))
            {
                target = other.transform;
                Invoke("JumpToTarget", 1f);
            }
            else if (other.CompareTag("FlyEnemyInside") )
            {
                target = other.transform;
                Invoke("JumpToTarget", 0f);
            }
        }

    }

    private void JumpToTarget()
    {
        float deltaY = target.position.y - transform.position.y;
        float deltaX = target.position.x - transform.position.x;
        float throwAngle = Mathf.Atan((deltaY + 4.803f * (jumpTime * jumpTime)) / deltaX);
        float totalVelocity = deltaX / Mathf.Cos(throwAngle);
        finalDirection = new Vector2(totalVelocity * Mathf.Cos(throwAngle), totalVelocity * Mathf.Sin(throwAngle)) * speed;
        myRigidbody.velocity = finalDirection;
    }

    private void FixedUpdate()
    {
        isGrounded = IsGrounded();
        //if enemy collided with the player, call hit
        if (_collidedWithPlayer == true) {

        }

        //disable colliders for some time after collision with player
        if (_disablePlayerColisionForSomeTime == true)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
        }
        //reset enemy colliders
        else
        {
            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<CircleCollider2D>().enabled = true;
        }
    }
    private bool IsGrounded()
    {
        Vector2 position = new Vector2(transform.position.x + ofsetFloorCheck.x, transform.position.y + ofsetFloorCheck.y);
        Collider2D[] col = Physics2D.OverlapBoxAll(position, floorCheckSize, groundLayerMask);
        return (col.Length > 0 || col != null);
    }


    private void OnDrawGizmosSelected()
    {
        Vector2 position = new Vector2(transform.position.x + ofsetFloorCheck.x, transform.position.y + ofsetFloorCheck.y);
        Gizmos.color = Color.red;
        Gizmos.DrawCube(position, floorCheckSize);
    }

    private void DealDamageToPlayer()
    {
    }

}