using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoyoteSpawner : MonoBehaviour
{
    [SerializeField] float maxAnimalsPerDay = 1;
    [SerializeField] GameObject coyotePrefab;

    float animalSpawnTimer;
    float elaspedSpawnTime;

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
            Instantiate(coyotePrefab, transform.position + (Vector3.left * Random.Range(-5f, 5f)) + (Vector3.forward * Random.Range(-5f, 5f)), Quaternion.identity, AnimalPool.Instance.transform);

            elaspedSpawnTime = 0;
        }

        elaspedSpawnTime += Time.deltaTime;
    }
}
