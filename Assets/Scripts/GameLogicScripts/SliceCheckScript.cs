using UnityEngine;

public class SliceCheckScript : MonoBehaviour
{
    [SerializeField] private GameSettings settings;

    private Vector2 _lastMousePos;
    private float _velocity;

    private void Update()
    {
        if(Input.GetMouseButton(0) && CoreValues.HealthCount > 0)
            _velocity = (GetPositionMouseOnWorld() - _lastMousePos).magnitude * Time.deltaTime;
        else
            _velocity = -1;

        if (_velocity * 10000 >= settings.SpeedSlice)
        {
            OnTriggerCollider();
        }

        GetSpeedMouse();
    }

    private void GetSpeedMouse()
    {
        _lastMousePos = GetPositionMouseOnWorld();
    }

    private Vector2 GetPositionMouseOnWorld()
    {
        return ScreenCoordinatesToWorld.ScreenToWorld(Input.mousePosition);
    }

    private void OnTriggerCollider()
    {
        int countUnits = PullObjects.Units.Count;
        for (int i = 0; i < countUnits; i++)
        {
            if (CheckSliceCollider(PullObjects.Units[i]))
            {
                break;
            }
        }
    }

    protected bool CheckSliceCollider(Unit entity)
    {
        if (entity.ColliderSphere.HittingCollider(entity.RadiusCollider))
        {
            entity.Destruction();
            return true;
        }

        return false;
    }
}
