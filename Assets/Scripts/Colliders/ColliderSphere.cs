using UnityEngine;

public class ColliderSphere : Collider
{
    public bool HittingCollider(float radius, Vector2 posStart, Vector2 posFinish)
    {
        Vector2 posX = new Vector2(Mathf.Min(posStart.x, posFinish.x), Mathf.Max(posStart.x, posFinish.x));
        Vector2 posY = new Vector2(Mathf.Min(posStart.y, posFinish.y), Mathf.Max(posStart.y, posFinish.y));



        if (GetLengthVector() >= radius && 
            transform.position.x >= posX.x && transform.position.x <= posX.y &&
             transform.position.y >= posY.x && transform.position.y <= posY.y)
        {
            return true;
        }
        else if (GetLengthVector() <= (radius / 2) && Vector2.Distance(posStart, posFinish) >= radius)
        {
            return true;
        }

        return false;
    }
}
