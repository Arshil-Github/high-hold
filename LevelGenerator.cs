using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject LevelChunk_pf;
    public int initialSpawnChunkRadius;

    [Header("Ranndom Spawns Level Chunk ")]
    public int randomSpawnsPerChunk_upperLimit;
    public int randomSpawnsPerChunk_lowerLimit;
    public int chunkLoadDist;

    public Transform player;
    public Transform ChunkHolder;

    List<GameObject> Chunks;

    Vector2 originalPos;
    public void Start()
    {
        Chunks = new List<GameObject>();
        originalPos = transform.position;
        //For For Square
        for(int x = -40 * initialSpawnChunkRadius; x <= 40 * initialSpawnChunkRadius ; x = x + 40)
        {
            for(int y = -40 * initialSpawnChunkRadius; y <= 40 * initialSpawnChunkRadius; y= y + 40)
            {
                if(x == 0 && y == 0)
                {
                }
                else
                {
                    GameObject lc = Instantiate(LevelChunk_pf, new Vector3(x + transform.position.x, y + transform.position.y, 1), Quaternion.identity);
                    lc.GetComponent<LevelChunk>().spawns = Random.Range(randomSpawnsPerChunk_lowerLimit, randomSpawnsPerChunk_upperLimit);
                    lc.transform.parent = ChunkHolder;
                    Chunks.Add(lc);
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if (Mathf.Abs((player.position - transform.position).x) > (initialSpawnChunkRadius - 1) * 40)
        {
            transform.position = new Vector3(player.position.x - player.position.x % 40, player.position.y - player.position.y % 40, transform.position.z);
            for (int i = -initialSpawnChunkRadius; i <= initialSpawnChunkRadius; i++)
            {
                float posX = transform.position.x + (80 * Mathf.Sign(player.transform.position.x));
                float posY = transform.position.y + (i * 40);

                GameObject lc = Instantiate(LevelChunk_pf, new Vector3(posX, posY, 1), Quaternion.identity);
                lc.GetComponent<LevelChunk>().spawns = Random.Range(randomSpawnsPerChunk_lowerLimit, randomSpawnsPerChunk_upperLimit);
                lc.transform.parent = ChunkHolder;
                Chunks.Add(lc);
            }


            foreach (GameObject g in Chunks)
            {
                if (Mathf.Abs((player.position - g.transform.position).magnitude) > chunkLoadDist)
                {
                    g.SetActive(false);
                }
                else
                {
                    g.SetActive(true);
                }
            }

            originalPos = transform.position;
        }
        if (Mathf.Abs((player.position - transform.position).y) > (initialSpawnChunkRadius - 1) * 40)
        {
            transform.position = new Vector3(player.position.x - player.position.x % 40, player.position.y - player.position.y % 40, transform.position.z);
            for (int i = -initialSpawnChunkRadius; i <= initialSpawnChunkRadius; i++)
            {
                float posX = transform.position.x+ (i * 40);
                float posY = transform.position.y + (80 * Mathf.Sign(player.transform.position.y));

                GameObject lc = Instantiate(LevelChunk_pf, new Vector3(posX, posY, 1), Quaternion.identity);
                lc.GetComponent<LevelChunk>().spawns = Random.Range(randomSpawnsPerChunk_lowerLimit, randomSpawnsPerChunk_upperLimit);
                lc.transform.parent = ChunkHolder;
                Chunks.Add(lc);
            }

            foreach(GameObject g in Chunks)
            {
                if (Mathf.Abs((player.position - g.transform.position).magnitude) > chunkLoadDist)
                {
                    g.SetActive(false);
                }
                else
                {
                    g.SetActive(true);
                }
            }

            originalPos = transform.position;
        }
    }
    private void Update()
    {
    }
}
