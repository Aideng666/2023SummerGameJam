using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour, IInteractable
{
    public int foodPoints;
    public bool status = true;

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

    [ContextMenu("Spawn Resources")]
    public void spawn()
    {
        ResourceManager.spawnResources(0.5f, 0.5f);
    }
}
