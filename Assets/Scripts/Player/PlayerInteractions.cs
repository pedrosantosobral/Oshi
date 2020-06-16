using UnityEngine;
using UnityEngine.SceneManagement;
using CustomEventSystem;
using UnityEngine.Experimental.Rendering.LWRP;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private GameObject playerHurtFeedback;
    [SerializeField] private Light2D playerLight;
    [SerializeField] private VoidEvent flyEnemyAtackAnim;
    [SerializeField] private VoidEvent jumpEnemyAtackAnim;
    [SerializeField] private GameObject deathFeedback;
    [SerializeField] private VoidEvent playerDeathFadeEvent;
    [SerializeField] private GameObject playerHP_particles;

    public Animator animator;

    public Collider2D lightCollider;

    private VoidEvent setPlayerOnTeleportManager;

    [SerializeField] private VoidEvent showTeleportButton;
    [SerializeField] private VoidEvent hideTeleportButton;

    //invincible timer variables
    private float _invincibleTimeReference;
    public float invincibleTime;
    private bool invinciblePlayerForSomeTime;
    //paint light component reference
    private GameObject _paintLightReference;
    //player HP
    private int _HP;
    private SavedData _savedDataReference;
    private int colectedColectables = 0;

    // Start is called before the first frame update
    void Start()
    {
        _paintLightReference = GameObject.Find("PaintLight");
        _savedDataReference = GameObject.Find("SaveData").GetComponent<SavedData>();

        _HP = 1;
        invinciblePlayerForSomeTime = true;
        _invincibleTimeReference = invincibleTime;
        _paintLightReference.GetComponent<PaintLight>().playerIsDamaged = true;
        //setPlayerOnTeleportManager.Raise();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Physics2D.IgnoreCollision(lightCollider, other, true);

        if (other.CompareTag("teleport"))
        {
            showTeleportButton.Raise();
        }

        if (invinciblePlayerForSomeTime == false)
        {
            if (!other.isTrigger && other.CompareTag("PatrolEnemy") || other.CompareTag("FlyEnemy") && !other.isTrigger || other.CompareTag("JumpEnemyInside") && !other.isTrigger)
            {
                if( other.CompareTag("FlyEnemy") && !other.isTrigger)
                {
                    flyEnemyAtackAnim.Raise();
                }
                if (other.CompareTag("JumpEnemyInside") && !other.isTrigger)
                {
                    jumpEnemyAtackAnim.Raise();
                }


                if (_HP == 2)
                {
                    invinciblePlayerForSomeTime = true;
                    Instantiate(playerHurtFeedback, gameObject.transform.position, Quaternion.identity);
                }

                _HP -= 1;
                _invincibleTimeReference = invincibleTime;
                //damage player;
                _paintLightReference.GetComponent<PaintLight>().playerIsDamaged = true;
                playerLight.intensity = 0.33f;
            }

            if (other.CompareTag("hpRecharge"))
            {
                if (_HP == 1)
                {
                    Instantiate(playerHP_particles, other.gameObject.transform.position, Quaternion.identity);
                    _invincibleTimeReference = invincibleTime;
                    _paintLightReference.GetComponent<PaintLight>().playerIsDamaged = false;
                    _HP = _HP + 1;
                    playerLight.intensity = 0.65f;
                }
            }
        }

        //if (other.CompareTag("Colectable") && this.gameObject.GetComponent<Collider2D>() is EdgeCollider2D)
        //{
        //    Destroy(other.gameObject);
        //    _savedDataReference.ActivateColectable(colectedColectables);
        //    Debug.Log("enteredColectable number" + (colectedColectables));
        //    colectedColectables++;
        //}

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("teleport"))
        {
            hideTeleportButton.Raise();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        animator.SetBool("haveDamage", invinciblePlayerForSomeTime);
        animator.SetInteger("HP", _HP);

        if (_HP <= 0)
        {
            KillPlayer();
        }

        if (invinciblePlayerForSomeTime == true)
        {
            InvinciblePlayerForSomeTime();
        }
    }

    private void InvinciblePlayerForSomeTime()
    {
        _invincibleTimeReference -= Time.deltaTime;
        //disable colisions between player and enemies
        Physics2D.IgnoreLayerCollision(12, 9, true);

        animator.Play("PlayerInvencible2");

        if (_invincibleTimeReference <= 0)
        {
            //_changeSpriteAlpha = false;
            //enable colisions between player and enemies
            invinciblePlayerForSomeTime = false;
            Physics2D.IgnoreLayerCollision(12, 9, false);
        }
    }

    private void KillPlayer()
    {
        Instantiate(deathFeedback, gameObject.transform.position, Quaternion.identity);
        playerDeathFadeEvent.Raise();
        Destroy(gameObject);

    }

    public void HitByTrap()
    {
        if(invinciblePlayerForSomeTime == false)
        {
            if (_HP == 2)
            {
                Instantiate(playerHurtFeedback, gameObject.transform.position, Quaternion.identity);
                invinciblePlayerForSomeTime = true;

            }

            _HP -= 1;
            _invincibleTimeReference = invincibleTime;
            _paintLightReference.GetComponent<PaintLight>().playerIsDamaged = true;
            playerLight.intensity = 0.33f;
        }
        
        
    }
}
