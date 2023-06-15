using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InteractionPromptUI : MonoBehaviour
{
    private Camera _mainCam;
    [SerializeField] private GameObject _uiPanelInteract;
    private UnityEventQueueSystem controllerEvent;
    private PlayerInput playerInput;
    private string scheme;
    public GameObject armature;

    public Sprite KeyboardInteract;
    public Sprite GamepadInteract;

    private void Start()
    {
        _mainCam = Camera.main;
        _uiPanelInteract.SetActive(false);
        playerInput = armature.GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            scheme = playerInput.currentControlScheme;
            Debug.Log("Scheme is " + scheme);
        }
        else Debug.Log("No input detected");
    }

    void LateUpdate()
    {
        var rotation = _mainCam.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }

    public bool isDisplayed = false;

    public void SetUpInteract()
    {
        //UpdateInteract();
        _uiPanelInteract.SetActive(true);
        isDisplayed = true;
    }

    public void CloseInteract()
    {
        _uiPanelInteract.SetActive(false);
        isDisplayed = false;
    }

    //TODO: Add controller support for UI
    //public void UpdateInteract()
    //{
    //    playerInput = armature.GetComponent<PlayerInput>();
    //    Debug.Log("Scheme is " + playerInput.currentControlScheme);
    //    if (string.Equals(playerInput.currentControlScheme, "KeyboardMouse"))
    //    {
    //        _uiPanelInteract.GetComponent<Image>().sprite = KeyboardInteract;
    //    }
    //    else _uiPanelInteract.GetComponent<Image>().sprite = GamepadInteract;
    //}
}
