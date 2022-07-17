using System.Collections;
using System.Linq;
using UnityEngine;

public class SpawnerEntitys : MonoBehaviour
{
    [SerializeField] private GameObject fruit;
    [SerializeField] private GameObject bomb;
    [SerializeField] private GameObject heartBonus;
    [SerializeField] private GameSettings settings;

    private int _minPriority;
    private int _maxPriority;
    private int _increase;
    private float _reducingInterval;

    private void Awake()
    {
        if (settings.Spawners.Count == 0)
        {
            Debug.Log("Spawners count is 0");
        }
        else
        {
            _minPriority = settings.Spawners.Min(s => s.Priority);
            _maxPriority = settings.Spawners.Max(s => s.Priority);
        }
    }

    private void Start()
    {
        _reducingInterval = 0;
    }

    private void OnEnable()
    {
        if (settings.Spawners.Count == 0)
        {
            Debug.Log("Spawners count is 0");
        }
        else
        {
            StartCoroutine(StartCreateFruits());
        }
    }

    private IEnumerator StartCreateFruits()
    {
        int priority = Random.Range(_minPriority, _maxPriority + 1);

        foreach(Spawner spawner in settings.Spawners)
        {
            if(spawner.Priority == priority)
            {
                CreateMultipleFruits(spawner);
            }
        }

        yield return new WaitForSeconds(settings.IntervalBetweenEntitysLoss - _reducingInterval);

        if(settings.MinIntervalBetweenEntitysLoss > settings.IntervalBetweenEntitysLoss - _reducingInterval)
            _reducingInterval -= settings.RedusingInInterval;

        StartCoroutine(StartCreateFruits());
    }
    
    private void CreateMultipleFruits(Spawner spawner)
    {
        int bombInPull = 0;
        int bonutsInPull = 0;

        if (settings.MaxFriutsAdd > CoreValues.NumberOfPoints / settings.AddingFruitsForPoints)
            _increase = Random.Range(settings.MinFriutsAdd, CoreValues.NumberOfPoints / settings.AddingFruitsForPoints);
        else
            _increase = Random.Range(settings.MinFriutsAdd, settings.MaxFriutsAdd + 1);

        int countFruits = Random.Range(spawner.MinObjects, spawner.MaxObjects + _increase);

        for (int j = 0; j < countFruits; j++)
        {
            int chance = Random.Range(0, 101);

            if(CalculateChance(chance, countFruits, settings.BombSettings.MaxChanceConvertBombIntoFruits, spawner.MaxProcentCountBombInPull, bombInPull))
            {
                CreateFruit(spawner, bomb);
                bombInPull++;
            }
            if(CalculateChance(chance, countFruits, settings.HealthSettings.MaxChanceConvertHeartIntoFruits, spawner.MaxProcentCountHeartInPull, bonutsInPull))
            {
                CreateFruit(spawner, heartBonus);
                bonutsInPull++;
            }
            else
            {
                CreateFruit(spawner, fruit);
            }
        }
    }

    private bool CalculateChance(float chance, int countFruits, int maxConvertFruits, float maxProcentCountInPull, int containPull)
    {
        return chance <= maxConvertFruits && containPull <= (countFruits * (maxProcentCountInPull / 100)) - 1;
    }

    private void CreateFruit(Spawner spawner, GameObject typeObject)
    {
        float halfWidth = WorldSizeCamera.HalfWidth;
        float halfHeight = WorldSizeCamera.HalfHeight;

        Vector3 position = new Vector3(Random.Range(halfWidth * 
            (spawner.BottomStartPosition / 100), halfWidth * (spawner.BottomEndPosition / 100)),
            Random.Range(halfHeight * (spawner.HeightStartPosition / 100), halfHeight * (spawner.HeightEndPosition / 100) + 1), 
            0);

        if(position.y < 0)
            position -= new Vector3(0, 1, 0);

        if(position.y > 0)
            position -= new Vector3(1, 0, 0);

        GameObject newObject = Instantiate(typeObject, position, Quaternion.Euler(0, 0, Random.Range(0, 360)));

        float angle = 0;

        if (!spawner.IsLeft)
            angle = Random.Range(spawner.MinAngle, spawner.MaxAngle + 1);
        else
        {
            angle = Random.Range(spawner.MaxAngle - 360, spawner.MinAngle);
            if (angle < 0)
                angle += 360;
        }

        newObject.GetComponent<Entity>().Trow(angle, Random.Range(spawner.MinImpuls, spawner.MaxImpuls), settings.Gravity, position);
    }
}
