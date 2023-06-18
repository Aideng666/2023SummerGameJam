using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    int foodCount;
    int waterCount;
    int materialCount;
    int population;

    [SerializeField] TMP_Text foodText;
    [SerializeField] TMP_Text waterText;
    [SerializeField] TMP_Text materialText;
    [SerializeField] TMP_Text populationText;

    public GameObject amountTextPrefab, resourceInstance;
    public string textToDisplay;

    // Start is called before the first frame update
    void Start()
    {
        foodCount = 0;
        waterCount = 0;
        materialCount = 0;
        population = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.Jump())
        {
            GameObject amountTextInstance = Instantiate(amountTextPrefab, resourceInstance.transform);
            amountTextInstance.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(textToDisplay);
        }

        foodText.text = foodCount.ToString();
        waterText.text = waterCount.ToString();
        materialText.text = materialCount.ToString();
        populationText.text = population.ToString();
    }
}
