
public static class CoreValues
{
    private static int record;
    private static int numberOfPoints;
    private static int healthCount;

    public static int Record
    {
        get { return record; }
        set { record = value; }
    }

    public static int HealthCount
    {
        get { return healthCount; }
        set { healthCount = value; }
    }

    public static int NumberOfPoints
    {
        get { return numberOfPoints; }
        set { numberOfPoints = value; }
    }
}
