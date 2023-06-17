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
        /*attackDots.gameObject.SetActive(false);
        Invoke("ApplySkin", 0.2f);

        audioS = gameObject.GetComponent<AudioSource>();*/
        currentState = playerState.normal;
        rb.drag = normalDrag;
    }
    public void ApplySkin()
    {
        /*Circle.sprite = currentSkin.sprite;
        Circle.color = currentSkin.color;

        pf_bullet = currentSkin.bullet;

        sfx_Attack = currentSkin.attackSFX;
        sfx_coinPickup = currentSkin.coinPickup;

        pf_deatheffect = currentSkin.burstEffect;*/
    }



    Vector2 swipeStart;
    Vector2 swipeEnd;
    Vector2 swipe;

    [Tooltip("The Post Processing manager")]public PPManager postProcessor;
    public float swipeRange;
    public float dashForce;
    public float maxSwipeBooster;
    public float swipeTime;
    public float normalDrag;
    public float chargedDrag;
    public float swipeCooldown;

    bool isTouched;
    float currentSwipeBooster;
    bool canSwipe = true;
    enum playerState
    {
        normal,
        charged
    }
    playerState currentState;


    public void FixedUpdate()
    {

        /*if (!Stop && Action == "Move")
        {
            transform.position += mover.up * speed * Time.deltaTime;
        }
        else if (Action == "Attack" && !Stop)
        {
            *//*GameObject bullet = Instantiate(pf_bullet, attackDots.position, attackDots.rotation);
            bullet.GetComponent<Bullet>().Target = GetClosestEnemy(GameObject.FindGameObjectsWithTag("Enemy"));
            bullet.GetComponent<Bullet>().rb.AddForce((bullet.transform.up) * bulletForce, ForceMode2D.Impulse);*//*

            Action = "Move";
        }
        else if(Stop && Action == "ChangeDirection")
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
        }
        else if(Stop && Action == "ChangeAttackDir")
        {
            attackDots.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
        }*/

        
    }
    public bool startCoolDown = false;
    public void Update()
    {
        //Get the Touch Input and do thigs ACcodingly
        if (Input.touchCount != 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            TouchDown();
        if (Input.touchCount != 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            TouchUp();

        //This to add a little boost when the player swipe for longer distance
        if(isTouched && currentSwipeBooster < maxSwipeBooster)
        {
            currentSwipeBooster += 0.1f;
        }
    }
    
    public void TouchDown()
    {
        if (!canSwipe)
            return;
        //Called when the swipe Starts
        currentSwipeBooster = 1;
        isTouched = true;

        //VElocity to nill
        rb.velocity = Vector2.zero;

        //Disable the text "Hold to continue" at the start
        if (tutorialText != null)
        {
            tutorialText.SetActive(false);

        }
        //Stop Time
        Stop = true;
        gm.pause = true;
        Bg_Sound.Instance.StopMusic();

        //  Changing post processing this can be better done by creating a post process Manager Comeback here
            postProcessing_slowedDown.SetActive(true);

        //Goes to each enemy and stops time for them
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemy.GetComponent<Enemy>().slow = true;
        }
        //Collect Data on Starting Position

        swipeStart = Input.GetTouch(0).position;

        /*
        //This do the Action on the bar
        if (gm.currentState == GameManager.barState.ChangeDirection)
        {
            //Show Dirn Pointer
            Action = "ChangeDirection";
            postProcessing_slowedDown.SetActive(true);
        }
        else if (gm.currentState == GameManager.barState.Attack)
        {
            //Show Attack Pointer
            attackDots.gameObject.SetActive(true);

            Action = "ChangeAttackDir";
            postProcessing_attack.SetActive(true);
        }*/
    }
    public void TouchUp()
    {
        if (!canSwipe)
            return;
        //Called when the player lifts his finger ie End of the Swipe

        isTouched = false;

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

        swipeEnd = Input.GetTouch(0).position;

        swipe = swipeEnd - swipeStart;

        if(swipe.magnitude >= swipeRange)
        {
            //Add a force in opposite direction of swipe vector
            rb.AddForce(dashForce * currentSwipeBooster * (-swipe / swipe.magnitude), ForceMode2D.Impulse);
        }

        //Change State to Boosted

        ChangeState(playerState.charged);

        //Start a coroutibne to turn off canSwipe
        StartCoroutine(SwitchCanSwipe());


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
        /*if (collision.CompareTag("coin"))
        {
            audioS.clip = sfx_coinPickup;
            audioS.Play();
            gm.AddCoin(1);
            Destroy(collision.gameObject);
        }*/
    }
    private void ChangeState(playerState newState)
    {
        currentState = newState;
        if(currentState == playerState.normal)
            EnterNormalState();
        else if(currentState == playerState.charged)
            EnterChargedState();
    }
    public void die()
    {
        /*if (!gm.GetComponent<PowerUps>().isShieldOn)
        {
            GameObject a = Instantiate(pf_deatheffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            FindObjectOfType<GameManager>().gameOver();
            Destroy(a, a.GetComponent<ParticleSystem>().time + 0.5f);
        }*/
    }

    private void EnterNormalState()
    {
        //This is for the Charged State
        rb.drag = normalDrag;
        Circle.color = Color.white;
    }
    private void EnterChargedState()
    {
        //This is the Charged State
        //Write all the graphics changes of Charged state here

        rb.drag = chargedDrag;
        Circle.color = Color.black;

        StartCoroutine(chargedCoolDown());

    }

    IEnumerator chargedCoolDown()
    {
        yield return new WaitForSeconds(swipeTime);
        ChangeState(playerState.normal);
    }
    IEnumerator SwitchCanSwipe()
    {
        canSwipe = false;
        yield return new WaitForSeconds(swipeCooldown);
        canSwipe = true;
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

    //This Section is to be deleted in the final build. use this to make your life easier
    Rect rect = new Rect(0, 0, 300, 100);
    Vector3 offset = new Vector3(0f, 0f, 0f); // height above the target position
    void OnGUI()
    {
        Vector2 point = Camera.main.WorldToScreenPoint(transform.position + offset);
        rect.x = point.x;
        rect.y = Screen.height - point.y - rect.height; // bottom left corner set to the 3D point

        if(rb.velocity != Vector2.zero)
        {
            GUI.Label(rect, rb.velocity.magnitude.ToString()); // display its name, or other string
        }

    }
}
