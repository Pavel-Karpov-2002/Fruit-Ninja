using DG.Tweening;
using TMPro;
using UnityEngine;

public class PointsTextUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numberOfPoints;

    private Sequence _sequence;

    private void Start()
    {
        _sequence = DOTween.Sequence();

        UpdateTextPoints();
    }

    public void UpdateTextPoints()
    {
        _sequence = DOTween.Sequence();

        _sequence.Append(DOTween.To(ScoringPoints, float.Parse(numberOfPoints.text), CoreValues.NumberOfPoints, 0.3f).SetEase(Ease.Linear));
    }

    private void ScoringPoints(float point)
    {
        numberOfPoints.text = ((int)point).ToString();
    }

    private void OnDestroy()
    {
        _sequence.Kill();
    }
}
