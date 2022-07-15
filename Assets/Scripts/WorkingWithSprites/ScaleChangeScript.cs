using UnityEngine;
using DG.Tweening;
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

    public static void ChangeOnWindow(Transform transform)
    {
        float normalScale = ((transform.position.y) / WorldSizeCamera.HalfHeight);

        if (normalScale > 0.86f)
        {
            normalScale = 0.86f;
        }
        else if (normalScale < 0.5f)
        {
            normalScale = 0.5f;
        }

        Change(transform, normalScale, 0);
    }
}
