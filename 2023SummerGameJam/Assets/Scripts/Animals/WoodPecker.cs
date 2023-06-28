using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodPecker : Animal
{
    [SerializeField] float flySpeed = 8f;
    [SerializeField] float verticalFlightSpeed = 3f;
    [SerializeField] float maxFlightStamina = 10f;

    float currentFlightStamina;
    bool flightActivated = false;

    protected override void Start()
    {
        base.Start();

        currentFlightStamina = maxFlightStamina;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (IsActiveAnimal)
        {
            //Detects if the bird is flying or not
            if (flightActivated && characterController.isGrounded)
            {
                flightActivated = false;

                animator.SetBool("Flying", false);
            }

            if (!flightActivated && InputManager.Instance.Fly() > 0 && currentFlightStamina > 0)
            {
                flightActivated = true;

                animator.SetBool("Flying", true);
            }

            //Moves horiztonally based on if the bird is in flight or not
            if (!flightActivated)
            {
                characterController.SimpleMove(moveDir * moveSpeed);
            }
            else
            {
                characterController.Move(moveDir * flySpeed * Time.deltaTime);
            }

            Fly();
            DepleteStamina();
            GainStamina();
        }
    }

    void Fly()
    {
        if (currentFlightStamina > 0)
        {
            //Flies up and down
            if (InputManager.Instance.Fly() > 0)
            {
                characterController.Move(Vector3.up * verticalFlightSpeed * Time.deltaTime);
            }
            else if (InputManager.Instance.Fly() < 0)
            {
                characterController.Move(Vector3.down * verticalFlightSpeed * Time.deltaTime);
            }
        }
    }

    //Depletes stamina over time while the bird is in flight
    void DepleteStamina()
    {
        if (flightActivated && !characterController.isGrounded)
        {
            currentFlightStamina -= Time.deltaTime;

            if (currentFlightStamina <= 0)
            {
                AlertSystem.Instance.CreateAlert("Out of Stamina!");
                StartCoroutine(BeginDescent());
            }
        }
    }

    //Recharges stamina when the bird is grounded
    void GainStamina()
    {
        if (!flightActivated && characterController.isGrounded)
        {
            currentFlightStamina += Time.deltaTime * 2;

            if (currentFlightStamina > maxFlightStamina)
            {
                currentFlightStamina = maxFlightStamina;
            }
        }
    }

    //If the bird runs out of stamina it will slowly descend to the ground to recharge
    IEnumerator BeginDescent()
    {
        characterController.Move(Vector3.down * verticalFlightSpeed * Time.deltaTime);

        yield return null;
    }
}
