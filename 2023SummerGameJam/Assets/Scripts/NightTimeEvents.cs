using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NightTimeEvents
{
    static Vector2 populationKillPercentageRange = new Vector2(0, 0.2f);
    static Vector2 foodStealPercentageRange = new Vector2(0.1f, 0.4f);
    static Vector2 shelterDestructionRange = new Vector2(0, 0.2f);

    public static void PickNightTimeEvent()
    {
        int choice = Random.Range(0, 4);

        switch (choice)
        {
            case 0:

                CoyoteAttackEvent();

                break;

            case 1:

                FoodRaidEvent();

                break;

            case 2:

                BadWeatherEvent();

                break;

            case 3:

                LuckyNight();

                break;
        }
    }

    static void CoyoteAttackEvent()
    {
        float percentPopulationToKill = Random.Range(populationKillPercentageRange.x, populationKillPercentageRange.y);

        int totalAnimalsInPopulation = CommunityManager.Instance.animalsInCommunity[0].Count + CommunityManager.Instance.animalsInCommunity[1].Count + CommunityManager.Instance.animalsInCommunity[2].Count + CommunityManager.Instance.animalsInCommunity[3].Count;

        int numKilledAnimals = (int)(totalAnimalsInPopulation * percentPopulationToKill);

        if (numKilledAnimals == 0)
        {
            numKilledAnimals = 1;
        }

        for (int i = 0; i < numKilledAnimals; i++)
        {
            int animalChoice = Random.Range(0, 4);

            if (CommunityManager.Instance.animalsInCommunity[animalChoice].Count > 0)
            {
                Animal killedAnimal = CommunityManager.Instance.animalsInCommunity[animalChoice][0];

                killedAnimal.Die();
            }
            else
            {
                i--;
            }
        }

        Debug.Log($"Coyotes attacked your community! You lost {numKilledAnimals} animals!");
    }

    static void FoodRaidEvent()
    {
        float percentFoodToSteal = Random.Range(foodStealPercentageRange.x, foodStealPercentageRange.y);

        int amountOfFoodStolen = (int)(percentFoodToSteal * ResourceManager.fruitPoints);

        ResourceManager.fruitPoints -= amountOfFoodStolen;

        Debug.Log($"Owls have raided your food storage! You lost {amountOfFoodStolen} food");
    }

    static void BadWeatherEvent()
    {
        float percentSheltersToDestroy = Random.Range(shelterDestructionRange.x, shelterDestructionRange.y);

        int totalShelters = CommunityManager.Instance.shelters[AnimalTypes.Squirrel] + CommunityManager.Instance.shelters[AnimalTypes.Beaver] + CommunityManager.Instance.shelters[AnimalTypes.Woodpecker] + CommunityManager.Instance.shelters[AnimalTypes.Duck];

        int numSheltersDestroyed = (int)(totalShelters * percentSheltersToDestroy);

        for (int i = 0; i < numSheltersDestroyed; i++)
        {
            foreach (Shelter shelter in GameObject.FindObjectsOfType<Shelter>())
            {
                if (CommunityManager.Instance.shelters[shelter.ShelterType] > 0)
                {
                    CommunityManager.Instance.shelters[shelter.ShelterType] -= 1;
                    GameObject.Destroy(shelter.gameObject);

                    break;
                }
            }
        }

        Debug.Log($"A bad storm occured! {numSheltersDestroyed} of your shelters have been destroyed!");
    }

    static void LuckyNight()
    {
        Debug.Log("Nothing bad happened overnight!");
    }
}
