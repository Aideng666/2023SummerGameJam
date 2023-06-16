using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squirrel : Animal
{
    protected override void Update()
    {
        base.Update();

        characterController.SimpleMove(moveDir * moveSpeed);
    }
}
