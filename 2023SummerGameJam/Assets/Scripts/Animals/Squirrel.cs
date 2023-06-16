using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squirrel : Animal
{
    protected override void Update()
    {
        base.Update();

        characterController.SimpleMove(moveDir * moveSpeed);

        Shoot();

        if (Input.GetKey("w") & TouchingWall == true)
        {
            transform.position += Vector3.up * Time.deltaTime * UpwardSpeed;
            TouchingWall = false;
        }

        if (Input.GetKeyUp("w"))
        {
            TouchingWall = false;
        }
    }

    public float open = 100f;
    public float range = 1f;
    public bool TouchingWall = false;
    public float UpwardSpeed = 3.3f;
    public Transform Head;

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(Head.position, Head.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Collider target = hit.transform.GetComponent<Collider>();
            if (target != null)
            {
                TouchingWall = true;
            }
        }
    }
}
