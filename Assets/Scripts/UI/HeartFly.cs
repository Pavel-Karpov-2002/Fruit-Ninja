using UnityEngine;
using DG.Tweening;
using System.Collections;

public class HeartFly : MonoBehaviour
{
    [SerializeField] private RectTransform sourceRectTransform;
    [SerializeField] private GameSettings gameSettings;

    private Sequence _sequence;

    public RectTransform SourceRectTransform
    {
        get { return sourceRectTransform; }
        set { sourceRectTransform = value; }
    }

    private void Start()
    {
        transform.localScale = new Vector3(1, 1, 1);

        Destroy(gameObject, gameSettings.HealthSettings.TimeMoveHeartToHeartPanel);
    }

    public void Move(HealthPanelUI panel)
    {
        _sequence = DOTween.Sequence();

        _sequence.Append(transform.DOMove(
            panel.GetPositionHeartInWorldCoordinates(panel.CountHearts - 2) - new Vector3(0.2f, 0.2f),
            gameSettings.HealthSettings.TimeMoveHeartToHeartPanel));

        StartCoroutine(ChangeAlpha(panel));
    }

    private IEnumerator ChangeAlpha(HealthPanelUI panel)
    {
        yield return new WaitForSeconds(gameSettings.HealthSettings.TimeMoveHeartToHeartPanel - 0.1f);

        panel.ChangeAlphaColorHeart(0, 1);
    }

    private void OnDestroy()
    {
        _sequence.Kill();
    }
}
