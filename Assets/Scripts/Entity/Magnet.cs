using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Magnet : Unit
{
    private float _timer;
    private bool _isSlice;
    private Coroutine _checkPosition;
    private Sequence _sequence;
    private MagnetSettings _magnetSettings;

    private void Awake()
    {
        _magnetSettings = Settings.MagnetSettings;
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 361));

        _sequence.Append(transform.DORotate(new Vector3(0, 0, 180), Settings.SpeedRotate).SetLoops(-180, LoopType.Incremental).SetEase(Ease.Linear));
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
        if(_isSlice)
            _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            StopCoroutine(_checkPosition);

            SourcePhysics.TimeLive = 0;
            Trow(0, 0, Settings.Gravity, transform.position);
        }
    }

    public override void Destruction()
    {
        SourcePhysics.Gravity = 0;

        if (!_isSlice)
        {
            BlobCreate(_magnetSettings.BlobSprite, 1);

            _isSlice = true;
        }

        _checkPosition = StartCoroutine(CheckFruitsPosition());

        PullObjects.Units.Remove(this);
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
            if (entities[i] is null)
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
        return Vector3.Distance(transform.position, entity.transform.position);
    }

    private IEnumerator Step(Entity entity)
    {
        yield return null;

        if (!(entity is null))
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
    }
}
