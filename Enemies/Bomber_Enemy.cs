using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber_Enemy : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float rangeForSlowDown;
    private Vector3 movement;
    public Rigidbody2D rb;
    public float slowSpeed;
    public GameObject pf_dieEffect;
    public GameObject pf_explodeEffect;

    public Transform rangeShow;
    public float attackRange;

    [Header("System Variables")]
    public bool inRange = false;
    public bool canExplode = false;
    public bool slow = false;
    public int coinRewarded;
    public int scoreRewarded;


    public AudioClip explodeSFX;
    public AudioClip dieSFX;

    AudioSource audioS;
    private void Start()
    {
        rangeShow.localScale = new Vector3(attackRange * 2, attackRange * 2, rangeShow.localScale.z);
        audioS = gameObject.GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        if((transform.position - GetClosestEnemy(GameObject.FindGameObjectsWithTag("Player")).position).magnitude >= attackRange)
        {   
            if (inRange == true)
            {
                movement = GetClosestEnemy(GameObject.FindGameObjectsWithTag("Player")).position - transform.position;

                if (slow)
                {
                    transform.position += (movement) * slowSpeed * Time.deltaTime;
                }
                else
                {
                    transform.position += (movement) * maxSpeed * Time.deltaTime;
                }
            }
        }
        else
        {
            Explode();
        }

        if ((transform.position - GetClosestEnemy(GameObject.FindGameObjectsWithTag("Player")).position).magnitude >= rangeForSlowDown)
        {
            slow = false;
        }
        else
        {
            slow = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
        }
    }
    public void kill()
    {
        GameObject effect = Instantiate(pf_dieEffect, transform.position, transform.rotation);

        audioS.clip = dieSFX;
        audioS.Play();

        Destroy(effect, effect.GetComponent<ParticleSystem>().time + 0.5f);
        Destroy(gameObject);
    }
    public void Explode()
    {
        foreach(GameObject gb in GameObject.FindGameObjectsWithTag("Player")){
            gb.GetComponent<PlayerMovement>().die();
        }

        GameObject effect = Instantiate(pf_explodeEffect, transform.position, transform.rotation);
        Destroy(effect, effect.GetComponent<ParticleSystem>().time + 0.5f);

        FindObjectOfType<GameManager>().AddCoin(coinRewarded);
        FindObjectOfType<GameManager>().AddScore(scoreRewarded);


        audioS.clip = explodeSFX;
        audioS.Play();

        Destroy(gameObject);
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
