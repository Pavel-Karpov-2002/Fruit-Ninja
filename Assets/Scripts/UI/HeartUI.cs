using System.Collections;
using UnityEngine;
using DG.Tweening;

public class HeartUI : MonoBehaviour
{
    [SerializeField] private GameSettings settings;

    [SerializeField] private GameObject heart;

    private HeartChange[] hearts;

    private void Awake()
    {
        for (int i = 0; i < settings.Health.StartHealth; i++)
        {
            CreateHeartImage();
        }
    }
    
    private void Start()
    {
        StartCoroutine(GetAllHearts());
    }

    public IEnumerator GetAllHearts()
    {
        StopCoroutine(Heartbeat(0));

        yield return new WaitForSeconds(0.1f);
        hearts = gameObject.GetComponentsInChildren<HeartChange>();

        if (hearts.Length > CoreValues.HealthCount)
            DestroyHeartImage();
        else if (hearts.Length > 0 && hearts[0] != null )
            StartCoroutine(Heartbeat(hearts.Length));

    }

    public void DestroyHeartImage()
    {
        if (hearts.Length > CoreValues.HealthCount)
        {
            StopCoroutine(Heartbeat(0));
            hearts = gameObject.GetComponentsInChildren<HeartChange>();
            Destroy(hearts[0].gameObject);
            StartCoroutine(GetAllHearts());
        }
    }

    public void CreateHeartImage()
    {
        GameObject newHealth = Instantiate(heart);
        newHealth.transform.SetParent(gameObject.transform);
        newHealth.GetComponent<HeartChange>().ChangePosition(heart.transform);
    }

    private IEnumerator Heartbeat(int numberHeart)
    {
        if (hearts.Length > 0)
        {
            numberHeart -= 1;
            if (numberHeart < 0)
                numberHeart = hearts.Length - 1;

            yield return new WaitForSeconds(0.5f);

            Beat(numberHeart, settings.ScaleSettings.HeartBeatScale, settings.ScaleSettings.HeartBeatSpeed);

            yield return new WaitForSeconds(0.5f);

            Beat(numberHeart, 1, settings.ScaleSettings.HeartBeatSpeed);

            StartCoroutine(Heartbeat(numberHeart));
        }
        else
        {
            StopCoroutine(Heartbeat(0));
        }
    }

    private void Beat(int numberHeart, float to, float speed)
    {
        if (numberHeart < hearts.Length && hearts[numberHeart] != null)
            ScaleChangeScript.Change(hearts[numberHeart].transform, to, speed);

    }
}
