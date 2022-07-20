using UnityEngine;
using System.Collections;

public class SliceCheckScript : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;

    private static bool _blockSlice;
    private Vector2 _lastMousePos;
    private float _AxisX;
    private float _AxisY;

    public static bool BlockSlice
    {
        get { return _blockSlice; }
        set { _blockSlice = value; }
    }

    private void Update()
    {
        /*for (int i = 0; i < Input.touchCount; ++i)
        {            
            if ((Input.GetTouch(i).phase == TouchPhase.Began || Input.GetMouseButton(0)) && CoreValues.HealthCount > 0)
            {
                float speed = Input.GetTouch(i).deltaPosition.x / Time.deltaTime;
                if(speed >= gameSettings.SpeedSlice)
                {
                    OnTriggerCollider();
                }
            }
        }*/

        GetSpeedMouse();

        if (Input.GetMouseButton(0) && CoreValues.HealthCount > 0 && !_blockSlice && (_AxisX > gameSettings.SpeedSlice || _AxisY > gameSettings.SpeedSlice))
            OnTriggerCollider();
    }

    private void GetSpeedMouse()
    {
        if (_lastMousePos == Vector2.zero)
        {
            _lastMousePos = Input.mousePosition;
        }
        else
        {
            _AxisX = ((Input.mousePosition.x - _lastMousePos.x) / Time.deltaTime) / Screen.width;
            _AxisY = ((Input.mousePosition.y - _lastMousePos.y) / Time.deltaTime) / Screen.height;
            _lastMousePos = Input.mousePosition;
        }
    }

    private void OnTriggerCollider()
    {
        foreach (Unit entity in PullObjects.Units)
        {
            if (CheckSliceCollider(entity))
                break;
        }
    }

    private bool CheckSliceCollider(Unit entity)
    {
        if (entity.ColliderSphere != null)
        {
            if (entity.ColliderSphere.HittingCollider(entity.RadiusCollider))
            {
                entity.Slice.StartEntry = entity.ColliderSphere.GetLengthVector();
            }
            else if (entity.Slice.StartEntry != 0 && entity.Slice.EndEntry == 0)
            {
                entity.Slice.EndEntry = entity.ColliderSphere.GetLengthVector();
                if (entity.Slice.IsCut(entity.RadiusCollider))
                {
                    entity.Destruction();
                    return true;
                }
            }
            else
            {
                entity.Slice.StartEntry = 0;
                entity.Slice.EndEntry = 0;
            }
        }

        return false;
    }
}
