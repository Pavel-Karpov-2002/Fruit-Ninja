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
        CoreValues.NumberOfPoints = 0;

        spriteAttenuation.DOFade(0, settings.TimeAttenuation).SetEase(Ease.Linear);

        UpdateRecord();
        CangeSpawners(false);
        loseCanvasGroup.gameObject.SetActive(false);

        StartCoroutine(TimeAttenuation());
    }

    private IEnumerator TimeAttenuation()
    {
        yield return new WaitForSeconds(settings.TimeAttenuation);

        spriteAttenuation.gameObject.SetActive(false);

        CangeSpawners(true);
    }

    private void CangeSpawners(bool onActive)
    {
        foreach (var spawner in PullObjects.Spawners)
        {
            spawner.gameObject.SetActive(onActive);
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
        CangeSpawners(false);

        yield return new WaitForSeconds(0.5f);

        if (PullObjects.Units.Count > 0)
        {
            StartCoroutine(RemoveUnnecessaryObjects());
        }
        else
        {
            yield return new WaitForSeconds(0.5f);

            loseCanvasGroup.gameObject.SetActive(true);

            AnimationUILose();
        }
    }

    private void AnimationUILose()
    {
        _sequence = DOTween.Sequence();

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
            CoreValues.Record = CoreValues.NumberOfPoints;
            SavingValues.SaveGame();
            UpdateRecord();
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
