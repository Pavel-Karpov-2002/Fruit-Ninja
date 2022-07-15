using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ChangeFade : MonoBehaviour
{
    public static void AddAttenuation(GameObject spriteAttenuation, float time, float alpha)
    {
        spriteAttenuation.SetActive(true);

        spriteAttenuation.GetComponent<Image>().DOFade(alpha, time).SetEase(Ease.Linear);
    }
}
