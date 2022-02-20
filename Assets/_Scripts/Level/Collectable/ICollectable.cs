public interface ICollectable : IPooledMonoBehaviour
{
    int Reward { get; }

    void Collect();
}