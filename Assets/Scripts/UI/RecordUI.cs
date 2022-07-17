using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using TMPro;

public class RecordUI : MonoBehaviour
{
    private MaskableGraphic _record;

    private string _recordStr;
    private TextMeshProUGUI _recordMeshPro;
    private Text _recordTextUI;

    private void Awake()
    {
        _record = GetComponent<TextMeshProUGUI>();

        if (_record == null)
        {
            _record = GetComponent<Text>();
            if (_record == null)
                Debug.Log($"{gameObject.name} does not have access to the text");
            else
            {
                _recordTextUI = _record.GetComponent<Text>();
                _recordStr = _recordTextUI.text;
            }
        }
        else
        {
            _recordMeshPro = _record.GetComponent<TextMeshProUGUI>();
            _recordStr = _recordMeshPro.text;
        }
    }

    private void Start()
    {
        UpdateTextRecord();
    }

    public void UpdateTextRecord()
    {
        DOTween.To(ScoringPoints, float.Parse(string.Join("", _recordStr.Where(c => char.IsDigit(c)))), CoreValues.Record, 0.3f).SetEase(Ease.Linear);
    }

    private void ScoringPoints(float point)
    {
        _recordStr = "Лучший: " + ((int)point).ToString();

        if (_recordTextUI == null)
            _recordMeshPro.text = _recordStr;
        else
            _recordTextUI.text = _recordStr;
    }
}
