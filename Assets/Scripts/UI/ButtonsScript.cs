using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ButtonsScript : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;
    [SerializeField] private GameObject spriteAttenuation;
    [SerializeField] private TextMeshProUGUI record;

    private void Start()
    {
        GetRecords();
    }

    public void StartGame()
    {
        if(spriteAttenuation.activeSelf != false)
            spriteAttenuation.SetActive(false);

        spriteAttenuation.GetComponent<Image>().color = new Color(0, 0, 0, 0);

        ChangeFade.AddAttenuation(spriteAttenuation, gameSettings.TimeAttenuation, 1);

        StartCoroutine(TimeAttenuaton(1));

    }

    public void MainMenu()
    {
        if (spriteAttenuation.activeSelf != false)
            spriteAttenuation.SetActive(false);
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

    public void ChangeScaleButtonUp(GameObject objectButton)
    {
        Transform buttonTransform = objectButton.transform;
        Vector2 effectScale = new Vector2(buttonTransform.localScale.x - gameSettings.ChangeScaleButton, buttonTransform.localScale.y - gameSettings.ChangeScaleButton);

        buttonTransform.DOScale(effectScale, gameSettings.TimeChangeScaleButton);
    }

    private void GetRecords()
    {
        record.text = CoreValues.Record.ToString();
    }
}
