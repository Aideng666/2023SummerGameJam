using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerInput playerInput;

    //Gameplay Controls
    InputAction interactAction;
    InputAction moveAction;
    InputAction jumpAction;
    InputAction flyAction;

    public bool interact = false;

    public static InputManager Instance { get; set; }
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

        //controls = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();

        interactAction = playerInput.actions["Interact"];
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        flyAction = playerInput.actions["Fly"];
    }

    public PlayerInput GetPlayerInput()
    {
        return playerInput;
    }

    public Vector2 Move()
    {
        return moveAction.ReadValue<Vector2>();
    }

    public float Fly()
    {
        return flyAction.ReadValue<float>();
    }

    public bool Interact()
    {
        return interactAction.triggered;
    }

    public bool Jump()
    {
        return jumpAction.triggered;
    }
}