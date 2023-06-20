using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIResourceManager : MonoBehaviour
{
    int foodCount;
    int materialCount;
    int population;

    [SerializeField] TMP_Text foodText;
    [SerializeField] TMP_Text materialText;
    [SerializeField] TMP_Text populationText;

    public GameObject amountTextPrefab, resourceInstance;
    public string textToDisplay;

    // Start is called before the first frame update
    void Start()
    {
        foodCount = 0;
        materialCount = 0;
        population = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.Jump())
        {
            GameObject amountTextInstance = Instantiate(amountTextPrefab, resourceInstance.transform);
            amountTextInstance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(textToDisplay);
        }

        foodText.text = foodCount.ToString();
        materialText.text = materialCount.ToString();
        populationText.text = population.ToString();
    }
}
