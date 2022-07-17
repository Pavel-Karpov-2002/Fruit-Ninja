using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComboSeriesUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countFruits;
    [SerializeField] private TextMeshProUGUI comboSeries;
    [SerializeField] private GameSettings settings;

    private float timeToFade;
    private float fade;
    private float alphaCanvasGroup;

    private void Start()
    {
        alphaCanvasGroup = GetComponent<CanvasGroup>().alpha;

        if (GetComponent<CanvasGroup>() != null)
            GetComponent<CanvasGroup>().alpha = 0;
        else
            Debug.Log("CanvasGroup not found! Add canvasGroup.");
    }

    private void Update()
    {
        if (timeToFade < settings.ComboSettings.TimeDisappearanceComboText)
        {
            timeToFade += Time.deltaTime;
        }else if(alphaCanvasGroup > 0)
        {
            ChangeFade();
        }
    }

    public void ChangeSeriesText(int countSeries)
    {
        GetComponent<CanvasGroup>().alpha = 1;

        comboSeries.text = "X" + countSeries.ToString();
        countFruits.text = countSeries.ToString() + " фруктов";

        timeToFade = 0;
        fade = 1;
    }

    private void ChangeFade()
    {
        fade -= Time.deltaTime;
        GetComponent<CanvasGroup>().alpha = fade;
     }
}
