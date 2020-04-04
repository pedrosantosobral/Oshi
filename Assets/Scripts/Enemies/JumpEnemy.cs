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
        if (isGrounded && timer >= jumpTimer)
        {
            if (other.CompareTag("Player"))
            {
                timer = 0f;
                target = other.transform;
                Invoke("JumpToTarget", 0f);

            }
            else if (other.CompareTag("FlyEnemyInside"))
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
        timer += Time.deltaTime;

        IsGrounded();
        Debug.Log(isGrounded);


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

    private void DealDamageToPlayer()
    {
    }

}