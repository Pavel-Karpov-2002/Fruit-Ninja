using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Settings", fileName = "GameSettings")]
public class GameSettings : ScriptableObject
{
    [SerializeField] private List<FruitSettings> fruitSettings;

    [SerializeField] private List<Spawner> spawners;

    [SerializeField] private HealthSettings health;

    [SerializeField] private ScaleSettings scaleSettings;

    [SerializeField] private BlobSettings blobSettings;

    [SerializeField] private ComboSettings comboSettings;

    [SerializeField] private TextMeshProSettings textMeshProSettings;

    [SerializeField] private BombSettigns bombSettings;

    [SerializeField] private int numberOfPointsPerFruit;

    [SerializeField] private int maxFriutsAdd;

    [SerializeField] private int addingFruitsForPoints;

    [SerializeField] private float gravity;

    [SerializeField] private float timeAttenuation;

    [SerializeField] private float timeChangeScaleButton;

    [SerializeField] private float changeScaleButton;

    public HealthSettings Health => health;

    public List<FruitSettings> FruitSettings => fruitSettings;

    public List<Spawner> Spawners => spawners;

    public BlobSettings BlobSettings => blobSettings;

    public ComboSettings ComboSettings => comboSettings;

    public TextMeshProSettings TextMeshProSettings => textMeshProSettings;

    public BombSettigns BombSettings => bombSettings;

    public int NumberOfPointsPerFruit => numberOfPointsPerFruit;

    public int MaxFriutsAdd => maxFriutsAdd;

    public int AddingFruitsForPoints => addingFruitsForPoints;

    public float Gravity => gravity;

    public ScaleSettings ScaleSettings => scaleSettings;

    public float TimeAttenuation => timeAttenuation;

    public float TimeChangeScaleButton => timeChangeScaleButton;

    public float ChangeScaleButton => changeScaleButton;
}
