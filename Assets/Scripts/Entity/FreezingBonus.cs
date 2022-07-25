using DG.Tweening;
using System.Collections;
using UnityEngine;

public class FreezingBonus : Unit
{
    [SerializeField] private CutSpriteScript cutObject;
    [SerializeField] private SpriteRenderer shadow;

    private FreezingSettings _freezingSettings;
    private Sequence _sequence;
    private bool isActive;

    private void Awake()
    {
        _sequence = DOTween.Sequence();

        PullObjects.Units.Add(this);

        _freezingSettings = Settings.FreezingSettings;

        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 361));

        _sequence.Append(transform.DORotate(new Vector3(0, 0, 180), Settings.TimeSpeedRotate).SetEase(Ease.Linear));
        _sequence.SetLoops(-180, LoopType.Incremental);

    }

    private void FixedUpdate()
    {
        ChangeScaleOnWindow();

        ChageRadiusCollider(_freezingSettings.RadiusCollider);
    }

    public override void Destruction()
    {
        BlobCreate(_freezingSettings.BlobSprite, 1);
        StartCoroutine(Freezing());

        PullObjects.Units.Remove(this);

        cutObject.CreateTwoHalves(SourcePhysics.Angle, SourcePhysics.Impuls, SourcePhysics.TimeLive, transform.position);
        
        SourceSprite.sprite = null;
        shadow.sprite = null;
    }

    private IEnumerator Freezing()
    {
        DownSpeed();
        isActive = true;
        
        yield return new WaitForSeconds(_freezingSettings.TimeReduction);

        UpSpeed();

        Destroy(gameObject);
    }

    private void DownSpeed()
    {
        SpeedObject.ChangeSpeed(_freezingSettings.SpeedReduction);
    }

    private void UpSpeed()
    {
        SpeedObject.ChangeSpeed(Settings.SpeedObjects);
    }

    private void OnBecameInvisible()
    {
        if (gameObject.activeSelf && transform.position.y < WorldSizeCamera.HalfHeight && !isActive)
        {
            PullObjects.Units.Remove(this);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        _sequence.Kill();
    }
}
