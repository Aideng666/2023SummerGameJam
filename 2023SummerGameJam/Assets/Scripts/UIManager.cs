using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] List<GameObject> buttons = new List<GameObject>();
    [SerializeField] List<GameObject> canvases = new List<GameObject>();
    Sequence sequence;
    Sequence settingsCanvasTween;
    Sequence creditsCanvasTween;
    Sequence exitCanvasTween;
    float panelTweenTime = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        sequence = DOTween.Sequence();
        settingsCanvasTween = DOTween.Sequence();
        creditsCanvasTween = DOTween.Sequence();
        exitCanvasTween = DOTween.Sequence();

        for (int i = 1; i < canvases.Count; ++i)
        {
            canvases[i].transform.localScale = new Vector3(0f, 0f, 0f);
        }

        ButtonAnim();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ButtonAnim()
    {    
        sequence
            .Append(buttons[0].transform.DOMoveX(1003, 0.5f).SetEase(Ease.InOutExpo))
            .Append(buttons[1].transform.DOMoveX(1003, 0.5f).SetEase(Ease.InOutExpo))
            .Append(buttons[2].transform.DOMoveX(1003, 0.5f).SetEase(Ease.InOutExpo))
            .Append(buttons[3].transform.DOMoveX(1003, 0.5f).SetEase(Ease.InOutExpo));     
    }

    public void SettingsCanvasIn()
    {     
        // scale out main menu canvas
        settingsCanvasTween.Append(canvases[0].transform.DOScale(new Vector3(0f, 0f, 0f), panelTweenTime).SetEase(Ease.InOutExpo))
        // scale in the settings canvas
            .Append(canvases[1].transform.DOScale(new Vector3(1f, 1f, 1f), panelTweenTime).SetEase(Ease.InOutExpo));
    }

    public void SettingsCanvasOut()
    {
        // scale out settings canvas
        settingsCanvasTween.Append(canvases[1].transform.DOScale(new Vector3(0f, 0f, 0f), panelTweenTime).SetEase(Ease.InOutExpo))
        // scale in the main menu canvas
            .Append(canvases[0].transform.DOScale(new Vector3(1f, 1f, 1f), panelTweenTime).SetEase(Ease.InOutExpo));
    }

    public void CreditsCanvasIn()
    {
        // scale out main menu canvas
        creditsCanvasTween.Append(canvases[0].transform.DOScale(new Vector3(0f, 0f, 0f), panelTweenTime).SetEase(Ease.InOutExpo))
        // scale in the credits canvas
            .Append(canvases[2].transform.DOScale(new Vector3(1f, 1f, 1f), panelTweenTime).SetEase(Ease.InOutExpo));
    }

    public void CreditsCanvasOut()
    {
        // scale out credits canvas
        creditsCanvasTween.Append(canvases[2].transform.DOScale(new Vector3(0f, 0f, 0f), panelTweenTime).SetEase(Ease.InOutExpo))
        // scale in the main menu canvas
            .Append(canvases[0].transform.DOScale(new Vector3(1f, 1f, 1f), panelTweenTime).SetEase(Ease.InOutExpo));
    }

    public void ExitCanvasIn()
    {
        // scale out main menu canvas
        exitCanvasTween.Append(canvases[0].transform.DOScale(new Vector3(0f, 0f, 0f), panelTweenTime).SetEase(Ease.InOutExpo))
        // scale in the exit canvas
            .Append(canvases[3].transform.DOScale(new Vector3(1f, 1f, 1f), panelTweenTime).SetEase(Ease.InOutExpo));
    }

    public void ExitCanvasOut()
    {
        // scale out exit canvas
        exitCanvasTween.Append(canvases[3].transform.DOScale(new Vector3(0f, 0f, 0f), panelTweenTime).SetEase(Ease.InOutExpo))
        // scale in the main menu canvas
            .Append(canvases[0].transform.DOScale(new Vector3(1f, 1f, 1f), panelTweenTime).SetEase(Ease.InOutExpo));
    }
}
