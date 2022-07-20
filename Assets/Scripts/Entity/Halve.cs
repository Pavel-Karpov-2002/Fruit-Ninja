
public class Halve : Entity
{
    private void Start()
    {
        PullObjects.Halves.Add(this);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        PullObjects.Halves.Remove(this);
    }
}
