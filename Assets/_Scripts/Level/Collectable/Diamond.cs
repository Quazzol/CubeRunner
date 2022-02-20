
using UnityEngine;

public class Diamond : PooledMonoBehaviour, ICollectable
{
    public int Reward => _reward;
    public override int InitialPoolSize => 100;
    
    [SerializeField] private int _reward = 3;

    public void Collect()
    {
        ParticleCreator.Instance.PlayCollectableParticle(transform.position);
        this.Disable();
    }
    
}