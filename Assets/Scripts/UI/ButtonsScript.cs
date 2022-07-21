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

        _sequence.Append(spriteAttenuation.DOFade(1, gameSettings.TimeAttenuation).SetEase(Ease.Linear));

        StartCoroutine(TimeAttenuaton(1));
    }

    public void MainMenu()
    {
        StartCoroutine(ChangeActiveAttenuation(true, 0));

        _sequence.Append(spriteAttenuation.DOFade(1, gameSettings.TimeAttenuation).SetEase(Ease.Linear));

        StartCoroutine(TimeAttenuaton(0));
    }

    public void ExitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    private IEnumerator TimeAttenuaton(int loadSceneNumber)
    {
        yield return new WaitForSeconds(gameSettings.TimeAttenuation);

        SceneManager.LoadScene("GamePlayScene");
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
