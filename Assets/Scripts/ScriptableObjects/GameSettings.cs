using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Settings", fileName = "GameSettings")]
public class GameSettings : ScriptableObject
{
    [SerializeField] private ScaleOnWindowSettings scaleSettings;

    [SerializeField] private List<FruitSettings> fruitSettings;

    [SerializeField] private List<Spawner> spawners;

    [SerializeField] private HealthSettings healthSettings;

    [SerializeField] private BlobSettings blobSettings;

    [SerializeField] private ComboSettings comboSettings;

    [SerializeField] private TextMeshProSettings textMeshProSettings;

    [SerializeField] private BombSettigns bombSettings;

    [SerializeField] private int numberOfPointsPerFruit;

    [SerializeField][Min(0)] private int maxFriutsAdd;

    [SerializeField][Min(0)] private int minFriutsAdd;

    [SerializeField][Min(0)] private int addingFruitsForPoints;

    [SerializeField] private float gravity;

    [SerializeField][Min(0)] private float timeAttenuation;

    [SerializeField][Min(0)] private float intervalBetweenEntitysLoss;

    [SerializeField][Min(0)] private float minIntervalBetweenEntitysLoss;

    [SerializeField][Min(0)] private float redusingInInterval;

    [SerializeField][Min(0)] private float speedSlice;


    public ScaleOnWindowSettings ScaleSettings => scaleSettings;

    public HealthSettings HealthSettings => healthSettings;

    public List<FruitSettings> FruitSettings => fruitSettings;

    public List<Spawner> Spawners => spawners;

    public BlobSettings BlobSettings => blobSettings;

    public ComboSettings ComboSettings => comboSettings;

    public TextMeshProSettings TextMeshProSettings => textMeshProSettings;

    public BombSettigns BombSettings => bombSettings;

    public int NumberOfPointsPerFruit => numberOfPointsPerFruit;

    public int MaxFriutsAdd => maxFriutsAdd;

    public int MinFriutsAdd => minFriutsAdd;

    public int AddingFruitsForPoints => addingFruitsForPoints;

    public float Gravity => gravity;
    
    public float TimeAttenuation => timeAttenuation;

    public float IntervalBetweenEntitysLoss => intervalBetweenEntitysLoss;

    public float MinIntervalBetweenEntitysLoss => minIntervalBetweenEntitysLoss;

    public float RedusingInInterval => redusingInInterval;

    public float SpeedSlice => speedSlice;
}
