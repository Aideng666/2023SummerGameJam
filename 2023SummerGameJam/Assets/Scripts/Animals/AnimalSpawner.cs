using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    [SerializeField] float maxAnimalsPerDay = 3;

    float animalSpawnTimer;float elaspedSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        animalSpawnTimer = CommunityManager.Instance.DayLength / maxAnimalsPerDay;
        elaspedSpawnTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (elaspedSpawnTime >= animalSpawnTimer) 
        {
            AnimalPool.Instance.SpawnAnimal((AnimalTypes)Random.Range(0, 4), transform.position + (Vector3.left * Random.Range(-5f, 5f)) + (Vector3.forward * Random.Range(-5f, 5f)));

            elaspedSpawnTime = 0;
        }

        elaspedSpawnTime += Time.deltaTime;
    }
}
