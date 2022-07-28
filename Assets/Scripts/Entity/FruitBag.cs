using UnityEngine;

public class FruitBag : Unit
{
    [SerializeField] private CutSpriteScript cutObject;
    [SerializeField] private FruitScript fruit;
    [SerializeField] private SpriteRenderer[] bagSprites;

    private FruitBagSettings _fruitBagSettings;

    private void Awake()
    {
        StartCoroutine(OutOfBounds());
        _fruitBagSettings = Settings.FruitBagSettings;

        PullObjects.Units.Add(this);
    }

    private void FixedUpdate()
    {
        ChangeScaleOnWindow();

        ChageRadiusCollider(_fruitBagSettings.RadiusCollider);
    }

    public override void Destruction()
    {
        int countFruits = Random.Range(_fruitBagSettings.MinFruitsInBag, _fruitBagSettings.MaxFruitsInBag + 1);
        for (int i = 0; i < countFruits; i++)
        {
            CreateFruits();
        }
        SourceSprite = null;
        SliceBagSprites();
        Destroy(gameObject);
    }

    private void SliceBagSprites()
    {
        foreach (SpriteRenderer sprite in bagSprites)
        {
            cutObject.SourceSprite = sprite;
            cutObject.CreateTwoHalves(SourcePhysics.Angle, SourcePhysics.Impuls, SourcePhysics.TimeLive, transform.position);
        }
    }

    private void CreateFruits()
    {
        FruitScript newFruit = Instantiate(fruit);

        int angle = Random.Range(_fruitBagSettings.MinAngleImpulseFruit,
            _fruitBagSettings.MaxAngleImpulseFruit + 1);

        float impulse = Random.Range(_fruitBagSettings.MinImpulsFruit,
            _fruitBagSettings.MaxImpulsFruit);

        Vector3 position = new Vector3(Random.Range(transform.position.x - RadiusCollider, 
            transform.position.x + RadiusCollider), transform.position.y,
            SourceSprite.transform.position.z  );

        newFruit.Trow(angle, impulse, Settings.Gravity, position);
    }

    private void OnDestroy()
    {
        PullObjects.Units.Remove(this);
    }
}
