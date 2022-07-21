using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPanelUI : MonoBehaviour
{
    [SerializeField] private GameSettings settings;

    [SerializeField] private HeartUI heart;

    private HealthSettings _healthSettings;
    private List<HeartUI> _hearts;
    private int _numberHeartChange;
    private Coroutine _changeHeart;
    private bool _coroutineAllowed;

    public int CountHearts
    {
        get => _hearts.Count;
    }

    private void Awake()
    {
        _healthSettings = settings.HealthSettings;
        _hearts = new List<HeartUI>();

        for (int i = 0; i < _healthSettings.StartHealth; i++)
        {
            CreateHeartImage();
        }
    }
    
    private void Start()
    {
        _numberHeartChange = _hearts.Count;

        _changeHeart = StartCoroutine(Heartbeat());

    }

    public void DestroyHeartImage()
    {
        if(!(_changeHeart is null))
            StopCoroutine(_changeHeart);

        for (int i = 0; i < _hearts.Count; i++)
        {
            Destroy(_hearts[i].gameObject);
        }
        
        if(_hearts.Count > 0)
            _hearts.Clear();

        for (int i = 0; i < CoreValues.HealthCount; i++)
        {
            CreateHeartImage();
        }

        _changeHeart = StartCoroutine(Heartbeat());
    }

    public void AddHeart()
    {
        CreateHeartImage();
    }

    public void CreateHeartImage()
    {
        HeartUI newHealth = Instantiate(heart);
        newHealth.transform.SetParent(gameObject.transform);

        _hearts.Add(newHealth);
        _numberHeartChange = _hearts.Count;
    }

    private IEnumerator Heartbeat()
    {
        _coroutineAllowed = true;
        if (_hearts.Count > 0)
        {
            if (_numberHeartChange - 1 > 0)
                _numberHeartChange -= 1;
            else
                _numberHeartChange = _hearts.Count;

            yield return new WaitForSeconds(_healthSettings.HeartBeatSpeed);

            _hearts[_numberHeartChange - 1].ChangeScaleHeart(_healthSettings.MaxHeartBeatScale);

            yield return new WaitForSeconds(_healthSettings.HeartBeatSpeed);

            _hearts[_numberHeartChange - 1].ChangeScaleHeart(_healthSettings.MinHeartBeatScale);

            _changeHeart = StartCoroutine(Heartbeat());
        }
    }

    public void ChangeAlphaColorHeart(int numberHeart, float alpha)
    {
        _hearts[numberHeart].SourceCanvasGroup.alpha = alpha;
    }

    public Vector2 GetScaleHeart(int numberHeart)
    {
        return _hearts[numberHeart].SourceRectTransfrom.sizeDelta;
    }

    public Vector3 GetPositionHeartInWorldCoordinates(int numberHeart)
    {
        RectTransform heartPosition = _hearts[numberHeart].SourceRectTransfrom;

        return heartPosition.TransformPoint(heartPosition.transform.position);
    }
}
