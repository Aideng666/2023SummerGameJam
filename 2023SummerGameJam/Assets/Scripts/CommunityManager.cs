using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunityManager : MonoBehaviour
{
    [SerializeField] Animal startingAnimal;
    [HideInInspector] public Animal activeAnimal;

    //0 = Squirrel
    //1 = Woodpecker
    //2 = beaver
    //3 = Duck
    public List<Animal>[] animalsInCommunity = new List<Animal>[4] { new List<Animal>(), new List<Animal>(), new List<Animal>(), new List<Animal>() };
    public static CommunityManager Instance { get; private set; }

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

    // Start is called before the first frame update
    void Start()
    {
        animalsInCommunity[0].Add(startingAnimal);

        activeAnimal = startingAnimal;
        activeAnimal.isActiveAnimal = true;
        activeAnimal.isRecruited = true;
    }

    public void RecruitAnimal(Animal animal, AnimalTypes animalType)
    {
        switch (animalType)
        {
            case AnimalTypes.Squirrel:
                Debug.Log("Recruited new Squirrel");
                animalsInCommunity[0].Add(animal);

                break;

            case AnimalTypes.Woodpecker:
                Debug.Log("Recruited new Bird");
                animalsInCommunity[1].Add(animal);

                break;

            case AnimalTypes.Beaver:
                Debug.Log("Recruited new Beaver");
                animalsInCommunity[2].Add(animal);

                break;

            case AnimalTypes.Duck:
                Debug.Log("Recruited new Duck");
                animalsInCommunity[3].Add(animal);

                break;
        }

        animal.isRecruited = true;
    }
}
