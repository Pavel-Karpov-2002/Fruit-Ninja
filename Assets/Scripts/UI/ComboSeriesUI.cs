using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;
using System.Linq;

public class ComboSeriesUI : MonoBehaviour
{
    [SerializeField] private Text countFruits;
    [SerializeField] private Text comboSeries;
    [SerializeField] private GameSettings settings;


    private Vector3 soursePosition;
    Sequence mySequence;

    private void Start()
    {
        mySequence = DOTween.Sequence();
        soursePosition = transform.position;

        if (GetComponent<CanvasGroup>() != null)
            GetComponent<CanvasGroup>().alpha = 0;
        else
            Debug.Log("CanvasGroup not found! Add canvasGroup.");
    }

    public void ChangeSeriesText(int countSeries)
    {
        GetComponent<CanvasGroup>().alpha = 1;

        Debug.Log(soursePosition);

        mySequence.Kill();

        GetComponent<CanvasGroup>().DOFade(1, settings.ComboSettings.TimeFadeComboText).SetEase(Ease.Linear);

        comboSeries.text = "X" + countSeries.ToString();

        countFruits.text = countSeries.ToString() + " фруктов";

        StopCoroutine(Dissapera());

        StartCoroutine(Dissapera());
    }

    private IEnumerator Dissapera()
    {
        yield return new WaitForSeconds(settings.ComboSettings.TimeDisappearanceComboText);

        GetComponent<CanvasGroup>().DOFade(0, settings.ComboSettings.TimeFadeComboText).SetEase(Ease.Linear);

    }
}
