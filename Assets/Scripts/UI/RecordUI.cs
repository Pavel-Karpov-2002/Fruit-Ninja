using DG.Tweening;
using TMPro;
using UnityEngine;

public class RecordUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _record;

    private float points;
    private const string recordStr = "Ћучший: ";

    private Sequence _sequence;

    private void Start()
    {
        _sequence = DOTween.Sequence();

        _record.text = recordStr + CoreValues.Record;
        UpdateTextRecord();
    }

    public void UpdateTextRecord()
    {
        _sequence.Append(DOTween.To(ScoringPoints, points, CoreValues.Record, 0.3f).SetEase(Ease.Linear));
    }

    private void ScoringPoints(float point)
    {
        points = point;

        _record.text = recordStr + ((int)point);
    }

    private void OnDestroy()
    {
        _sequence.Kill();
    }
}
