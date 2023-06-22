using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class AlertSystem : MonoBehaviour
{

    Sequence alertTween;
    float panelTweenTime = 0.8f;

    public float waitTime = 2.0f;
    [SerializeField] GameObject AlertBox;
    [SerializeField] TextMeshProUGUI AlertText;

    public static AlertSystem Instance { get; private set; }

    private void Awake()
    {
        AlertBox.transform.localScale = new Vector3(0f, 0f, 0f);
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void CreateAlert(string message)
    {
        alertTween = DOTween.Sequence();
        AlertText.SetText(message);
        alertTween.Append(AlertBox.transform.DOScale(new Vector3(1f, 1f, 1f), panelTweenTime).SetEase(Ease.InOutExpo))
        .AppendInterval(waitTime)
        .Append(AlertBox.transform.DOScale(new Vector3(0f, 0f, 0f), panelTweenTime).SetEase(Ease.InOutExpo));

    }
}
