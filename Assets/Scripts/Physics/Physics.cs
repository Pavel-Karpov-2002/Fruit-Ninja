using UnityEngine;
using System.Collections;

public class Physics : MonoBehaviour
{
    private float timeStart;

    private void Start()
    { 
        timeStart = Time.realtimeSinceStartup;
    }

    static float ConvertToRadian(float angle)
    {
        return (angle * Mathf.PI) / 180;
    }

    private static float CountingPositionX(float angle, float impuls, float t)
    {
        return impuls * Mathf.Cos(angle) * t;
    }

    private static float CountingPositionY(float angle, float impuls, float g, float t)
    {
        return impuls * Mathf.Sin(angle) * t - ((g * t * t) / 2);
    }

    private static Vector3 CalculateVector(GameObject objectGame, float angle, float impuls, float g, float t)
    {
        float x = CountingPositionX(ConvertToRadian(angle), impuls, t);
        float y = CountingPositionY(ConvertToRadian(angle), impuls, g, t);

        return new Vector3(x, y, 0);
    }

    private IEnumerator Cast(float angle, float impuls, float g, Vector3 startPosition)
    {
        float t = Time.realtimeSinceStartup - timeStart;
        transform.position = Physics.CalculateVector(gameObject, angle, impuls, g, t) + startPosition;

        yield return new WaitForFixedUpdate();

        StartCoroutine(Cast(angle, impuls, g, startPosition));
    }

    public void AddImpulse(float angle, float impuls, float g, Vector3 startPosition)
    {
        StartCoroutine(Cast(angle, impuls, g, startPosition));
    }
}
