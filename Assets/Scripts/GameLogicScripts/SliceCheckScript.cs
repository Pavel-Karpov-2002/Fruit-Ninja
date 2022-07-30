using UnityEngine;

public class SliceCheckScript : MonoBehaviour
{
    [SerializeField] private GameSettings settings;

    private Vector2 _lastMousePos;
    private float _velocity;

    private void Update()
    {
        if(Input.GetMouseButton(0) && CoreValues.HealthCount > 0 && _lastMousePos.x > 0 && _lastMousePos.y > 0)
        {
            _velocity = Vector2.Distance(GetPositionMouseOnWorld(), _lastMousePos) * Time.deltaTime;
        }

        if (_velocity * 10000 >= settings.SpeedSlice)
        {
            OnTriggerCollider();
        }

        _velocity = -1;
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
