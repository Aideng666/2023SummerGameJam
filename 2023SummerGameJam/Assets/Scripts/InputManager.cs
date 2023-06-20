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
    InputAction animal1Action;
    InputAction animal2Action;
    InputAction animal3Action;
    InputAction animal4Action;
    InputAction buildAction;
    InputAction rotateBuildAction;
    InputAction pauseAction;

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
        animal1Action = playerInput.actions["Animal1"];
        animal2Action = playerInput.actions["Animal2"];
        animal3Action = playerInput.actions["Animal3"];
        animal4Action = playerInput.actions["Animal4"];
        buildAction = playerInput.actions["Build"];
        rotateBuildAction = playerInput.actions["RotateBuild"];
        pauseAction = playerInput.actions["Pause"];
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
    public bool SelectAnimal1()
    {
        return animal1Action.triggered;
    }

    public bool SelectAnimal2()
    {
        return animal2Action.triggered;
    }

    public bool SelectAnimal3()
    {
        return animal3Action.triggered;
    }

    public bool SelectAnimal4()
    {
        return animal4Action.triggered;
    }

    public bool Build()
    {
        return buildAction.triggered;
    }

    public float RotateBuild()
    {
        return rotateBuildAction.ReadValue<float>();
    }

    public bool Pause()
    {
        return pauseAction.triggered;
    }
}