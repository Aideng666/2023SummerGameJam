using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour, IInteractable
{
    [SerializeField] AnimalTypes animalType;
    [SerializeField] GameObject shelterPrefab;
    [SerializeField] protected float moveSpeed = 5;

    Camera cam;
    protected CharacterController characterController;
    protected Vector3 moveDir = Vector3.zero;
    private bool isBuildingShelter;

    public GameObject placeableShelter { get; private set; }
    public bool IsBuildingShelter { get { return isBuildingShelter; } set { isBuildingShelter = value; placeableShelter = Instantiate(shelterPrefab); } }
    public bool isActiveAnimal { get; set; }

    public bool isRecruited { get; set; } = false;

    LayerMask defaultLayer;
    LayerMask interactableLayer;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        characterController = GetComponent<CharacterController>();
        cam = Camera.main;

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
                BuildShelter();
            }
            else if (InputManager.Instance.Move().magnitude > 0.1f)
            {
                Move();

                transform.forward = moveDir;
            }
        }
        else
        {
            gameObject.layer = interactableLayer;
        }
    }

    void Move()
    {
        //Gets movement direction based on input and camera rotation
        moveDir = new Vector3(InputManager.Instance.Move().x, 0, InputManager.Instance.Move().y).normalized;
        moveDir = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y, 0) * moveDir;
    }

    public bool Interact(Interactor interactor)
    {
        if (!CanInteract())
        {
            return false;
        }

        if (!isRecruited)
        {
            CommunityManager.Instance.RecruitAnimal(this, animalType);
        }
        else
        {
            CommunityManager.Instance.SwapAnimals(this);
        }

        return true;
    }

    public void BuildShelter()
    {
        Ray ray = new Ray(CommunityManager.Instance.ShelterCam.transform.position, CommunityManager.Instance.ShelterCam.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            placeableShelter.transform.position = hit.point;
            //placeableShelter.transform.rotation = Quaternion.Euler(placeableShelter.transform.rotation.eulerAngles.x, CommunityManager.Instance.ShelterCam.transform.rotation.eulerAngles.y, placeableShelter.transform.rotation.eulerAngles.z);
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
        if (InputManager.Instance.Interact())
        {
            CommunityManager.Instance.CancelBuild();
        }
    }

    public bool CanInteract()
    {
        return true;
    }
}

public enum AnimalTypes
{
    Squirrel,
    Woodpecker,
    Beaver,
    Duck
}
