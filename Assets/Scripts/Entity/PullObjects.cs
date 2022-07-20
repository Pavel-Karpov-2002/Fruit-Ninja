using System.Collections.Generic;

public static class PullObjects
{
    private static GamePlayEvents _gamePlayer;

    private static List<Unit> _units = new List<Unit>();

    private static List<Halve> _halves = new List<Halve>();

    private static List<SpawnerEntitys> _spawners = new List<SpawnerEntitys>();

    public static GamePlayEvents GamePlayer
    {
        get { return _gamePlayer; }
        set { _gamePlayer = value; }
    }

    public static List<Halve > Halves
    {
        get { return _halves; }
        set { _halves = value; }
    }

    public static List<Unit> Units
    {
        get { return _units; }
        set { _units = value; }
    }

    public static List<SpawnerEntitys> Spawners
    {
        get { return _spawners; }
        set { _spawners = value; }
    }
}
