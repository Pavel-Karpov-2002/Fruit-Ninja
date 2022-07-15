using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class RecordUI : MonoBehaviour
{
    private Text record;

    private void Awake()
    {
        SavingValues.LoadGame();

        record = GetComponent<Text>();
        if (record == null)
            Debug.Log("The counter does not have access to the text");
    }

    private void Start()
    {
        UpdateTextRecord();
    }

    public void UpdateTextRecord()
    {
        if (record != null)
        {
            DOTween.To(ScoringPoints, float.Parse(string.Join("", record.text.Where(c => char.IsDigit(c)))), CoreValues.Record, 0.3f).SetEase(Ease.Linear);
        }
        else
            Debug.Log("The counter does not have access to the text");
    }

    private void ScoringPoints(float point)
    {
        record.text = "Лучший: " + ((int)point).ToString();
    }
}
