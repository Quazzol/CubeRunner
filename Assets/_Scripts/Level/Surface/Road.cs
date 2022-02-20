public class Road : PooledMonoBehaviour, ISurface
{
    public float Length => 7.62f;
    public override int InitialPoolSize => 5;
}