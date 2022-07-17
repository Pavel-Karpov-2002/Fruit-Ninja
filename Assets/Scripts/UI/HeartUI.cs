using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    [SerializeField] private GameSettings settings;

    [SerializeField] private GameObject heart;

    private HeartChange[] hearts;

    private Sequence changeScale;
    private HealthSettings healthSettings;
    private int numberHeartChange;

    private void Awake()
    {
        healthSettings = settings.HealthSettings;

        for (int i = 0; i < healthSettings.StartHealth; i++)
        {
            CreateHeartImage();
        }

        hearts = gameObject.GetComponentsInChildren<HeartChange>();
        numberHeartChange = hearts.Length;
    }
    
    private void Start()
    {
        changeScale = DOTween.Sequence();

        StartCoroutine(Heartbeat());
    }

    private void Update()
    {
        if(hearts.Length > CoreValues.HealthCount)
            GetAllHearts();
    }

    public void GetAllHearts()
    {
        hearts = gameObject.GetComponentsInChildren<HeartChange>();
        numberHeartChange = hearts.Length;
    }

    public void DestroyHeartImage()
    {
        GetAllHearts();

        if (hearts.Length > CoreValues.HealthCount)
        {
            Destroy(hearts[0].gameObject);
            GetAllHearts();
        }
    }

    public void AddHeart()
    {
        CreateHeartImage();
        GetAllHearts();
    }

    public void CreateHeartImage()
    {
        GameObject newHealth = Instantiate(heart);
        newHealth.transform.SetParent(gameObject.transform);
        newHealth.GetComponent<HeartChange>().ChangePosition(heart.transform);
    }

    private IEnumerator Heartbeat()
    {
        if (hearts.Length > 0)
        {
            numberHeartChange -= 1;
            if (numberHeartChange < 0)
                numberHeartChange = hearts.Length - 1;

            yield return new WaitForSeconds(healthSettings.HeartBeatSpeed);

            Beat(healthSettings.MaxHeartBeatScale, healthSettings.HeartBeatSpeed);

            yield return new WaitForSeconds(healthSettings.HeartBeatSpeed);

            Beat(healthSettings.MinHeartBeatScale, healthSettings.HeartBeatSpeed);

            StartCoroutine(Heartbeat());
        }
    }

    private void Beat(float to, float speed)
    {
        if (numberHeartChange < hearts.Length && hearts[numberHeartChange] != null)
        {
            DOTween.Kill(changeScale, false);

            changeScale.Append(hearts[numberHeartChange].transform.DOScale(to, speed).SetEase(Ease.Linear));
        }
        else
        {
            foreach(var heart in hearts)
            {
                if(heart != null)
                    changeScale.Append(heart.transform.DOScale(healthSettings.MinHeartBeatScale, 0.2f).SetEase(Ease.Linear));
            }
        }
    }

    public void ChangeAlphaColorHeart(int numberHeart, float alpha)
    {
        if(hearts[numberHeart] != null)
            hearts[numberHeart].GetComponent<CanvasGroup>().alpha = alpha;
    }

    public Vector2 GetScaleHeart(int numberHeart)
    {
        return hearts[numberHeart].GetComponent<RectTransform>().sizeDelta;
    }

    public Vector3 GetPositionHeartInWorldCoordinates(int numberHeart)
    {
        RectTransform lastHeartPosition = hearts[numberHeart].GetComponent<RectTransform>();

        return lastHeartPosition.TransformPoint(lastHeartPosition.transform.position);
    }
}
