using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ParticleCreator : IDisposable
{
    private static ParticleCreator _instance = null;
    public static ParticleCreator Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ParticleCreator();
            }

            return _instance;
        }
    }

    private List<ParticleObstacle> _obstaclePrefabs;
    private List<ParticleCollectable> _collectablePrefabs;
    private List<ParticleCelebration> _celebrationPrefabs;
    private GameObject _parent = null;
    private GameObject Parent
    {
        get
        {
            if (_parent == null)
            {
                _parent = new GameObject();
                _parent.name = "Particle Parent";
            }

            return _parent;
        }
    }

    private ParticleCreator()
    {
        _obstaclePrefabs = new List<ParticleObstacle>();
        _collectablePrefabs = new List<ParticleCollectable>();
        _celebrationPrefabs = new List<ParticleCelebration>();
        FillPrefabs();
    }

    public void PlayObstacleParticle(Vector3 position)
    {
        var particle = _obstaclePrefabs[Random.Range(0, _obstaclePrefabs.Count)].Get<IPooledMonoBehaviour>(Parent.transform, position, Quaternion.identity);
        particle.DisableWithTime(1f);
    }

    public void PlayCollectableParticle(Vector3 position)
    {
        var particle = _collectablePrefabs[Random.Range(0, _collectablePrefabs.Count)].Get<IPooledMonoBehaviour>(Parent.transform, position, Quaternion.identity);
        particle.DisableWithTime(1f);
    }

    public void PlayCelebrationParticle(Vector3 position)
    {
        var particle = _celebrationPrefabs[Random.Range(0, _celebrationPrefabs.Count)].Get<IPooledMonoBehaviour>(Parent.transform, position, Quaternion.identity);
        particle.DisableWithTime(7f);
    }

    public void Reset()
    {
        if (_parent != null)
        {
            _parent.DisableAllChildren();
            GameObject.Destroy(_parent);
            _parent = null;
        }
    }

    private void FillPrefabs()
    {
        var objects = Resources.LoadAll<PooledMonoBehaviour>("Prefabs/Particles");
        foreach (var go in objects)
        {
            if (go.TryGetComponent<ParticleObstacle>(out var obstacle))
            {
                _obstaclePrefabs.Add(obstacle);
                continue;
            }

            if (go.TryGetComponent<ParticleCollectable>(out var collectable))
            {
                _collectablePrefabs.Add(collectable);
                continue;
            }

            if (go.TryGetComponent<ParticleCelebration>(out var celebration))
            {
                _celebrationPrefabs.Add(celebration);
                continue;
            }
        }
    }

    public void Dispose()
    {
        _instance = null;
    }
}