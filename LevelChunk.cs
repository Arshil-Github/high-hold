using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChunk : MonoBehaviour
{
    public List<GameObject> SubChunks;
    public List<GameObject> Spawnables;
    public int spawns;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i <= spawns; i++)
        {
            int chunkRandomator = Random.Range(0, SubChunks.Count);
            int spawnRandomator = Random.Range(0, Spawnables.Count);

            GameObject a = Instantiate(Spawnables[spawnRandomator], SubChunks[chunkRandomator].transform.position, Quaternion.identity);
            a.transform.parent = SubChunks[chunkRandomator].transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
