using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform Target;
    public float bulletForce;
    public Rigidbody2D rb;
    public void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if(collision.TryGetComponent<Enemy>(out Enemy e))
            {
                collision.GetComponent<Enemy>().kill();
            }
            else if (collision.TryGetComponent<Bomber_Enemy>(out Bomber_Enemy Be))
            {
                collision.GetComponent<Bomber_Enemy>().kill();
            }
            //collision.GetComponent<Enemy>().kill();
            Destroy(gameObject);
        }
    }
}
