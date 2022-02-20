using System.Linq;

public class LevelSection : PooledMonoBehaviour, ILevelSection
{
    public float Length => _length;

    float _length;

    private void Awake()
    {
        var surfaces = GetComponentsInChildren<ISurface>();
        _length = surfaces.Sum(q => q.Length);
    }
}