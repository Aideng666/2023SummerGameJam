using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] List<GameObject> buttons = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        ButtonAnim();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ButtonAnim()
    {
        Sequence sequence = DOTween.Sequence();

        sequence
            .Append(buttons[0].transform.DOMoveX(1003, 1).SetEase(Ease.InOutExpo))
            .Append(buttons[1].transform.DOMoveX(1003, 1).SetEase(Ease.InOutExpo))
            .Append(buttons[2].transform.DOMoveX(1003, 1).SetEase(Ease.InOutExpo))
            .Append(buttons[3].transform.DOMoveX(1003, 1).SetEase(Ease.InOutExpo));     
    }
}
