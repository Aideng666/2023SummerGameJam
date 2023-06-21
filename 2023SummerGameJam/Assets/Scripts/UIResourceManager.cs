using UnityEngine;
using TMPro;
using static ResourceManager;

public class UIResourceManager : MonoBehaviour
{
    int population;

    [SerializeField] TMP_Text foodText;
    [SerializeField] TMP_Text waterText;
    [SerializeField] TMP_Text materialText;
    [SerializeField] TMP_Text populationText;

    public GameObject amountTextPrefab, foodResourceInstance, woodResourceInstance, popInstance;

    // Start is called before the first frame update
    void Start()
    {
        population = 0;
    }

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
        string textToDisplay = "+" + amount.ToString();
        GameObject amountTextInstance = Instantiate(amountTextPrefab, textSpawn.transform);
        amountTextInstance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(textToDisplay);
    }
}
