using System.Collections;
using UnityEngine;

public class StrikeSeriesScript : MonoBehaviour
{
    [SerializeField] private GameSettings settings;
    [SerializeField] private ComboSeriesUI comboSeriesUI;

    private int series;
    private float timer;

    private void Start()
    {
        timer = 0;
        series = 0;
        StartCoroutine(SeriesUpdate());
    }

    private IEnumerator SeriesUpdate()
    {
        if(timer >= settings.TimeAttenuation)
            series = 0;
        else if(timer < settings.TimeAttenuation)
            timer += Time.deltaTime;

        yield return null;
        StartCoroutine(SeriesUpdate());
    }

    public int CountSeries(int points)
    {
        timer = 0;

        if(series < settings.ComboSettings.MaxCombo)
            series++;

        if (series >= 2)
            ChangeUI();

        return points * series;
    }

    private void ChangeUI()
    {
        if (comboSeriesUI != null)
            comboSeriesUI.ChangeSeriesText(series);
        else
            Debug.Log("ComboUI not found!");
    }
}
