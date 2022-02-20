using UnityEngine;

public class Barrier : PooledMonoBehaviour, IObstacle
{
    public int Damage => _damage;
    public override int InitialPoolSize => 30;

    [SerializeField] private int _damage = 3;
    
    public void Stumble()
    {
        ParticleCreator.Instance.PlayObstacleParticle(transform.position);
        this.Disable();
    }
}