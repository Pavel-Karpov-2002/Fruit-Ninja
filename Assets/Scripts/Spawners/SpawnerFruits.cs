using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerFruits : MonoBehaviour
{
    [SerializeField] private GameObject fruit;
    [SerializeField] private GameObject bomb;
    [SerializeField] private float timerInterval;
    [SerializeField] private GameSettings settings;

    private int minPriority;
    private int maxPriority;
    private int increase;

    private void Awake()
    {
        if (settings.Spawners.Count == 0)
        {
            Debug.Log("Spawners count is 0");
        }
        else
        {
            minPriority = settings.Spawners.Min(s => s.Priority);
            maxPriority = settings.Spawners.Max(s => s.Priority);
        }
    }

    private void Start()
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
        int priority = Random.Range(minPriority, maxPriority + 1);

        foreach(Spawner spawner in settings.Spawners)
        {
            if(spawner.Priority == priority)
            {
                CreateMultipleFruits(spawner);
            }
        }

        yield return new WaitForSeconds(timerInterval);

        if (timerInterval > 0.4f)
            timerInterval -= 0.1f;


        StartCoroutine(StartCreateFruits());
    }
    
    private void CreateMultipleFruits(Spawner spawner)
    {
        int bombInPull = 0;

        if(increase < settings.MaxFriutsAdd)
            increase = Random.Range(0, CoreValues.NumberOfPoints / settings.AddingFruitsForPoints);

        int countFruits = Random.Range(spawner.MinObjects, spawner.MaxObjects + increase);

        for (int j = 0; j < countFruits; j++)
        {
            if(bombInPull < (countFruits * (spawner.MaxProcentCountBombInPull / 100)) - 1)
            {
                bombInPull++;
                int chance = Random.Range(0, 100);

                if (chance <= settings.BombSettings.MaxChanceConvertBombIntoFruits + settings.BombSettings.MinChanceConvertBombIntoFruits)
                {
                    CreateFruit(spawner, bomb);
                }
                else
                {
                    CreateFruit(spawner, fruit);
                }
            }
            else
            {
                CreateFruit(spawner, fruit);
            }
        }
    }

    private void CreateFruit(Spawner spawner, GameObject typeObject)
    {
        float halfWidth = WorldSizeCamera.HalfWidth;
        float halfHeight = WorldSizeCamera.HalfHeight;

        Vector3 position = new Vector3(Random.Range(halfWidth * 
            (spawner.BottomStartPosition / 100), halfWidth * (spawner.BottomEndPosition / 100)),
            Random.Range(halfHeight * (spawner.HeightStartPosition / 100), halfHeight * (spawner.HeightEndPosition / 100)), 
            0);

        if(position.y < 0)
            position -= new Vector3(0, 1, 0);

        if(position.y > 0)
            position -= new Vector3(1, 0, 0);

        GameObject newObject = Instantiate(typeObject, position, Quaternion.Euler(0, 0, Random.Range(0, 360)));

        float angle = 0;

        if (!spawner.IsLeft)
            angle = Random.Range(spawner.MinAngle, spawner.MaxAngle);
        else
        {
            angle = Random.Range(spawner.MaxAngle - 360, spawner.MinAngle);
            if (angle < 0)
                angle += 360;
        }

        newObject.GetComponent<Entity>().Trow(angle, Random.Range(spawner.MinImpuls, spawner.MaxImpuls), settings.Gravity, position);
    }
}
