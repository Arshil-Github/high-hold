using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb_Spawner : MonoBehaviour
{
    public GameObject pf_ToSpawn;
    public float minTimeInBtw;
    public float maxTimeInBtw;

    public void Start()
    {
        StartCoroutine(spawn_prefab());
    }

    IEnumerator spawn_prefab()
    {
        float timeDelay = Random.Range(minTimeInBtw, maxTimeInBtw);
        yield return new WaitForSeconds(timeDelay);

        Instantiate(pf_ToSpawn, transform.position, Quaternion.identity);

        StartCoroutine(spawn_prefab());

    }
}
