using UnityEngine;

public class SavingValues : MonoBehaviour
{
    private const string record = "Record";

    public static void SaveGame()
    {
        PlayerPrefs.SetInt(record, CoreValues.Record);
        PlayerPrefs.Save();
    } 

    public static void LoadGame()
    {
        if (PlayerPrefs.HasKey(record))
        {
            CoreValues.Record = PlayerPrefs.GetInt(record);
        }
        else
            Debug.LogError("There is no save data!");
    }
}
