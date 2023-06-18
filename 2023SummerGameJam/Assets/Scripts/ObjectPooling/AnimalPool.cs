using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalPool : MonoBehaviour
{
    [SerializeField] GameObject squirrelPrefab;
    [SerializeField] GameObject woodpeckerPrefab;
    [SerializeField] GameObject beaverPrefab;
    [SerializeField] GameObject duckPrefab;

    Queue<GameObject> availableSquirrels = new Queue<GameObject>();
    Queue<GameObject> availableWoodpeckers = new Queue<GameObject>();
    Queue<GameObject> availableBeavers = new Queue<GameObject>();
    Queue<GameObject> availableDucks = new Queue<GameObject>();

    float numEachAnimal = 5;

    public static AnimalPool Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        CreatePools();
    }

    void CreatePools()
    {
        for (int i = 0; i < numEachAnimal; i++)
        {
            GameObject squirrel = Instantiate(squirrelPrefab);
            availableSquirrels.Enqueue(squirrel);
            squirrel.SetActive(false);

            GameObject woodpecker = Instantiate(woodpeckerPrefab);
            availableWoodpeckers.Enqueue(woodpecker);
            woodpecker.SetActive(false);
            
            GameObject beaver = Instantiate(beaverPrefab);
            availableBeavers.Enqueue(beaver);
            beaver.SetActive(false);
            
            GameObject duck = Instantiate(duckPrefab);
            availableDucks.Enqueue(duck);
            duck.SetActive(false);
        }
    }

    public void AddAnimaltoPool(GameObject animal, AnimalTypes animalType)
    {
        switch (animalType)
        {
            case AnimalTypes.Squirrel:

                availableSquirrels.Enqueue(animal);
                animal.SetActive(false);

                break;

            case AnimalTypes.Woodpecker:

                availableWoodpeckers.Enqueue(animal);
                animal.SetActive(false);

                break;

            case AnimalTypes.Beaver:

                availableBeavers.Enqueue(animal);
                animal.SetActive(false);

                break;

            case AnimalTypes.Duck:

                availableDucks.Enqueue(animal);
                animal.SetActive(false);

                break;
        }
    }

    public GameObject SpawnAnimal(AnimalTypes animalType, Vector3 pos)
    {
        GameObject animal = null;

        switch (animalType)
        {
            case AnimalTypes.Squirrel:

                if (availableSquirrels.Count <= 0)
                {
                    CreatePools();
                }

                animal = availableSquirrels.Dequeue();
                animal.SetActive(true);
                animal.transform.position = pos;

                break;

            case AnimalTypes.Woodpecker:

                if (availableWoodpeckers.Count <= 0)
                {
                    CreatePools();
                }

                animal = availableWoodpeckers.Dequeue();
                animal.SetActive(true);
                animal.transform.position = pos;

                break;

            case AnimalTypes.Beaver:

                if (availableBeavers.Count <= 0)
                {
                    CreatePools();
                }

                animal = availableBeavers.Dequeue();
                animal.SetActive(true);
                animal.transform.position = pos;

                break;

            case AnimalTypes.Duck:

                if (availableDucks.Count <= 0)
                {
                    CreatePools();
                }

                animal = availableDucks.Dequeue();
                animal.SetActive(true);
                animal.transform.position = pos;

                break;
        }

        return animal;
    }
}
