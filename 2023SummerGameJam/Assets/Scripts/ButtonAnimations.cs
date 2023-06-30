using UnityEngine;
using DG.Tweening;

public class ButtonAnimations : MonoBehaviour
{
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
        AudioManager.Instance.Play("ButtonClick");
    }
}
