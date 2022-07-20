using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ChangeFade : MonoBehaviour
{
    public static void AddAttenuation(Image spriteAttenuation, float time, float alpha)
    {
        spriteAttenuation.gameObject.SetActive(true);

        spriteAttenuation.DOFade(alpha, time).SetEase(Ease.Linear);
    }
}
