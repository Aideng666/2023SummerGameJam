using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HotkeyManager : MonoBehaviour
{
    public GameObject[] HotkeyIcons;

    public static HotkeyManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        for (int i = 1; i < 4; i++)
        {
            HotkeyIcons[i].transform.localScale = new Vector3(0f, 0f, 0f);
        }
    }

    public void expandIcon(int index)
    {
        HotkeyIcons[index].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f).SetEase(Ease.InOutExpo);
    }

    public void collapseIcon(int index)
    {
        HotkeyIcons[index].transform.DOScale(new Vector3(0f, 0f, 0f), 0.5f).SetEase(Ease.InOutExpo);
    }

}
