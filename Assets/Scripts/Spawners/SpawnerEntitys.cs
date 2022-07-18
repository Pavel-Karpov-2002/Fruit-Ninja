using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

public class SpawnerEntitys : MonoBehaviour
{
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

        foreach(SpawnerSettings spawner in settings.Spawners)
        {
            if(spawner.Priority == priority)
            {
                CreateMultipleFruits(spawner, settings.ChancesOfSpawn);
            }
        }

        yield return new WaitForSeconds(settings.IntervalBetweenEntitysLoss - _reducingInterval);

        if(settings.MinIntervalBetweenEntitysLoss > settings.IntervalBetweenEntitysLoss - _reducingInterval)
            _reducingInterval -= settings.RedusingInInterval;

        StartCoroutine(StartCreateFruits());
    }

    private void CreateMultipleFruits(SpawnerSettings spawner, List<ChancesOfSpawn> chancesOfSpawn)
    {
        int[] countObject = new int[chancesOfSpawn.Count + 1];

        int countFruits = Random.Range(spawner.MinObjects, spawner.MaxObjects + _increase);
        bool isCreateObject;

        if (settings.MaxFriutsAdd > CoreValues.NumberOfPoints / settings.AddingFruitsForPoints)
            _increase = Random.Range(settings.MinFriutsAdd, CoreValues.NumberOfPoints / settings.AddingFruitsForPoints);
        else
            _increase = Random.Range(settings.MinFriutsAdd, settings.MaxFriutsAdd + 1);

        for (int i = 0; i < countFruits; i++)
        {
            isCreateObject = false;
            for (int j = 0; j < chancesOfSpawn.Count; j++)
            {
                if (chancesOfSpawn[j].MinPointsForSpawn <= CoreValues.NumberOfPoints && Random.Range(0, 101) <= chancesOfSpawn[j].MaxChanceOfSpawn)
                {
                    if (CoreValues.HealthCount >= settings.HealthSettings.MaxHealth && chancesOfSpawn[j].SpawnObject.GetComponent<BonusHeart>() != null)
                    {
                        continue;
                    }

                    if (countFruits * (chancesOfSpawn[j].MaxPercentageInPool / 100) - 1 > countObject[j])
                    {
                        countObject[j]++;

                        CreateObject(spawner, chancesOfSpawn[j].SpawnObject);
                        isCreateObject = true;
                        break;
                    }
                }
            }

            if(!isCreateObject)
                CreateObject(spawner, chancesOfSpawn[0].SpawnObject);
        }
    }

    private void CreateObject(SpawnerSettings spawner, GameObject typeObject)
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

        GameObject newObject = Instantiate(typeObject, position, typeObject.transform.rotation);

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
