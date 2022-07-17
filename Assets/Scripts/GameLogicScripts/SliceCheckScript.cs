using UnityEngine;
using System.Collections;

public class SliceCheckScript : MonoBehaviour
{
    [SerializeField] private GamePlayEvents player;
    [SerializeField] private GameSettings gameSettings;

    private static bool blockSlice;
    private Vector2 lastMousePos;
    private float AxisX;
    private float AxisY;

    public static bool BlockSlice
    {
        get { return blockSlice; }
        set { blockSlice = value; }
    }

    public void Update()
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

        if (Input.GetMouseButton(0) && CoreValues.HealthCount > 0 && !blockSlice && (AxisX > gameSettings.SpeedSlice || AxisY > gameSettings.SpeedSlice))
            OnTriggerCollider();
    }

    private void GetSpeedMouse()
    {
        if (lastMousePos == Vector2.zero)
        {
            lastMousePos = Input.mousePosition;
        }
        else
        {
            AxisX = ((Input.mousePosition.x - lastMousePos.x) / Time.deltaTime) / Screen.width;
            AxisY = ((Input.mousePosition.y - lastMousePos.y) / Time.deltaTime) / Screen.height;
            lastMousePos = Input.mousePosition;
        }
    }

    private void OnTriggerCollider()
    {
        foreach (Entity entity in player.Entitys)
        {
            if (CheckSliceCollider(entity))
                break;
        }
    }

    private bool CheckSliceCollider(Entity entity)
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
