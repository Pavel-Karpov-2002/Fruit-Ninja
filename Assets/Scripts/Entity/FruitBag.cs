using UnityEngine;

public class FruitBag : Entity
{
    [SerializeField] private GameObject fruit;
    [SerializeField] private GameSettings gameSettings;
    [SerializeField] private GameObject fruitsSprite;

    private FruitBagSettings _fruitBagSettings;

    private void Awake()
    {
        blade = FindObjectOfType<GamePlayEvents>();
        spawners = FindObjectsOfType<SpawnerEntitys>();

        ColliderSphere = GetComponent<ColliderSphere>();
        Slice = GetComponent<SliceRange>();

        if (ColliderSphere == null)
            Debug.Log($"{gameObject.name} don't have a collider!");

        if (Slice == null)
            Debug.Log($"{gameObject.name} don't have a slice script!");

        _fruitBagSettings = gameSettings.FruitBagSettings;
    }


    private void FixedUpdate()
    {
        ScaleChangeScript.ChangeOnWindow(transform, gameSettings.ScaleSettings.MinScaleOnWindow, gameSettings.ScaleSettings.MaxScaleOnWindow);

        ChageRadiusCollider(_fruitBagSettings.RadiusCollider);
    }

    public override void Destruction()
    {
        int countFruits = Random.Range(_fruitBagSettings.MinFruitsInBag, _fruitBagSettings.MaxFruitsInBag + 1);

        for(int i = 0; i < countFruits; i++)
        {
            CreateFruits();
        }

        RemoveFruitsInBag();
        blade.Entitys.Remove(this);
    }

    private void RemoveFruitsInBag()
    {
        Destroy(fruitsSprite);
    }

    private void CreateFruits()
    {
        GameObject newFruit = Instantiate(fruit);

        int angle = Random.Range(_fruitBagSettings.MinAngleImpulseFruit, _fruitBagSettings.MaxAngleImpulseFruit + 1);

        float impulse = Random.Range(_fruitBagSettings.MinImpulsFruit, _fruitBagSettings.MaxImpulsFruit);

        Vector3 position = new Vector3(Random.Range(transform.position.x - RadiusCollider, transform.position.x + RadiusCollider), transform.position.y, fruitsSprite.transform.position.z  );

        newFruit.GetComponent<Physics>().AddImpulse(angle, impulse, gameSettings.Gravity, position);
    }

    private void OnBecameInvisible()
    {
        if (gameObject.activeSelf)
            Destroy(gameObject);
    }
}
