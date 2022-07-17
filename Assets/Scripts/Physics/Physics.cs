using UnityEngine;
using System.Collections;

public class Physics : MonoBehaviour
{
    private float _timeLive;

    private float _impuls;

    private float _gravity;

    private Vector3 _startPosition;

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

    public float TimeLive
    {
        get { return _timeLive; }
        set { _timeLive = value; }
    }

    public Vector3 StartPosition
    {
        get { return _startPosition; }
        set { _startPosition = value; }
    }

    private void Start()
    {
        _timeLive = 0;
        
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

    private IEnumerator Cast(float angle)
    {
        yield return null;

        if(_gravity != 0)
        {
            _timeLive += Time.deltaTime;
            transform.position = CalculateVector(gameObject, angle, _timeLive) + _startPosition;
        }

        StartCoroutine(Cast(angle));
    }

    public void AddImpulse(float angle, float impuls, float gravity, Vector3 startPosition)
    {
        _gravity = gravity;
        _impuls = impuls;
        _startPosition = startPosition;

        StartCoroutine(Cast(angle));
    }
}
