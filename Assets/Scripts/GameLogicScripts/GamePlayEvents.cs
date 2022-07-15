using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class GamePlayEvents : MonoBehaviour
{
    [SerializeField] private GameSettings settings;
    [SerializeField] private GameObject loseCanvas;
    [SerializeField] private GameObject spriteAttenuation;

    [SerializeField] private GameObject healthPanel;
    [SerializeField] private List<GameObject> countPointsText;
    [SerializeField] private List<GameObject> recordText;

    [SerializeField] private GameObject[] spawners;

    private List<Entity> _entitys;

    public List<Entity> Entitys
    {
        get { return _entitys; }
        set { _entitys = value; }
    }

    private void Awake()
    {
        spriteAttenuation.SetActive(true);
        foreach (GameObject spawner in spawners)
        {
            spawner.SetActive(false);
        }

        CoreValues.HealthCount = settings.Health.StartHealth;
    }

    private void Start()
    {
        ChangeFade.AddAttenuation(spriteAttenuation, settings.TimeAttenuation, 0);
        UpdateRecord();
        _entitys = new List<Entity>();

        StartCoroutine(TimeAttenuation());
    }

    private IEnumerator TimeAttenuation()
    {
        yield return new WaitForSeconds(settings.TimeAttenuation);

        spriteAttenuation.SetActive(false);
        foreach (GameObject spawner in spawners)
        {
            spawner.SetActive(true);
        }
    }

    public void SubstractHealth(int countHealth)
    {
        if (CoreValues.HealthCount > 0)
        {
            CoreValues.HealthCount -= countHealth;

            if (healthPanel.GetComponent<HeartUI>() != null)
            {
                healthPanel.GetComponent<HeartUI>().DestroyHeartImage();
            }
            else
                Debug.Log("HeartPanel not found!");

            if(CoreValues.HealthCount <= 0)
            {
                StartCoroutine(RemoveUnnecessaryObjects());
            }
        }
    }
    
    private IEnumerator RemoveUnnecessaryObjects()
    {
        if(spawners != null)
        {
            foreach (var spawner in spawners)
            {
                Destroy(spawner);
            }
        }

        yield return new WaitForSeconds(0.5f);

        FruitScript[] fruits = FindObjectsOfType<FruitScript>();

        if (fruits.Length > 0)
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
        loseCanvas.SetActive(true);
        DOTween.To(ChangingOpasityLoseCanvas, 0, 1, 1.5f).SetEase(Ease.Linear);
    }

    private void ChangingOpasityLoseCanvas(float alpha)
    {
        loseCanvas.GetComponent<CanvasGroup>().alpha = alpha;
    }

    public void AddPoint(int countPoints, GameObject fruit, Color colorPoints)
    {
        if (GetComponent<StrikeSeriesScript>() != null)
        {
            int count = GetComponent<StrikeSeriesScript>().CountSeries(countPoints);
            CoreValues.NumberOfPoints += count;

            DemonstrationPoints.Demonstration(fruit,
                count,
                settings.TextMeshProSettings.TextPointsStyle,
                settings.TextMeshProSettings,
                colorPoints
                );
        }
        else
            CoreValues.NumberOfPoints += countPoints;

        foreach (var text in countPointsText)
        {
            if (text.GetComponent<PointsTextUI>() != null)
                text.GetComponent<PointsTextUI>().UpdateTextPoints();
            else
                Debug.Log("Points Text not found!");
        }

        if (CoreValues.NumberOfPoints > CoreValues.Record)
        {
            CoreValues.Record = CoreValues.NumberOfPoints;
            SavingValues.SaveGame();

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
            if (text.GetComponent<RecordUI>() != null)
                text.GetComponent<RecordUI>().UpdateTextRecord();
            else
                Debug.Log("Points Text not found!");
        }
    }
}
