using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Linq;

public class AlertSystem : MonoBehaviour
{

    Sequence alertTween;
    float panelTweenTime = 0.8f;

    public float waitTime = 2.0f;
    [SerializeField] GameObject AlertBox;
    [SerializeField] TextMeshProUGUI AlertText;

    List<string> messageQueue = new List<string>();

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

    private void Update()
    {
        if (alertTween == null && messageQueue.Count > 0)
        {
            alertTween = DOTween.Sequence();

            AlertText.SetText(messageQueue[0]);
            messageQueue.RemoveAt(0);

            alertTween
                .Append(AlertBox.transform.DOScale(new Vector3(1f, 1f, 1f), panelTweenTime).SetEase(Ease.InOutExpo))
                .AppendInterval(waitTime)
                .Append(AlertBox.transform.DOScale(new Vector3(0f, 0f, 0f), panelTweenTime).SetEase(Ease.InOutExpo))
                .SetRecyclable(true)
                .SetAutoKill(false);
        }
        if (messageQueue.Count > 0 && !alertTween.IsPlaying())
        {
            AlertText.SetText(messageQueue[0]);
            messageQueue.RemoveAt(0);
            alertTween.Restart();
        }
    }

    public void CreateAlert(string message)
    {
        messageQueue.Add(message);
    }
}
