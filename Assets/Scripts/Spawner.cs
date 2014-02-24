using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject[] items;
    public float spawnRate = 1;
    private int previusRandom;

    void Start()
    {
        previusRandom = 1;
        InvokeRepeating("Spawn", 2, spawnRate);
    }

    void Spawn()
    {
        int random = Random.Range(0, items.Length);
        if(random == previusRandom)
            random = Random.Range(0, items.Length);
        if(random == previusRandom)
            random = Random.Range(0, items.Length);


        Instantiate(items[random], transform.position, Quaternion.identity);
        previusRandom = random;
    }

}
