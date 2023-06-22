using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI resourceRequirement;
    private Canvas UICanvas;

    private void Start()
    {
        UICanvas = GetComponent<Canvas>();
        Deactivate();
    }

    public void Activate(int cost)
    {
        resourceRequirement.SetText(ResourceManager.woodPoints.ToString() + "/" + cost.ToString());
        UICanvas.enabled = true;
    }

    public void Deactivate() 
    {
        UICanvas.enabled = false;
    }
}
