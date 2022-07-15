using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(menuName ="Settings", fileName = "GameSettings")]
public class GameSettings : ScriptableObject
{
    [SerializeField] private List<FruitSprite> fruitSprites;

    [SerializeField] private List<Spawner> spawners;

    [SerializeField] private HealthSettings health;

    [SerializeField] private ScaleSettings scaleSettings;

    [SerializeField] private BlobSettings blobSettings;

    [SerializeField] private ComboSettings comboSettings;

    [SerializeField] private int numberOfPointsPerFruit;

    [SerializeField] private int maxFriutsAdd;

    [SerializeField] private int addingFruitsForPoints;

    [SerializeField] private float gravity;

    [SerializeField] private GameObject textPointsStile;

    [SerializeField] private float timeAttenuation;


    public HealthSettings Health => health;

    public List<FruitSprite> FruitSprites => fruitSprites;

    public List<Spawner> Spawners => spawners;

    public BlobSettings BlobSettings => blobSettings;

    public ComboSettings ComboSettings => comboSettings;

    public int NumberOfPointsPerFruit => numberOfPointsPerFruit;

    public int MaxFriutsAdd => maxFriutsAdd;

    public int AddingFruitsForPoints => addingFruitsForPoints;

    public float Gravity => gravity;

    public GameObject TextPointsStyle => textPointsStile;

    public ScaleSettings ScaleSettings => scaleSettings;

    public float TimeAttenuation => timeAttenuation;
}
