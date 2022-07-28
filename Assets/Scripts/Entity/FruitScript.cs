using DG.Tweening;
using UnityEngine;

public class FruitScript : Unit
{
    [SerializeField] private CutSpriteScript cutObject;
    [SerializeField] private ParticleSystem juiceParticle;

    private int _numFruit;
    private FruitSettings _fruitSettings;
    private bool _onVisible;
    private Sequence _sequence;
    private bool _onSlice;

    public FruitSettings FruitSettings => _fruitSettings;

    private void Awake()
    {
        StartCoroutine(OutOfBounds());

        _numFruit = Random.Range(0, Settings.FruitSettings.Count);

        _fruitSettings = Settings.FruitSettings[_numFruit];
        SourceSprite.sprite = _fruitSettings.FruitsSprite;
    }

    private void Start()
    {
        StartScale = transform.localScale.y;

        HeightSprite = (SourceSprite.sprite.bounds.size.y) / 2;

        PullObjects.Units.Add(this);
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 361));


        _sequence = DOTween.Sequence();

        _sequence.Append(transform.DORotate(new Vector3(0, 0, 180), Settings.TimeSpeedRotate).SetEase(Ease.Linear));
        _sequence.SetLoops(-180, LoopType.Incremental);

        NormalizeObject();
    }

    private void FixedUpdate()
    {
        NormalizeObject();

        if (transform.position.y > 0 && (transform.position.x > 0 || transform.position.x < WorldSizeCamera.HalfWidth))
            _onVisible = true;
    }

    private void NormalizeObject()
    {
        ChangeScaleOnWindow();

        ChageRadiusCollider(_fruitSettings.RadiusCollider);
    }

    public override void Destruction()
    {
        _onSlice = true;
        PullObjects.GamePlayer.AddPoint(Settings.NumberOfPointsPerFruit, SourceSprite, _fruitSettings.ColorFruit);

        cutObject.CreateTwoHalves(SourcePhysics.Angle, SourcePhysics.Impuls, SourcePhysics.TimeLive, transform.position);

        MoreBlobsCreate(_fruitSettings.BlobSprite, Random.Range(Settings.BlobSettings.MinBlobCount, Settings.BlobSettings.MaxBlobCount));

        ParticleSystem newParticle = Instantiate(juiceParticle, transform.position, transform.rotation);

        ParticleSystem.MainModule colorParticle = newParticle.main;

        colorParticle.startColor = _fruitSettings.ColorFruit;

        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        float halfHeight = WorldSizeCamera.HalfHeight;

        if (!_onSlice && transform.position.y < halfHeight && _onVisible)
        {
            PullObjects.GamePlayer.SubstractHealth(1);

            Destroy(gameObject);
        }
        else if (transform.position.y < -5)
        {

            Destroy(gameObject);
        }
    }
    
    private void OnDestroy()
    {
        PullObjects.Units.Remove(this);
        _sequence.Kill();
    }
}
