using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed; //Speed
    public float rotationSpeed; //This rotates when the player holds the screen
    Rigidbody2D rb; //Can be Deleted?

    [HideInInspector]public bool Stop;
    [HideInInspector]public Vector3 moveDir; //Dirn in which we want to move
    [HideInInspector]public string Action;

    public GameObject pf_bullet;
    public GameManager gm;

    public float bulletForce;
    public Transform mover;
    public Transform attackDots;
    public float changeStateCooldownTime;
    public bool allowedToChange = true;
    public GameObject postProcessing_slowedDown;
    public GameObject postProcessing_attack;

    public GameObject pf_deatheffect;

    [Header("Skin Stuff")]
    public Skin currentSkin;
    public SpriteRenderer Circle;
    public SpriteRenderer Pointer;

    [Header("Sound Stuff")]
    public AudioClip sfx_Attack;
    public AudioClip sfx_coinPickup;
    AudioSource audioS;

    public GameObject tutorialText;
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Get the rb Component of Player
        attackDots.gameObject.SetActive(false);
        Invoke("ApplySkin", 0.2f);

        audioS = gameObject.GetComponent<AudioSource>();

    }
    public void ApplySkin()
    {
        Circle.sprite = currentSkin.sprite;
        Circle.color = currentSkin.color;

        pf_bullet = currentSkin.bullet;

        sfx_Attack = currentSkin.attackSFX;
        sfx_coinPickup = currentSkin.coinPickup;

        pf_deatheffect = currentSkin.burstEffect;
    }



    Vector2 swipeStart;
    Vector2 swipeEnd;
    Vector2 swipe;
    [Tooltip("The Post Processing manager")]public PPManager postProcessor;





    public void FixedUpdate()
    {

        if (!Stop && Action == "Move")
        {
            transform.position += mover.up * speed * Time.deltaTime;
        }
        else if (Action == "Attack" && !Stop)
        {
            /*GameObject bullet = Instantiate(pf_bullet, attackDots.position, attackDots.rotation);
            bullet.GetComponent<Bullet>().Target = GetClosestEnemy(GameObject.FindGameObjectsWithTag("Enemy"));
            bullet.GetComponent<Bullet>().rb.AddForce((bullet.transform.up) * bulletForce, ForceMode2D.Impulse);*/

            Action = "Move";
        }
        else if(Stop && Action == "ChangeDirection")
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
        }
        else if(Stop && Action == "ChangeAttackDir")
        {
            attackDots.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
        }
    }
    public bool startCoolDown = false;
    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.A) && !startCoolDown)
        {
            TouchDown();
        }
        else if(Input.GetKeyUp(KeyCode.A))
        {
            TouchUp();
        }

    }
    
    public void TouchDown()
    {
        //Called when the swipe Starts
        Stop = true;

        Bg_Sound.Instance.StopMusic();

        if (gm.currentState == GameManager.barState.ChangeDirection)
        {
            Action = "ChangeDirection";
            postProcessing_slowedDown.SetActive(true);
        }
        else if (gm.currentState == GameManager.barState.Attack)
        {
            attackDots.gameObject.SetActive(true);

            Action = "ChangeAttackDir";
            postProcessing_attack.SetActive(true);
        }
        gm.pause = true;


        if (tutorialText != null)
        {
            tutorialText.SetActive(false);

        }

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if(enemy.TryGetComponent<Enemy>(out Enemy e))
            {
                enemy.GetComponent<Enemy>().slow = true;
            }
            else if (enemy.TryGetComponent<Bomber_Enemy>(out Bomber_Enemy f))
            {
                enemy.GetComponent<Bomber_Enemy>().slow = true;
            }
        }
    }
    public void TouchUp()
    {
        //Called when the player lifts his finger ie End of the Swipe


        //Resume Time
        Stop = false;
        Bg_Sound.Instance.StartMusic(Bg_Sound.Instance.mainSong);
        attackDots.gameObject.SetActive(false);

        gm.pause = false;//Contacting the game manger object

        //  Changing post processing this can be better done by creating a post process Manager Comeback here
            postProcessing_slowedDown.SetActive(false);
            postProcessing_attack.SetActive(false);


        //Go to each enemy and resume them ----. This can be done in a better way
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (enemy.TryGetComponent<Enemy>(out Enemy e))
            {
                enemy.GetComponent<Enemy>().slow = false;
            }
            else if (enemy.TryGetComponent<Bomber_Enemy>(out Bomber_Enemy f))
            {
                enemy.GetComponent<Bomber_Enemy>().slow = false;
            }
        }

        //Finish the swipe track and add force Accodingly

            

        /*if (!startCoolDown && Stop)
        {
        // This is for controlling the cool down bar
            gm.GetComponent<GameManager>().StartCoolDown();
            startCoolDown = true;
        }*/


        /*if (gm.currentState == GameManager.barState.ChangeDirection)
        {
            Action = "Move";
        }
        else if (gm.currentState == GameManager.barState.Attack)
        {
            Action = "Attack";
            audioS.clip = sfx_Attack;
            audioS.Play();

        }*/
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("coin"))
        {
            audioS.clip = sfx_coinPickup;
            audioS.Play();
            gm.AddCoin(1);
            Destroy(collision.gameObject);
        }
    }

    public void die()
    {
        if (!gm.GetComponent<PowerUps>().isShieldOn)
        {
            GameObject a = Instantiate(pf_deatheffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            FindObjectOfType<GameManager>().gameOver();
            Destroy(a, a.GetComponent<ParticleSystem>().time + 0.5f);
        }
    }
    Transform GetClosestEnemy(GameObject[] enemies)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.transform;
            }
        }

        return bestTarget;
    }
}
