
public class Halve : Entity
{
    private void Start()
    {
        player.Entitys.Add(this);
    }

    public override void Destruction()
    {
        return;
    }

    private void OnDestroy()
    {
        player.Entitys.Remove(this);
    }
}
