using UnityEngine;

public static class ResourceManager
{
    [SerializeField] static GameObject[] fruit;
    [SerializeField] static GameObject[] wood;

    public static int woodPoints = 0, fruitPoints = 0, population = 1;

    //Spawn rate of each (Non-independant probability, i.e. if p=0.4, then 0.6 of all fruit will spawn)
    public static float fruitProb, woodProb;

    static UIResourceManager UIResource;
    public static UIResourceManager Instance
    {
        get
        {
            if (UIResource == null)
            {
                // create or find object
                UIResource = GameObject.FindObjectOfType<UIResourceManager>();
            }
            return UIResource;
        }
    }
    public static void limitResources(float fruitProb, float woodProb)
    {
        if (fruit == null)
            fruit = GameObject.FindGameObjectsWithTag("Fruit");
        if (wood == null)
            wood = GameObject.FindGameObjectsWithTag("Wood");

        GameObject[] fruitSpawn = SelectRandom(fruit, fruitProb);
        GameObject[] woodSpawn = SelectRandom(wood, woodProb);

        foreach (GameObject fruit in fruitSpawn)
        {
            fruit.SetActive(false);
            fruit.tag = "EmptyFruit";
            Fruit tempFruit = fruit.GetComponent<Fruit>();
            if (tempFruit) tempFruit.status = false;
        }

        foreach (GameObject wood in woodSpawn)
        {
            wood.SetActive(false);
            wood.tag = "EmptyWood";
            Wood tempWood = wood.GetComponent<Wood>();
            if (tempWood) tempWood.status = false;
        }
    }

    public static void replenishResources(float percent)
    {
        GameObject[] emptyFruit = GameObject.FindGameObjectsWithTag("EmptyFruit");
        GameObject[] emptyWood = GameObject.FindGameObjectsWithTag("EmptyWood");

        GameObject[] fruitSpawn = SelectRandom(emptyFruit, percent);
        GameObject[] woodSpawn = SelectRandom(emptyWood, percent);

        foreach (GameObject fruit in fruitSpawn)
        {
            Fruit tempFruit = fruit.GetComponent<Fruit>();
            tempFruit.replenish();
        }

        foreach (GameObject wood in woodSpawn)
        {
            Wood tempWood = wood.GetComponent<Wood>();
            tempWood.replenish();
        }
    }
    public static void ResetResources()
    {
        woodPoints = 0;
        fruitPoints = 0;
        population = 1;
    }

    public static int addToWood(int amount)
    {
        woodPoints += amount;
        Instance.addWoodUI(amount);
        return woodPoints;
    }

    public static int addToFood(int amount)
    {
        fruitPoints += amount;
        Instance.addFoodUI(amount);
        return fruitPoints;
    }

    public static int addToPop(int amount)
    {
        population += amount;
        Instance.addPopUI(amount);
        return fruitPoints;
    }


    static GameObject[] SelectRandom(GameObject[] objectList, float fraction)
    {
        int numRequired = (int) (objectList.Length * fraction);

        GameObject[] result = new GameObject[numRequired];

        int numToChoose = numRequired;

        for (int numLeft = objectList.Length; numLeft > 0; numLeft--)
        {

            float prob = (float)numToChoose / (float)numLeft;

            if (Random.value <= prob)
            {
                numToChoose--;
                result[numToChoose] = objectList[numLeft - 1];

                if (numToChoose == 0)
                {
                    break;
                }
            }
        }
        return result;
    }
}
