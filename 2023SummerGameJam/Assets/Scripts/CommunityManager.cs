using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CommunityManager : MonoBehaviour
{
    [SerializeField] Transform communityArea;
    [SerializeField] Animal startingAnimal;
    [SerializeField] Camera mainCam;
    [SerializeField] Camera shelterCam;
    [SerializeField] CinemachineFreeLook virtualCam;
    [SerializeField] float communityRadius = 20;
    [SerializeField] float camMovespeed = 5;
    [SerializeField] float dayLength = 600f; // in seconds
    [SerializeField] TextMeshProUGUI dayCounter;
    [SerializeField] public BuildUI buildUI;
    [HideInInspector] public Animal activeAnimal;
    [SerializeField] GameObject deathScreenCanvas;
    [SerializeField] GameObject mainMenuCanvas;

    public event Action OnDayChange;

    Vector3 startShelterCamPos;
    float elaspedDayTime = 0;

    public bool backHomeForMorning { get; set; }
    public float dayNum { get; private set; } = 0;
    public float DayLength { get { return dayLength; } }
    public float ElasedDayTime { get { return elaspedDayTime; } }
    public Camera ShelterCam { get { return shelterCam; } }
    public Camera MainCam { get { return mainCam; } }
    public Transform CommunityArea { get { return communityArea; } }
    public float CommunityRadius { get { return communityRadius; } }
    public int totalAnimalsInCommunity { get { return animalsInCommunity[0].Count + animalsInCommunity[1].Count + animalsInCommunity[2].Count + animalsInCommunity[3].Count; } }

    //0 = Squirrel
    //1 = Woodpecker
    //2 = beaver
    //3 = Duck
    public List<Animal>[] animalsInCommunity = new List<Animal>[4] { new List<Animal>(), new List<Animal>(), new List<Animal>(), new List<Animal>() };
    public Dictionary<AnimalTypes, int> shelters = new Dictionary<AnimalTypes, int>();
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
        activeAnimal.IsActiveAnimal = true;
        activeAnimal.isRecruited = true;

        shelterCam.enabled = false;

        shelters.Add(AnimalTypes.Squirrel, 0);
        shelters.Add(AnimalTypes.Woodpecker, 0);
        shelters.Add(AnimalTypes.Beaver, 0);
        shelters.Add(AnimalTypes.Duck, 0);

        AlertSystem.Instance.CreateAlert("Welcome to Backyard Builders! Collect resources, build shelters and recruit animals to join your community.");
    }

    private void Update()
    {
        //Lose Condition
        if (totalAnimalsInCommunity <= 0)
        {
            //YOU LOSE
            DeathScreen();
        }

        //Switching Days
        if (elaspedDayTime >= dayLength)
        {
            backHomeForMorning = false;

            elaspedDayTime = 0;

            NightTimeEvents.PickNightTimeEvent();

            ResourceManager.replenishResources(0.7f);
            DepleteFood();

            dayNum++;
            dayCounter.SetText((dayNum + 1).ToString());

            OnDayChange.Invoke();
        }

        if (!activeAnimal.IsBuildingShelter)
        {
            SwapAnimals();

            if (InputManager.Instance.Build())
            {
                activeAnimal.IsBuildingShelter = true;

                SwapCamera();

                virtualCam.enabled = false;
                startShelterCamPos = shelterCam.transform.position;
            }
        } 
        else
        {
            Vector2 moveInput = InputManager.Instance.Move();

            shelterCam.transform.position += Quaternion.Euler(0, shelterCam.transform.rotation.eulerAngles.y, 0) * (new Vector3(moveInput.x, 0, moveInput.y) * camMovespeed * Time.deltaTime);

            if (InputManager.Instance.Build())
            {
                Destroy(activeAnimal.placeableShelter);
                CancelBuild();
            }
        }

        elaspedDayTime += Time.deltaTime;
    }
    void DepleteFood()
    {
        AlertSystem.Instance.CreateAlert($"Your animals ate {2 * totalAnimalsInCommunity} food during the night");

        ResourceManager.fruitPoints -= 2 * totalAnimalsInCommunity;

        //If food ran out, animals will starve
        if (ResourceManager.fruitPoints < 0)
        {
            int numAnimalsStarved = ResourceManager.fruitPoints / -2;

            for (int i = 0; i < numAnimalsStarved; i++)
            {
                int animalChoice = UnityEngine.Random.Range(0, 4);

                if (animalsInCommunity[animalChoice].Count > 0)
                {
                    Animal killedAnimal = animalsInCommunity[animalChoice][0];

                    killedAnimal.Die();
                }
                else
                {
                    i--;
                }
            }

            ResourceManager.fruitPoints = 0;
        }
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

                activeAnimal.IsActiveAnimal = false;
                activeAnimal = closestAnimal;
                activeAnimal.IsActiveAnimal = true;

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

                activeAnimal.IsActiveAnimal = false;
                activeAnimal = closestAnimal;
                activeAnimal.IsActiveAnimal = true;

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

                activeAnimal.IsActiveAnimal = false;
                activeAnimal = closestAnimal;
                activeAnimal.IsActiveAnimal = true;
                    
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

                activeAnimal.IsActiveAnimal = false;
                activeAnimal = closestAnimal;
                activeAnimal.IsActiveAnimal = true;

                GameManager.Instance.UpdateCameraTarget();
            }
        }
    }
    public void SwapAnimals(Animal animal)
    {
        activeAnimal.IsActiveAnimal = false;
        activeAnimal = animal;
        activeAnimal.IsActiveAnimal = true;

        GameManager.Instance.UpdateCameraTarget();
    }

    void SwapCamera()
    {
        shelterCam.enabled = !shelterCam.enabled;
        mainCam.enabled = !mainCam.enabled;
    }

    public void CancelBuild()
    {
        if (activeAnimal.IsBuildingShelter)
        {
            activeAnimal.IsBuildingShelter = false;
            shelterCam.transform.position = startShelterCamPos;
            virtualCam.enabled = true;
            buildUI.Deactivate();
            SwapCamera();
        }
    }

    public void RecruitAnimal(Animal animal, AnimalTypes animalType)
    {
        switch (animalType)
        {
            case AnimalTypes.Squirrel:

                animalsInCommunity[0].Add(animal);
                HotkeyManager.Instance.expandIcon(0);

                break;

            case AnimalTypes.Woodpecker:

                animalsInCommunity[1].Add(animal);
                HotkeyManager.Instance.expandIcon(1);

                break;

            case AnimalTypes.Beaver:

                animalsInCommunity[2].Add(animal);
                HotkeyManager.Instance.expandIcon(2);

                break;

            case AnimalTypes.Duck:

                animalsInCommunity[3].Add(animal);
                HotkeyManager.Instance.expandIcon(3);

                break;
        }
        ResourceManager.addToPop(1);
        animal.isRecruited = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(communityArea.position, communityRadius);
    }

    public void DeathScreen()
    {
        mainMenuCanvas.SetActive(false);
        deathScreenCanvas.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
