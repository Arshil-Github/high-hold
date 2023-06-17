using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float accelerationTime = 2f;
    public float maxSpeed = 5f;
    private Vector3 movement;
    private float timeLeft;
    public Rigidbody2D rb;
    public float slowSpeed;
    public bool slow = false;
    public GameObject pf_dieEffect;

    public Vector3 max_Scale;
    public Vector3 min_Scale;

    public int coinRewarded;
    public int scoreRewarded;

    public AudioClip dieSFX;

    AudioSource audioS;
    private void Start()
    {
        float x = Random.Range(min_Scale.x, max_Scale.x);
        Vector3 randomized_Scale = new Vector3(x, x);
        transform.localScale = randomized_Scale;

        audioS = GameObject.FindGameObjectWithTag("EnemySFXGen").GetComponent<AudioSource>();
    }
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            movement = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            timeLeft += accelerationTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().die();
        }
    }
    void FixedUpdate()
    {
        if (slow)
        {
            transform.position += (movement) * slowSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += (movement) * maxSpeed * Time.deltaTime;
        }
    }
    public void kill()
    {
        FindObjectOfType<GameManager>().AddCoin(coinRewarded);
        FindObjectOfType<GameManager>().AddScore(scoreRewarded);

        audioS.clip = dieSFX;
        audioS.Play();

        GameObject effect = Instantiate(pf_dieEffect, transform.position, transform.rotation);
        Destroy(effect, effect.GetComponent<ParticleSystem>().time + 0.5f);
        Destroy(gameObject);

    }
}
