using UnityEngine;

public class BonusHeart : Unit
{
    [SerializeField] private CutSpriteScript cutObject;
    [SerializeField] private HeartFly heart;

    private HealthSettings _healthSettings;
    private HealthPanelUI _panel;

    private void Start()
    {
        StartCoroutine(OutOfBounds());

        _panel = PullObjects.GamePlayer.HealthPanel;

        StartScale = transform.localScale.y;

        HeightSprite = (SourceSprite.sprite.bounds.size.y) / 2;

        PullObjects.Units.Add(this);

        _healthSettings = Settings.HealthSettings;

        SourcePhysics.ChangeSpeed(_healthSettings.IncreaseSpeed);
    }

    private void FixedUpdate()
    {
        ChangeScaleOnWindow();

        ChageRadiusCollider(_healthSettings.RadiusCollider);
    }

    public override void Destruction()
    {
        _panel.AddHeart();
        _panel.ChangeAlphaColorHeart(0, 0);

        PullObjects.GamePlayer.AddHealth(_healthSettings.CountHealthAdd);

        transform.rotation = Quaternion.Euler(0, 0, 0);

        BlobCreate(_healthSettings.BlobSprite, 1);
        cutObject.CreateTwoHalves(SourcePhysics.Angle, SourcePhysics.Impuls, SourcePhysics.TimeLive, transform.position);

        SourcePhysics.enabled = false;

        PullObjects.Units.Remove(this);

        HeartFly();

        Destroy(gameObject);
    }

    private void HeartFly()
    {
        HeartFly newHeart = Instantiate(heart, transform.position, transform.rotation);

        newHeart.transform.SetParent(PullObjects.GamePlayer.MainCanvas.transform);
        newHeart.SourceRectTransform.sizeDelta = _panel.GetScaleHeart(0);

        newHeart.Move(_panel);
    }

    private void OnDestroy()
    {
        PullObjects.Units.Remove(this);
    }
}
