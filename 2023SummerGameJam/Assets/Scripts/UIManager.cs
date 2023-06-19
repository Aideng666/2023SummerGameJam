using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] List<GameObject> buttons = new List<GameObject>();
    [SerializeField] List<GameObject> canvases = new List<GameObject>();
    Tween settingsCanvasTween;

    // Start is called before the first frame update
    void Start()
    {
        ButtonAnim();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ButtonAnim()
    {
        Sequence sequence = DOTween.Sequence();

        sequence
            .Append(buttons[0].transform.DOMoveX(1003, 0.6f).SetEase(Ease.InOutExpo))
            .Append(buttons[1].transform.DOMoveX(1003, 0.6f).SetEase(Ease.InOutExpo))
            .Append(buttons[2].transform.DOMoveX(1003, 0.6f).SetEase(Ease.InOutExpo))
            .Append(buttons[3].transform.DOMoveX(1003, 0.6f).SetEase(Ease.InOutExpo));     
    }

    public void SettingsButton()
    {     
        settingsCanvasTween = canvases[0].transform.DOScale(new Vector3(0f, 0f, 0f), 0.8f).SetEase(Ease.InOutExpo);
    }
}
