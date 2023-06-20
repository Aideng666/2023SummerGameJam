using UnityEngine;

public class Fruit : MonoBehaviour, IInteractable
{
    public int foodPoints;
    public bool status = true;

    public bool Interact(Interactor player)
    {
        if (!CanInteract()) return false;

        Debug.Log("Collected " + foodPoints + " fruit!");
        ResourceManager.addToFood(foodPoints);

        deplete();
        return true;
    }

    public void replenish()
    {
        if (!status)
        {
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<Collider>().enabled = true;
            gameObject.tag = "Fruit";
            status = true;
        }
    }

    public void deplete()
    {
        if (status)
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            gameObject.tag = "EmptyFruit";
            status = false;
        }
    }

    public bool CanInteract()
    {
        return true;
    }


    //Function for testing
    [ContextMenu("Limit Resources")]
    public void limit()
    {
        ResourceManager.limitResources(0.5f, 0.5f);
    }

    //Function for testing
    [ContextMenu("Replenish Resources")]
    public void spawn()
    {
        ResourceManager.replenishResources(0.5f);
    }
}
