using UnityEngine;

public static class ResourceManager
{
    [SerializeField] static GameObject[] fruit;
    [SerializeField] static GameObject[] wood;

    //Spawn rate of each (Non-independant probability, i.e. if p=0.4, then 0.6 of all fruit will spawn)
    public static float fruitProb, woodProb;
    public static void spawnResources(float fruitProb, float woodProb)
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
            Fruit tempFruit = fruit.GetComponent<Fruit>();
            if (tempFruit) tempFruit.status = false;
        }

        foreach (GameObject wood in woodSpawn)
        {
            wood.SetActive(false);
            Wood tempWood = wood.GetComponent<Wood>();
            if (tempWood) tempWood.status = false;
        }
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
