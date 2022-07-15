using UnityEngine;

public class HeartChange : MonoBehaviour
{
    public void ChangeScale(float scale)
    {
        transform.localScale = new Vector3(scale,scale);
    }

    public void ChangePosition(Transform heart)
    {
        transform.localScale = heart.localScale;
        transform.localRotation = heart.localRotation;
        transform.localPosition = heart.localPosition;
    }
}
