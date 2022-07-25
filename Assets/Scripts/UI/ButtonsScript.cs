using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsScript : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;
    [SerializeField] private Image spriteAttenuation;
    [SerializeField] private TextMeshProUGUI record;

    private Sequence _sequence;

    private void Start()
    {
        _sequence = DOTween.Sequence();
        _sequence.Append(spriteAttenuation.DOFade(0, gameSettings.TimeAttenuation).SetEase(Ease.Linear));
        StartCoroutine(ChangeActiveAttenuation(false, gameSettings.TimeAttenuation));
        GetRecords();
    }

    public void StartGame()
    {
        StartCoroutine(ChangeActiveAttenuation(true, 0));

        _sequence = DOTween.Sequence();

        _sequence.Append(spriteAttenuation.DOFade(1, gameSettings.TimeAttenuation).SetEase(Ease.Linear));

        StartCoroutine(TimeAttenuaton("GamePlayScene"));
    }

    public void MainMenu()
    {
        StartCoroutine(ChangeActiveAttenuation(true, 0));
        
        _sequence = DOTween.Sequence();

        _sequence.Append(spriteAttenuation.DOFade(1, gameSettings.TimeAttenuation).SetEase(Ease.Linear));

        StartCoroutine(TimeAttenuaton("MainMenuScene"));
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }

    private IEnumerator TimeAttenuaton(string loadScene)
    {
        yield return new WaitForSeconds(gameSettings.TimeAttenuation);

        SceneManager.LoadScene(loadScene);
    }

    private void GetRecords()
    {
        record.text = CoreValues.Record.ToString();
    }

    private IEnumerator ChangeActiveAttenuation(bool active, float time)
    {
        yield return new WaitForSeconds(time);

        spriteAttenuation.gameObject.SetActive(active);
    }
}
