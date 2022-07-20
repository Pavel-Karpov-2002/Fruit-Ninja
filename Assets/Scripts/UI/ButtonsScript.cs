using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class ButtonsScript : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;
    [SerializeField] private Image spriteAttenuation;
    [SerializeField] private TextMeshProUGUI record;

    private void Start()
    {
        GetRecords();
    }

    public void StartGame()
    {
        if(spriteAttenuation.gameObject.activeSelf != false)
            spriteAttenuation.gameObject.SetActive(false);

        spriteAttenuation.color = new Color(0, 0, 0, 0);

        ChangeFade.AddAttenuation(spriteAttenuation, gameSettings.TimeAttenuation, 1);

        StartCoroutine(TimeAttenuaton(1));

    }

    public void MainMenu()
    {
        if (spriteAttenuation.gameObject.activeSelf != false)
            spriteAttenuation.gameObject.SetActive(false);
        ChangeFade.AddAttenuation(spriteAttenuation, gameSettings.TimeAttenuation, 1);

        StartCoroutine(TimeAttenuaton(0));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private IEnumerator TimeAttenuaton(int loadSceneNumber)
    {
        yield return new WaitForSeconds(gameSettings.TimeAttenuation);

        SceneManager.LoadScene(loadSceneNumber);
    }

    private void GetRecords()
    {
        record.text = CoreValues.Record.ToString();
    }
}
