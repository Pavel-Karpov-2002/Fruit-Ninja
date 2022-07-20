using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayEvents : MonoBehaviour
{
    [SerializeField] private GameSettings settings;
    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private Image spriteAttenuation;
    [SerializeField] private StrikeSeriesScript strikeSeries;
    [SerializeField] private CanvasGroup loseCanvasGroup;
    [SerializeField] private TextInScreen pointsInSceen;

    [SerializeField] private HealthPanelUI healthPanel;
    [SerializeField] private List<PointsTextUI> countPointsText;
    [SerializeField] private List<RecordUI> recordText;

    private Sequence _sequence;

    public GameObject MainCanvas
    {
        get { return mainCanvas; }
        set { mainCanvas = value; }
    }

    public HealthPanelUI HealthPanel
    {
        get { return healthPanel; }
        set { healthPanel = value; }
    }

    private void Awake()
    {
        _sequence = DOTween.Sequence();
        PullObjects.GamePlayer = this;
        SpeedObject.ChangeSpeed(settings.SpeedObjects);
        
        spriteAttenuation.gameObject.SetActive(true);

        CoreValues.HealthCount = settings.HealthSettings.StartHealth;
    }

    private void Start()
    {
        if (spriteAttenuation != null)
            ChangeFade.AddAttenuation(spriteAttenuation, settings.TimeAttenuation, 0);

        UpdateRecord();

        foreach (var spawner in PullObjects.Spawners)
        {
            if (spawner != null)
                spawner.enabled = false;
        }

        StartCoroutine(TimeAttenuation());
    }

    private IEnumerator TimeAttenuation()
    {
        yield return new WaitForSeconds(settings.TimeAttenuation);

        spriteAttenuation.gameObject.SetActive(false);

        foreach (var spawner in PullObjects.Spawners)
        {
            spawner.enabled = true;
        }
    }

    public void SubstractHealth(int countHealth)
    {
        if (CoreValues.HealthCount > 0)
        {
            CoreValues.HealthCount -= countHealth;

            healthPanel.DestroyHeartImage();

            if (CoreValues.HealthCount <= 0)
            {
                StartCoroutine(RemoveUnnecessaryObjects());
            }
        }
    }

    public void AddHealth(int count)
    {
        CoreValues.HealthCount += count;
    }
    
    private IEnumerator RemoveUnnecessaryObjects()
    {
        Debug.Log(PullObjects.Spawners.Count);
        if(PullObjects.Spawners != null)
        {
            foreach (var spawner in PullObjects.Spawners)
            {
                spawner.enabled = false;
            }
        }

        yield return new WaitForSeconds(0.5f);

        if (PullObjects.Units.Count > 0)
        {
            StartCoroutine(RemoveUnnecessaryObjects());
        }
        else
        {
            yield return new WaitForSeconds(0.5f);

            AnimationUILose();
        }
    }

    private void AnimationUILose()
    {
        _sequence.Append(DOTween.To(ChangingOpasityLoseCanvas, 0, 1, settings.TimeAttenuation).SetEase(Ease.Linear));
    }

    private void ChangingOpasityLoseCanvas(float alpha)
    {
        loseCanvasGroup.alpha = alpha;
    }

    public void AddPoint(int countPoints, SpriteRenderer fruit, Color colorPoints)
    {
        int count = strikeSeries.CountSeries(countPoints);

        AddPoints.Add(count);

        TextInScreen pointsText = Instantiate(pointsInSceen);

        pointsText.Demonstration(fruit,
            count.ToString(),
            false,
            colorPoints
            );

        foreach (var text in countPointsText)
        {
            text.UpdateTextPoints();
        }

        if (CoreValues.NumberOfPoints > CoreValues.Record)
        {
            foreach (var text in recordText)
            {
                UpdateRecord();
            }
        }
    }

    private void UpdateRecord()
    {
        foreach (var text in recordText)
        {
            text.UpdateTextRecord();
        }
    }
}
