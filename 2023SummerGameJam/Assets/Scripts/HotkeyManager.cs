using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class HotkeyManager : MonoBehaviour
{
    public GameObject[] HotkeyIcons;
    [SerializeField] bool[] active = new bool[4];

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

        active[0] = true;
        for (int i = 1; i < 4; i++)
        {
            HotkeyIcons[i].transform.localScale = new Vector3(0f, 0f, 0f);
        }
    }

    private void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            if (active[i])
            {
                int numAnimals = CommunityManager.Instance.animalsInCommunity[i].Count;
                HotkeyIcons[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().SetText($"x{numAnimals}");
            }
        }
    }

    public void expandIcon(int index)
    {
        if (!active[index])
        {
            HotkeyIcons[index].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f).SetEase(Ease.InOutExpo);
            active[index] = true;
        }
    }

    public void collapseIcon(int index)
    {
        if (active[index])
        {
            HotkeyIcons[index].transform.DOScale(new Vector3(0f, 0f, 0f), 0.5f).SetEase(Ease.InOutExpo);
            active[index] = false;
        }
    }

}
