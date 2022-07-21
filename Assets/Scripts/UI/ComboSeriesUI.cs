using UnityEngine;
using System.Collections;
using TMPro;
using DG.Tweening;

public class ComboSeriesUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countFruits;
    [SerializeField] private TextMeshProUGUI comboSeries;
    [SerializeField] private GameSettings settings;
    [SerializeField] private CanvasGroup sourceCanvasGroup;

    private Coroutine _fadeCoroutine;
    private Sequence _sequence;
    private ComboSettings _comboSettings;

    private void Start()
    {
        _sequence = DOTween.Sequence();

        _comboSettings = settings.ComboSettings;

        sourceCanvasGroup.alpha = 0;
    }

    public void ChangeSeriesText(int countSeries)
    {
        if(!(_sequence is null))
            _sequence.Kill();

        if (!(_fadeCoroutine is null))
            StopCoroutine(_fadeCoroutine);

        sourceCanvasGroup.alpha = _comboSettings.MaxFade;

        comboSeries.text = "X" + countSeries.ToString();
        countFruits.text = countSeries.ToString() + " фруктов";

        _fadeCoroutine = StartCoroutine(Fading());
    }

    private IEnumerator Fading()
    {
        yield return new WaitForSeconds(settings.ComboSettings.TimeDisappearanceComboText);

        if (_sequence is null)
            _sequence = DOTween.Sequence();

        _sequence.Append(DOTween.To(ChangeFade, sourceCanvasGroup.alpha, 0, _comboSettings.TimeFadeComboText));
    }

    private void ChangeFade(float alpha)
    {
        sourceCanvasGroup.alpha = alpha;
    }
}
