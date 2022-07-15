using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class ComboSeriesUI : MonoBehaviour
{
    [SerializeField] private Text countFruits;
    [SerializeField] private Text comboSeries;
    [SerializeField] private GameSettings settings;

    Sequence mySequence;

    private void Start()
    {
        mySequence = DOTween.Sequence();

        if (GetComponent<CanvasGroup>() != null)
            GetComponent<CanvasGroup>().alpha = 0;
        else
            Debug.Log("CanvasGroup not found! Add canvasGroup.");
    }

    public void ChangeSeriesText(int countSeries)
    {
        GetComponent<CanvasGroup>().alpha = 1;
        mySequence.Kill();
        DOTween.Kill(mySequence);
        mySequence = DOTween.Sequence();

        comboSeries.text = "X" + countSeries.ToString();
        countFruits.text = countSeries.ToString() + " фруктов";

        StopCoroutine(Dissapera());
        StartCoroutine(Dissapera());
    }

    private IEnumerator Dissapera()
    {
        yield return new WaitForSeconds(settings.ComboSettings.TimeDisappearanceComboText);

        if(GetComponent<CanvasGroup>().alpha == 1)
            ChangeFade(0);
    }

    private void ChangeFade(float endValue)
    {
        mySequence.Append(GetComponent<CanvasGroup>().DOFade(endValue, settings.ComboSettings.TimeFadeComboText).SetEase(Ease.Linear));
    }
}
