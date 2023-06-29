using UnityEngine;

public class Wood : MonoBehaviour, IInteractable
{
    public int woodPoints;
    public bool status = true;

    public bool Interact(Interactor player)
    {
        if (!CanInteract()) return false;
        
        //Debug.Log("Collected " + woodPoints + " wood!");
        ResourceManager.addToWood(woodPoints);
        AudioManager.Instance.Play("PickupWood");

        deplete();
        return true;
    }

    public void replenish()
    {
        GetComponentInChildren<MeshRenderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
        gameObject.tag = "Wood";
        status = true;
    }

    public void deplete()
    {
        GetComponentInChildren<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        gameObject.tag = "EmptyWood";
        status = false;
    }

    public bool CanInteract()
    {
        return true;
    }
}
