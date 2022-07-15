using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class PointsTextUI : MonoBehaviour
{
    private Text numberOfPoints;
    private void Awake()
    {
        numberOfPoints = GetComponent<Text>();
        if (numberOfPoints == null)
            Debug.Log("The counter does not have access to the text");
    }

    private void Start()
    {
        UpdateTextPoints();
    }

    public void UpdateTextPoints()
    {
        if (numberOfPoints != null)
            DOTween.To(ScoringPoints, float.Parse(numberOfPoints.text), CoreValues.NumberOfPoints, 0.3f).SetEase(Ease.Linear);
        else
            Debug.Log("The counter does not have access to the text");
    }

    private void ScoringPoints(float point)
    {
        numberOfPoints.text = ((int)point).ToString();
    }
}
