using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beaver : Animal
{
    protected override void Update()
    {
        base.Update();

        if (isActiveAnimal)
        {
            characterController.SimpleMove(moveDir * moveSpeed);
        }
    }

    void Swim()
    {

    }
}
