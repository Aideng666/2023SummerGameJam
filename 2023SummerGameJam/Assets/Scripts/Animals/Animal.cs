using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour, IInteractable
{
    [SerializeField] AnimalTypes animalType;
    [SerializeField] GameObject shelterPrefab;
    [SerializeField] int shelterCost = 10;
    [SerializeField] protected float moveSpeed = 5;
    [SerializeField] protected bool canSwim;

    Camera cam;
    protected Animator animator;
    protected CharacterController characterController;
    protected Vector3 moveDir = Vector3.zero;
    private bool isBuildingShelter;
    private bool isActiveAnimal;

    public GameObject placeableShelter { get; private set; }
    public bool IsBuildingShelter { get { return isBuildingShelter; } set { isBuildingShelter = value; if (value) placeableShelter = Instantiate(shelterPrefab); } }
    public bool IsActiveAnimal { get { return isActiveAnimal;  } set { isActiveAnimal = value; if (!value) elaspedInteractionDelay = 0; } }
    public bool isRecruited { get; set; } = false;
    public AnimalTypes AnimalType { get { return animalType; } }

    LayerMask defaultLayer;
    LayerMask interactableLayer;

    //For AutoMove when animals are not active
    Vector3 chosenDirection = Vector3.zero;
    float moveTime = 1f;
    float elaspedMoveTime = 0;
    bool isMoving = false;

    //Interaction timer
    float interactionDelay = 0.25f;
    float elaspedInteractionDelay = 0f;


    private void OnEnable()
    {
        isRecruited = false;
        isActiveAnimal = false;

        CommunityManager.Instance.OnDayChange += TeleportAllHome;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        characterController = GetComponent<CharacterController>();
        cam = Camera.main;
        animator = GetComponentInChildren<Animator>();

        defaultLayer = LayerMask.NameToLayer("Default");
        interactableLayer = LayerMask.NameToLayer("Interactable");
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (isActiveAnimal)
        {
            gameObject.layer = defaultLayer;
            moveDir = Vector3.zero;

            if (isBuildingShelter)
            {
                animator?.SetBool("Running", false);

                BuildShelter();
            }
            else if (InputManager.Instance.Move().magnitude > 0.1f)
            {
                Move();

                transform.forward = moveDir;

                animator?.SetBool("Running", true);
            }
            else
            {
                animator?.SetBool("Running", false);
            }
        }
        else
        {
            gameObject.layer = interactableLayer;

            AutoMove();

            elaspedInteractionDelay += Time.deltaTime;
        }

    }

    void Move()
    {
        //Gets movement direction based on input and camera rotation
        moveDir = new Vector3(InputManager.Instance.Move().x, 0, InputManager.Instance.Move().y).normalized;
        moveDir = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y, 0) * moveDir;
    }

    void AutoMove()
    {
        if (elaspedMoveTime >= moveTime)
        {
            isMoving = !isMoving;
            elaspedMoveTime = 0;

            if (isMoving)
            {
                chosenDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
                transform.forward = chosenDirection;
            }
        }

        if (isMoving)
        {
            characterController.SimpleMove(chosenDirection * moveSpeed);

            animator?.SetBool("Running", true);
        }
        else
        {
            //Still use the move function so gravity gets applied
            characterController.SimpleMove(chosenDirection * 0);

            animator?.SetBool("Running", false);
        }

        elaspedMoveTime += Time.deltaTime;
    }

    public bool Interact(Interactor interactor)
    {
        if (!CanInteract())
        {
            return false;
        }

        print("Interacting");

        if (!isRecruited && CommunityManager.Instance.shelters[animalType] >= CommunityManager.Instance.animalsInCommunity[(int)animalType].Count)
        {
            CommunityManager.Instance.RecruitAnimal(this, animalType);
        }
        else if (!isRecruited)
        {
            print($"Not Enough Shelters to recruit this animal, build more {animalType} shelters first");
            AlertSystem.Instance.CreateAlert("Not Enough Shelters to recruit this animal");
        }
        else
        {
            print("Swapping");
            CommunityManager.Instance.SwapAnimals(this);
        }

        return true;
    }

    public void BuildShelter()
    {
        Ray ray = new Ray(CommunityManager.Instance.ShelterCam.transform.position, CommunityManager.Instance.ShelterCam.transform.forward);
        RaycastHit hit;

        CommunityManager.Instance.buildUI.Activate(shelterCost);

        if (Physics.Raycast(ray, out hit))
        {
            placeableShelter.transform.position = hit.point;
        }

        if (InputManager.Instance.RotateBuild() < 0)
        {
            placeableShelter.transform.Rotate(Vector3.up, -1f);
        }
        else if (InputManager.Instance.RotateBuild() > 0)
        {
            placeableShelter.transform.Rotate(Vector3.up, 1f);
        }

        //Confirming Build
        if (InputManager.Instance.Interact() && ResourceManager.woodPoints >= shelterCost && Vector3.Distance(placeableShelter.transform.position, CommunityManager.Instance.CommunityArea.position) <= CommunityManager.Instance.CommunityRadius)
        {
            ResourceManager.woodPoints -= shelterCost;

            placeableShelter.GetComponent<Shelter>().ShelterType = animalType;

            placeableShelter.transform.GetChild(0).gameObject.SetActive(false);
            CommunityManager.Instance.shelters[animalType] += 1;
            CommunityManager.Instance.CancelBuild();
        }
        else if (InputManager.Instance.Interact())
        {
            if (ResourceManager.woodPoints < shelterCost)
            {
                print("Not enough wood to build shelter");
                AlertSystem.Instance.CreateAlert("Not enough wood to build shelter");
            }
            if (Vector3.Distance(placeableShelter.transform.position, CommunityManager.Instance.CommunityArea.position) > CommunityManager.Instance.CommunityRadius)
            {
                print("Cannot build shelter outside of community zone");
                AlertSystem.Instance.CreateAlert("Cannot build shelter outside of community zone");
            }
        }
    }

    public void Die(bool coyoteAttack = false)
    {
        if (IsActiveAnimal)
        {
            foreach (Animal animal in FindObjectsOfType<Animal>())
            {
                if (!animal.IsActiveAnimal && animal.isRecruited)
                {
                    CommunityManager.Instance.SwapAnimals(animal);

                    break;
                }
            }
        }

        AnimalPool.Instance.AddAnimaltoPool(gameObject, AnimalType);

        if (isRecruited)
        {
            if (coyoteAttack) AlertSystem.Instance.CreateAlert($"A Coyote has killed one of your animals!");
            CommunityManager.Instance.animalsInCommunity[(int)AnimalType].RemoveAt(0);
            if (CommunityManager.Instance.animalsInCommunity[(int)AnimalType].Count < 1)
            {
                HotkeyManager.Instance.collapseIcon((int)AnimalType);
            }

            ResourceManager.addToPop(-1);
        }
    }

    public bool CanInteract()
    {
        if (elaspedInteractionDelay > interactionDelay)
        {
            return true;
        }

        return false;
    }

    public void TeleportHome()
    {
        transform.position = CommunityManager.Instance.CommunityArea.position;
    }

    void TeleportAllHome()
    {
        if (isActiveAnimal)
        {
            print("Teleporting Home");
            TeleportHome();
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!canSwim && hit.gameObject.CompareTag("Water"))
        {
            TeleportHome();
        }
    }
}

public enum AnimalTypes
{
    Squirrel,
    Woodpecker,
    Beaver,
    Duck
}
