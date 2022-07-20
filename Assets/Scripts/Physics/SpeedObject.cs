
using UnityEngine;

public class SpeedObject : MonoBehaviour
{
    private static float _speed;

    public static float Speed => _speed;

    public static void ChangeSpeed(float addSpeed)
    {
        _speed = addSpeed;
    }
}
