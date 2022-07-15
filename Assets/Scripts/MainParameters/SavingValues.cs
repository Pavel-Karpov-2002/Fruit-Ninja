using UnityEngine;

public class SavingValues : MonoBehaviour
{
    public static void SaveGame()
    {
        PlayerPrefs.SetInt("Record", CoreValues.Record);
        PlayerPrefs.Save();
        Debug.Log("Game data saved!" + " " + CoreValues.Record);
    } 

    public static void LoadGame()
    {
        if (PlayerPrefs.HasKey("Record"))
        {
            CoreValues.Record = PlayerPrefs.GetInt("Record");
        }
        else
            Debug.LogError("There is no save data!");
    }
}
