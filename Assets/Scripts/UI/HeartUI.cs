using UnityEngine;
using DG.Tweening;

public class HeartUI : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;
    [SerializeField] private CanvasGroup sourceCanvasGroup;
    [SerializeField] private RectTransform sourceRectTransform;

    private Sequence _sequence;

    public RectTransform SourceRectTransfrom
    {
        get { return sourceRectTransform; }
        set { sourceRectTransform = value; }
    }

    public CanvasGroup SourceCanvasGroup
    {
        get { return sourceCanvasGroup; }
        set { sourceCanvasGroup = value; }
    }

    private void Start()
    {
        _sequence = DOTween.Sequence();
        transform.localScale = Vector3.one;
    }

    public void ChangeScaleHeart(float scale)
    {
        _sequence.Append(transform.DOScale(scale, gameSettings.HealthSettings.HeartBeatSpeed).SetEase(Ease.Linear));
    }

    private void OnDestroy()
    {
        _sequence.Kill();
    }
}
