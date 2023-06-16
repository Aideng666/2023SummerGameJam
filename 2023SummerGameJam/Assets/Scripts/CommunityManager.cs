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

    private void Update()
    {
        SwapAnimals();
    }

    void SwapAnimals()
    {
        //Checks each hotkey and selects the closest animal of the chosen kind
        if (InputManager.Instance.SelectAnimal1())
        {
            if (animalsInCommunity[0].Count > 0)
            {
                Animal closestAnimal = animalsInCommunity[0][0];

                foreach (Animal animal in animalsInCommunity[0])
                {
                    if (Vector3.Distance(animal.transform.position, activeAnimal.transform.position) < Vector3.Distance(closestAnimal.transform.position, activeAnimal.transform.position))
                    {
                        closestAnimal = animal;
                    }
                }

                activeAnimal.isActiveAnimal = false;
                activeAnimal = closestAnimal;
                activeAnimal.isActiveAnimal = true;

                GameManager.Instance.UpdateCameraTarget();
            }
        }
        if (InputManager.Instance.SelectAnimal2())
        {
            if (animalsInCommunity[1].Count > 0)
            {
                Animal closestAnimal = animalsInCommunity[1][0];

                foreach (Animal animal in animalsInCommunity[1])
                {
                    if (Vector3.Distance(animal.transform.position, activeAnimal.transform.position) < Vector3.Distance(closestAnimal.transform.position, activeAnimal.transform.position))
                    {
                        closestAnimal = animal;
                    }
                }

                activeAnimal.isActiveAnimal = false;
                activeAnimal = closestAnimal;
                activeAnimal.isActiveAnimal = true;

                GameManager.Instance.UpdateCameraTarget();
            }
        }
        if (InputManager.Instance.SelectAnimal3())
        {
            if (animalsInCommunity[2].Count > 0)
            {
                Animal closestAnimal = animalsInCommunity[2][0];

                foreach (Animal animal in animalsInCommunity[2])
                {
                    if (Vector3.Distance(animal.transform.position, activeAnimal.transform.position) < Vector3.Distance(closestAnimal.transform.position, activeAnimal.transform.position))
                    {
                        closestAnimal = animal;
                    }
                }

                activeAnimal.isActiveAnimal = false;
                activeAnimal = closestAnimal;
                activeAnimal.isActiveAnimal = true;

                GameManager.Instance.UpdateCameraTarget();
            }
        }
        if (InputManager.Instance.SelectAnimal4())
        {
            if (animalsInCommunity[3].Count > 0)
            {
                Animal closestAnimal = animalsInCommunity[3][0];

                foreach (Animal animal in animalsInCommunity[3])
                {
                    if (Vector3.Distance(animal.transform.position, activeAnimal.transform.position) < Vector3.Distance(closestAnimal.transform.position, activeAnimal.transform.position))
                    {
                        closestAnimal = animal;
                    }
                }

                activeAnimal.isActiveAnimal = false;
                activeAnimal = closestAnimal;
                activeAnimal.isActiveAnimal = true;

                GameManager.Instance.UpdateCameraTarget();
            }
        }
    }
    public void SwapAnimals(Animal animal)
    {
        activeAnimal.isActiveAnimal = false;
        activeAnimal = animal;
        activeAnimal.isActiveAnimal = true;

        GameManager.Instance.UpdateCameraTarget();
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
