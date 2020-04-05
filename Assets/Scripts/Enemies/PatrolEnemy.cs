using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    public GameObject insideToRotate;
    public GameObject spriteToRotate;

    //collider references
    public Collider2D enemyCollider;
    public Collider2D enemyColliderTrigger;

    //increased speed timers and bool
    public float    timeWithIncreasedSpeed;
    private float   _timeWithIncreasedSpeedReference;
    private bool    _collidedWithPlayer;
    private bool    _disablePlayerColisionForSomeTime;

    //speed variables
    public float    defaultSpeed;
    public float    increasedSpeed;
    private float   _speed;

    //ground and wall detection references
    public Transform groundDetection;
    public Transform wallDetection;
    public LayerMask whatIsGround;
    private float   _overlapGroundRad = 0.3f;
    private bool    _isGrounded;
    private bool    _isWall;

    private bool    _movingRight      = true;

    //enemy light reference
    public GameObject enemyLightReference;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_disablePlayerColisionForSomeTime == false)
        {
            if (other.CompareTag("Player") && other.isTrigger)
            {
                _collidedWithPlayer = true;
                _timeWithIncreasedSpeedReference = timeWithIncreasedSpeed;
                enemyLightReference.transform.localScale = new Vector3(3f, 3f, 0);

                if (_movingRight == true && other.transform.position.x > gameObject.transform.position.x)
                {
                    //rotate sprite
                    spriteToRotate.GetComponent<SpriteRenderer>().flipX = true;

                    insideToRotate.transform.eulerAngles = new Vector3(0, -180, 0);
                    _movingRight = false;
                }
                else if (_movingRight == false && other.transform.position.x < gameObject.transform.position.x)
                {
                    //rotate sprite
                    spriteToRotate.GetComponent<SpriteRenderer>().flipX = false;

                    insideToRotate.transform.eulerAngles = new Vector3(0, 0, 0);
                    _movingRight = true;
                }
            }
        }

        //if (other.CompareTag("FlyEnemy") && other.isTrigger == false)
        //{
        //    Destroy(this.gameObject);
        //}
    }

    private void Start()
    {
        _disablePlayerColisionForSomeTime = false;
        _collidedWithPlayer = false;
        _speed = defaultSpeed;
    }
    private void FixedUpdate()
    {
        //if enemy collided with the player, enter in panic
        if (_collidedWithPlayer == true)
        {
            IncreaseSpeedForSomeTime();
        }

        //disable colliders for some time after collision with player
        if (_disablePlayerColisionForSomeTime == true)
        {
            enemyCollider.enabled = false;
            enemyColliderTrigger.enabled = false;
        }
        //reset enemy colliders
        else
        {
            enemyCollider.enabled = true;
            enemyColliderTrigger.enabled = true;
        }
      
        //move the enemy in one direction
        if(_movingRight == true)
        {
            transform.Translate(Vector2.right * _speed * Time.deltaTime);
        }
        else 
        {
            transform.Translate(Vector2.left * _speed * Time.deltaTime);
        }
        
        //cast circles to check ground or walls to change direction
        _isGrounded = Physics2D.OverlapCircle(groundDetection.position, _overlapGroundRad, whatIsGround);
        _isWall = Physics2D.OverlapCircle(wallDetection.position, _overlapGroundRad, whatIsGround);

        //if its not touching the ground or finds a wall
        if (!_isGrounded || _isWall)
        {
            if (_movingRight == true)
            {
                //rotate sprite
                spriteToRotate.GetComponent<SpriteRenderer>().flipX = true;
                //rotate the enemy(change direction)
                insideToRotate.transform.eulerAngles = new Vector3(0f, -180f, 0f);
                _movingRight = false;
            }
            else
            {
                //rotate sprite
                spriteToRotate.GetComponent<SpriteRenderer>().flipX = false;
                //rotate the enemy(change direction)
                insideToRotate.transform.eulerAngles = new Vector3(0f, 0f, 0f);
                _movingRight = true;
            }
        }

    }

    private void IncreaseSpeedForSomeTime()
    {
        _timeWithIncreasedSpeedReference -= Time.deltaTime;
        _speed = increasedSpeed;
        _disablePlayerColisionForSomeTime = true;

        //decrease enemy light
        enemyLightReference.SetActive(true);
        enemyLightReference.transform.localScale -= new Vector3(1f, 1f, 0) * Time.deltaTime;
        
        if(_timeWithIncreasedSpeedReference <= 0)
        {
            _speed = defaultSpeed;
            _collidedWithPlayer = false;
            _disablePlayerColisionForSomeTime = false;
            //turn off enemy light completely
            enemyLightReference.SetActive(false); ;

        }
        
    }
}

