using UnityEngine;
using System.Collections;

public class JumpEnemy : MonoBehaviour
{
    private Transform target;
    private Rigidbody2D rigid;
    private Vector3 finalVelocity;

    [SerializeField]
    float initialAngle;

    void Start()
    {
         rigid = GetComponent<Rigidbody2D>();

        // Alternative way:
        // rigid.AddForce(finalVelocity * rigid.mass, ForceMode.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            target = other.transform;

            Vector3 p = target.position;

            float gravity = Physics2D.gravity.magnitude;
            // Selected angle in radians
            float angle = initialAngle * Mathf.Deg2Rad;

            // Positions of this object and the target on the same plane
            Vector3 planarTarget = new Vector3(p.x, 0, p.z);
            Vector3 planarPostion = new Vector3(transform.position.x, 0, transform.position.z);

            // Planar distance between objects
            float distance = Vector2.Distance(planarTarget, planarPostion);
            // Distance along the y axis between objects
            float yOffset = transform.position.y - p.y;

            float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

            Vector3 velocity = new Vector3(0, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

            // Rotate our velocity to match the direction between the two objects
            float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTarget - planarPostion);
            finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;


            Invoke("JumpToTarget",2f);
           // rigid.velocity = finalVelocity;
           
        }
    }

    private void JumpToTarget()
    {
        rigid.AddForce(finalVelocity * rigid.mass, ForceMode2D.Impulse);
    }
}