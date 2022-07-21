using UnityEngine;

public class AddPoints : MonoBehaviour
{
    public static void Add(int count)
    {
        CoreValues.NumberOfPoints += count;
    }
}
