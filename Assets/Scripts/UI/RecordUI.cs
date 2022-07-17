using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using TMPro;

public class RecordUI : MonoBehaviour
{
    private TextMeshProUGUI _record;

    private void Awake()
    {
        _record = GetComponent<TextMeshProUGUI>();

        if (_record == null)
        {
            Debug.Log($"{gameObject.name} does not have access to the text");
        }
    }

    private void Start()
    {
        _record.text = "Лучший: " + CoreValues.Record;
        UpdateTextRecord();
    }

    public void UpdateTextRecord()
    {
        DOTween.To(ScoringPoints, float.Parse(string.Join("", _record.text.Where(c => char.IsDigit(c)))), CoreValues.Record, 0.3f).SetEase(Ease.Linear);
    }

    private void ScoringPoints(float point)
    {
        _record.text = "Лучший: " + ((int)point).ToString();
    }
}
