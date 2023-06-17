using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour, IInteractable
{
    public int foodPoints;

    public bool Interact(Interactor player)
    {
        if (!CanInteract()) return false;
        gameObject.SetActive(false);
        Debug.Log("Collected " + foodPoints + " fruit!");
        return true;
    }

    public bool CanInteract()
    {
        return true;
    }
}
