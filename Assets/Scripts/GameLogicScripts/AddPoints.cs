using UnityEngine;

public class AddPoints : MonoBehaviour
{
    public static void Add(int count)
    {
        CoreValues.NumberOfPoints += count;

        if (CoreValues.NumberOfPoints > CoreValues.Record)
        {
            CoreValues.Record = CoreValues.NumberOfPoints;
            SavingValues.SaveGame();
        }
    }
}
