public interface IObstacle : IPooledMonoBehaviour
{
    int Damage { get; }

    void Stumble();
}