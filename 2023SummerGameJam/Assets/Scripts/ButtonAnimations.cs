using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonAnimations : MonoBehaviour
{
    [SerializeField] GameObject gameTitle;
    [SerializeField] float zRot = 0f;
    Sequence gameTitleSequence;

    // Start is called before the first frame update
    void Start()
    {
        if (gameTitle != null)
        {
            gameTitleSequence = DOTween.Sequence();
            gameTitleSequence.Append(gameTitle.transform.DORotate(new Vector3(0f, 0f, zRot), 1).SetEase(Ease.InOutExpo))
                .Append(gameTitle.transform.DORotate(new Vector3(0f, 0f, -zRot), 1).SetEase(Ease.InOutExpo))
                .SetLoops(-1, LoopType.Yoyo);
        }
    }

    public void ButtonHover(GameObject button)
    {     
        Tween buttonTween;

        buttonTween = button.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f).SetEase(Ease.InOutExpo);
    }

    public void ButtonExit(GameObject button)
    {
        Tween buttonTween;

        buttonTween = button.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f).SetEase(Ease.InOutExpo);
    }

    public void ButtonClick(GameObject button)
    {
        button.transform.localScale = (new Vector3(1f, 1f, 1f));
    }
}
