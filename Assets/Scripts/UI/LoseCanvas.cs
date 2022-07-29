using DG.Tweening;
using UnityEngine;

public class LoseCanvas : MonoBehaviour
{
    [SerializeField] private CanvasGroup sourceCanvasGroup;
    [SerializeField] private GameSettings settings;

    private Sequence _sequence;

    private void OnEnable()
    {
        _sequence = DOTween.Sequence();

        _sequence.Append(DOTween.To(ChangeFade, sourceCanvasGroup.alpha, 1, settings.TimeAttenuation));
    }

    private void ChangeFade(float alpha)
    {
        sourceCanvasGroup.alpha = alpha;
    }

    private void OnDisable()
    {
        _sequence.Kill();
    }
}
