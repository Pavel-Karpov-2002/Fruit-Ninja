
public class Halve : Entity
{
    private void Start()
    {
        blade.Entitys.Add(this);
    }

    public override void Destruction()
    {
        return;
    }

    private void OnDestroy()
    {
        blade.Entitys.Remove(this);
    }
}
