using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float radius;
    public float detoriateTime;
    public Transform rangeShow;
    public GameObject pf_detoriate_vfx;


    public void Start()
    {
        rangeShow.localScale = new Vector3(radius * 2, radius * 2, rangeShow.localScale.z);
        StartCoroutine(deterioteDelay());
    }
    IEnumerator deterioteDelay()
    {
        yield return new WaitForSeconds(detoriateTime);
        Collider2D[] inRadius = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D enemy in inRadius)
        {
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<Enemy>().kill();
            }
        }

        GameObject effect = Instantiate(pf_detoriate_vfx, transform.position, transform.rotation);
        effect.transform.localScale = new Vector3(2, 2, 0);
        Destroy(effect, effect.GetComponent<ParticleSystem>().time + 0.5f);

        Destroy(gameObject);
    }
}
