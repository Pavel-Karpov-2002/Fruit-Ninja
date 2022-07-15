using UnityEngine;
using System.Collections;

public class Physics : MonoBehaviour
{
    private float _timeStart;

    private float _impuls;

    private float _gravity;
    private float downtime;

    public float Impuls
    {
        get { return _impuls; }
        set { _impuls = value; }
    }

    public float Gravity
    {
        get { return _gravity; }
        set { _gravity = value; }
    }

    private void Start()
    {
        _timeStart = Time.realtimeSinceStartup;
    }

    static float ConvertToRadian(float angle)
    {
        return (angle * Mathf.PI) / 180;
    }

    private float CountingPositionX(float angle, float t)
    {
        return _impuls * Mathf.Cos(angle) * t;
    }

    private float CountingPositionY(float angle, float t)
    {
        return _impuls * Mathf.Sin(angle) * t - ((_gravity * t * t) / 2);
    }

    private Vector3 CalculateVector(GameObject objectGame, float angle, float t)
    {
        float x = CountingPositionX(ConvertToRadian(angle), t);
        float y = CountingPositionY(ConvertToRadian(angle), t);

        return new Vector3(x, y, 0);
    }

    private IEnumerator Cast(float angle, Vector3 startPosition)
    {
        yield return null;

        float t = Time.realtimeSinceStartup - _timeStart - downtime;
        if(_gravity != 0)
            transform.position = CalculateVector(gameObject, angle, t) + startPosition;
        else
            downtime += Time.deltaTime;

        StartCoroutine(Cast(angle, startPosition));
    }

    public void AddImpulse(float angle, float impuls, float gravity, Vector3 startPosition)
    {
        _gravity = gravity;
        _impuls = impuls;
        StartCoroutine(Cast(angle, startPosition));
    }
}
