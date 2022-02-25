using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class BaseLevelCreator : ILevelCreator
{
    protected (int, int) _duration = (15, 25); // How long is it going to take a level to complete, min-max
    protected float _levelLength;
    protected GameObject _path = null;
    protected List<IPooledMonoBehaviour> _startZonePrefabs;
    protected List<IPooledMonoBehaviour> _endZonePrefabs;

    public BaseLevelCreator()
    {
        _levelLength = Random.Range(_duration.Item1 * CharacterMovementController.MovementSpeed, _duration.Item2 * CharacterMovementController.MovementSpeed);

        FillPrefabsList();
    }

    protected virtual void FillPrefabsList()
    {
        _startZonePrefabs = new List<IPooledMonoBehaviour>();
        _endZonePrefabs = new List<IPooledMonoBehaviour>();

        _startZonePrefabs.AddRange(Resources.LoadAll<PooledMonoBehaviour>("Prefabs/StartSections"));
        _endZonePrefabs.AddRange(Resources.LoadAll<PooledMonoBehaviour>("Prefabs/EndSections"));
    }

    public float CreateLevel()
    {
        ReleaseCurrentLevel();

        _path = new GameObject();
        _path.name = "Path";
        
        float pathLength = 0;
        pathLength = CreateStartSection(_path);
        pathLength = CreateMidSection(_path, pathLength);
        CreateEndSection(_path, pathLength);

        return pathLength;
    }

    protected virtual void ReleaseCurrentLevel()
    {
        if (_path == null)
            return;

        _path.DisableAllChildren();
        GameObject.Destroy(_path);
        _path = null;
    }

    protected virtual float CreateStartSection(GameObject parent)
    {
        var startZone = _startZonePrefabs[Random.Range(0, _startZonePrefabs.Count)].Get<IStartZone>(parent.transform);
        startZone.transform.position = new Vector3(0, 0, 0);
        return startZone.Length;
    }

    protected abstract float CreateMidSection(GameObject parent, float pathLength);
    
    protected virtual void CreateEndSection(GameObject parent, float pathLength)
    {
        var endZone = _endZonePrefabs[Random.Range(0, _endZonePrefabs.Count)].Get<IPooledMonoBehaviour>(parent.transform);
        endZone.transform.position = new Vector3(0, 0, pathLength);
    }
}