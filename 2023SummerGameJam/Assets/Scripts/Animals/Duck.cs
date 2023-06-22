using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Duck : Animal
{
    [SerializeField] InputAction quackAction;

    private void OnEnable()
    {
        quackAction.Enable();
    }

    private void OnDisable()
    {
        quackAction.Disable();
    }

    protected override void Update()
    {
        base.Update();

        if (IsActiveAnimal)
        {
            characterController.SimpleMove(moveDir * moveSpeed);

            if (quackAction.triggered)
            {
                Quack();
            }
        }
    }

    void Quack()
    {
        print("Quack");

        //maybe add a quack sound effect or a little cute animation for fun
        int quackChoice = Random.Range(0, 2);

        if (quackChoice == 0)
        {
            AudioManager.Instance.Play("Quack1");
        }
        else
        {
            AudioManager.Instance.Play("Quack2");
        }
    }
}
