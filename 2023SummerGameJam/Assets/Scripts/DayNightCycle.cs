using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class DayNightCycle : MonoBehaviour
{
    public Image mask;
    [SerializeField] float timeDuration = 0f;

    [SerializeField] TMP_Text dayCounter;
    [SerializeField] int dayNumber;

    Sequence dayAlertTween;
    [SerializeField] GameObject dayAlert;

    private void Start()
    {
        dayAlertTween = DOTween.Sequence();
        dayAlert.transform.localScale = new Vector3(0f, 0f, 0f);
        ShowDayAlert();
    }

    // Update is called once per frame
    void Update()
    {
        mask.fillAmount += 1.0f / timeDuration * Time.deltaTime;
        dayCounter.text = dayNumber.ToString();

        UpdateDayCounter();
    }

    public Tween PopupTween(GameObject alert)
    {
        return alert.transform.DOScale(new Vector3(1f, 1f, 1f), 1.5f).SetEase(Ease.InOutExpo);
    }

    public Tween PopoutTween(GameObject alert)
    {
        return alert.transform.DOScale(new Vector3(0f, 0f, 0f), 1.5f).SetEase(Ease.InOutExpo);
    }

    public void ShowDayAlert()
    {
        dayAlertTween.Append(PopupTween(dayAlert)).AppendInterval(1f).Append(PopoutTween(dayAlert));
    }

    public void UpdateDayCounter()
    {    
        if (mask.fillAmount >= 1f)
        {
            dayNumber++;
            mask.fillAmount = 0f;
        }
    }
}
