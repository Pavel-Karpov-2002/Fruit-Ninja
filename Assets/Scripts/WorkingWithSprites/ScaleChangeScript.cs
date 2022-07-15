using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Threading.Tasks;

public class ScaleChangeScript : MonoBehaviour
{
    public static void Change(Transform objectChange, float toScale, float timeChange)
    {
        objectChange.DOScale(toScale, timeChange).SetEase(Ease.Linear);
    }

    public async static void DelayedChange(Transform objectChange, float toScale, float timeChange, float timeDelay)
    {

        await Task.Delay((int)(timeDelay * 1000));

        objectChange.DOScale(toScale, timeChange).SetEase(Ease.Linear);
    }
}
