using UnityEngine;
using TMPro;
using static ResourceManager;

public class UIResourceManager : MonoBehaviour
{

    [SerializeField] TMP_Text foodText;
    [SerializeField] TMP_Text waterText;
    [SerializeField] TMP_Text materialText;
    [SerializeField] TMP_Text populationText;

    public GameObject amountTextPrefab, foodResourceInstance, woodResourceInstance, popInstance;


    // Update is called once per frame
    void Update()
    {
        foodText.text = fruitPoints.ToString();
        materialText.text = woodPoints.ToString();
        populationText.text = population.ToString();
    }

    public void addFoodUI(int amount)
    {
        addResourceUI(foodResourceInstance, amount);
    }

    public void addWoodUI(int amount)
    {
        addResourceUI(woodResourceInstance, amount);
    }

    public void addPopUI(int amount)
    {
        addResourceUI(popInstance, amount);
    }
    private void addResourceUI(GameObject textSpawn, int amount)
    {
        string textToDisplay;
        if (amount == 0) return;
        else if (amount > 0) textToDisplay = "+" + amount.ToString();
        else textToDisplay = "-" + amount.ToString();

        GameObject amountTextInstance = Instantiate(amountTextPrefab, textSpawn.transform);
        amountTextInstance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(textToDisplay);
    }
}
