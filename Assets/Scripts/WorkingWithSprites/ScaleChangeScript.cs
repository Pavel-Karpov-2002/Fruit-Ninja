using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class ScaleChangeScript : MonoBehaviour
{
    public static void Change(Transform objectChange, float toScale, float timeChange)
    {
        objectChange.DOScale(toScale, timeChange).SetEase(Ease.Linear);
    }

    public static void ChangeRectangleSize(RectTransform objectChange, Vector2 toScale, float timeChange)
    {
        objectChange.DOSizeDelta(new Vector2(toScale.x, toScale.y), timeChange);
    }

    public async static void DelayedChange(Transform objectChange, float toScale, float timeChange, float timeDelay)
    {
        await Task.Delay((int)(timeDelay * 1000));

        objectChange.DOScale(toScale, timeChange).SetEase(Ease.Linear);
    }

    public static void ChangeOnWindow(Transform transform, float minScale, float maxScale)
    {
        float normalScale = ((transform.position.y) / WorldSizeCamera.HalfHeight);

        if (normalScale > maxScale)
        {
            normalScale = maxScale;
        }
        else if (normalScale < minScale)
        {
            normalScale = minScale;
        }

        Change(transform, normalScale, 0);
    }
}
