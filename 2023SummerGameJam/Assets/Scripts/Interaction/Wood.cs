using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour, IInteractable
{
    public int woodPoints;
    public bool status = true;

    public bool Interact(Interactor player)
    {
        if (!CanInteract()) return false;
        gameObject.SetActive(false);
        Debug.Log("Collected " + woodPoints + " wood!");
        return true;
    }

    public bool CanInteract()
    {
        return true;
    }
}
