using UnityEngine;
using UnityEngine.SceneManagement;
using CustomEventSystem;

public class PlayerInteractions : MonoBehaviour
{
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
    private int colectedColectables = -1;

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

        if(other.CompareTag("teleport"))
        {
            showTeleportButton.Raise();
        }

        if (invinciblePlayerForSomeTime == false)
        {
            if (!other.isTrigger && other.CompareTag("PatrolEnemy") || other.CompareTag("FlyEnemy") && !other.isTrigger || other.CompareTag("JumpEnemyInside") && !other.isTrigger)
            {
                if (_HP == 2)
                {
                    invinciblePlayerForSomeTime = true;
                }

                _HP -= 1;
                _invincibleTimeReference = invincibleTime;
                //damage player;
                _paintLightReference.GetComponent<PaintLight>().playerIsDamaged = true;
            }

            if (other.CompareTag("hpRecharge"))
            {
                if (_HP == 1)
                {
                    _invincibleTimeReference = invincibleTime;
                    _paintLightReference.GetComponent<PaintLight>().playerIsDamaged = false;
                    _HP = _HP + 1;
                }
            }
        }

        if (other.CompareTag("Colectable"))
        {
            Destroy(other.gameObject);
            colectedColectables++;
            _savedDataReference.ActivateColectable(colectedColectables);
        }

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

        animator.SetBool("IsDamaged", invinciblePlayerForSomeTime);
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
        //reload the scene
        if (SceneManager.GetActiveScene().name == "Oshi_Tutorial")
        {
            SceneManager.LoadScene("DiedTutorialScreen");
        }
        else if (SceneManager.GetActiveScene().name == "Oshi")
        {
            SceneManager.LoadScene("DiedScreen");
        }
    }

    public void HitByTrap()
    {
        if (_HP == 2)
        {
            invinciblePlayerForSomeTime = true;
        }

        _HP -= 1;
        _invincibleTimeReference = invincibleTime;
        _paintLightReference.GetComponent<PaintLight>().playerIsDamaged = true;
    }
}
