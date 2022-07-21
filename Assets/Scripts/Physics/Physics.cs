using UnityEngine;
using System.Collections;

public class Physics : MonoBehaviour
{
    private float _timeLive;

    private float _impuls;

    private float _gravity;

    private Vector3 _startPosition;

    private float _angle;

    private float _speed;

    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

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

    public float Angle
    {
        get { return _angle; }
        set { _angle = value; }
    }

    private void Start()
    {
        _timeLive = 0;

    }

    private float ConvertToRadian(float angle)
    {
        return angle * Mathf.PI / 180;
    }
    
    private float CountingPositionX()
    {
        return _impuls * Mathf.Cos(_angle) * _timeLive;
    }

    private float CountingPositionY()
    {
        return _impuls * Mathf.Sin(_angle) * _timeLive - ((_gravity * _timeLive * _timeLive) / 2);
    }

    private Vector3 CalculateVector()
    {
        float x = CountingPositionX();
        float y = CountingPositionY();

        return new Vector3(x, y, 0);
    }

    public void ChangeSpeed(float speed)
    {
        _speed = speed;
    }

    private IEnumerator Cast()
    {
        yield return null;

        if(_gravity != 0)
        {
            _timeLive += Time.deltaTime * (SpeedObject.Speed + _speed);

            transform.position = CalculateVector() + _startPosition;
        }

        StartCoroutine(Cast());
    }

    public void AddImpulse(float angle, float impuls, float gravity, Vector3 startPosition)
    {
        _gravity = gravity;
        _impuls = impuls;
        _startPosition = startPosition;

        _angle = ConvertToRadian(angle);

        StartCoroutine(Cast());
    }
}
