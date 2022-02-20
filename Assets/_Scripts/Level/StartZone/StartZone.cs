using System.Linq;

public class StartZone : PooledMonoBehaviour, IStartZone
{
    public override int InitialPoolSize => 1;

    public float Length => _length;

    float _length;

    private void Awake()
    {
        var surfaces = GetComponentsInChildren<ISurface>();
        _length = surfaces.Sum(q => q.Length);
    }
}