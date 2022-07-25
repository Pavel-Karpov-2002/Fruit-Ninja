using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Magnet : Unit
{
    private float _timer;
    private bool _isActive;
    private Coroutine _checkPosition;
    private Sequence _sequence;
    private MagnetSettings _magnetSettings;

    private void Awake()
    {
        _magnetSettings = Settings.MagnetSettings;
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 361));
        
        _sequence = DOTween.Sequence();

        _sequence.Append(transform.DORotate(new Vector3(0, 0, 180), Settings.TimeSpeedRotate).SetEase(Ease.Linear));
        _sequence.SetLoops(-180, LoopType.Incremental);
    }

    private void Start()
    {
        StartCoroutine(OutOfBounds());
        HeightSprite = (SourceSprite.sprite.bounds.size.y) / 2;

        PullObjects.Units.Add(this);

        StartScale = transform.localScale.y;
        _timer = _magnetSettings.AttractionTime;
    }

    private void FixedUpdate()
    {
        ChangeScaleOnWindow();

        ChageRadiusCollider(Settings.MagnetSettings.RadiusCollider);
    }

    private void Update()
    {
        if(_isActive)
            _timer -= Time.deltaTime;

        if (CoreValues.HealthCount <= 0)
            _timer = 0;

        if (_timer <= 0)
        {
            if (_isActive)
            {
                SourcePhysics.TimeLive = 0;

                Trow(Random.Range(_magnetSettings.MinAngleDown, _magnetSettings.MaxAngleDown),
                    Random.Range(_magnetSettings.MinImpulsDown, _magnetSettings.MaxImpulsDown),
                    Settings.Gravity,
                    transform.position);
            }

            _isActive = false;

            StopCoroutine(_checkPosition);
        }
    }

    public override void Destruction()
    {
        if (!_isActive)
        {
            SourcePhysics.Gravity = 0;

            BlobCreate(_magnetSettings.BlobSprite, 1);

            _isActive = true;

            _checkPosition = StartCoroutine(CheckFruitsPosition());
        }
    }

    private IEnumerator CheckFruitsPosition()
    {
        yield return null;

        SearchNear(PullObjects.Units.ToArray());

        _checkPosition = StartCoroutine(CheckFruitsPosition());
    }

    private void SearchNear(Entity[] entities)
    {
        for (int i = 0; i < entities.Length; i++)
        {
            if (entities[i] == null || entities[i] is Magnet)
                continue;

            if (entities[i] != this)
            {
                if (entities[i].SourcePhysics.Gravity != 0 && GetDistance(entities[i]) <= _magnetSettings.RadiusAttraction)
                {
                    entities[i].SourcePhysics.Gravity = 0;
                    StartCoroutine(Step(entities[i]));
                }
            }
        }
    }

    private float GetDistance(Entity entity)
    {
        return Mathf.Abs(Vector2.Distance(transform.position, entity.transform.position));
    }

    private IEnumerator Step(Entity entity)
    {
        yield return null;

        if (entity != null)
        {
            Vector3 delta = entity.transform.position - transform.position;
            delta.Normalize();

            float moveSpeed = _magnetSettings.SpeedAttraction * Time.deltaTime * SpeedObject.Speed;

            entity.transform.position = entity.transform.position - (delta * moveSpeed);

            if (_timer > 0)
                StartCoroutine(Step(entity));
            else
            {
                entity.SourcePhysics.TimeLive = 0;
                entity.SourcePhysics.Impuls = 0;
                entity.SourcePhysics.Gravity = Settings.Gravity;
                entity.SourcePhysics.StartPosition = entity.transform.position;
            }
        }
    }

    private void OnDestroy()
    {
        _sequence.Kill();

        PullObjects.Units.Remove(this);
    }
}
