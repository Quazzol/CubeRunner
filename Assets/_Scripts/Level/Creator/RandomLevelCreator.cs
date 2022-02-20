using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomLevelCreator : BaseLevelCreator
{
    private List<IPooledMonoBehaviour> _surfacePrefabs;
    private List<IPooledMonoBehaviour> _obstaclePrefabs;
    private List<IPooledMonoBehaviour> _collectablePrefabs;
    private float[] _possibleXCoordinates = {-1, 1};

    public RandomLevelCreator() : base()
    {}

    protected override void FillPrefabsList()
    {
        base.FillPrefabsList();

        _surfacePrefabs = new List<IPooledMonoBehaviour>();
        _obstaclePrefabs = new List<IPooledMonoBehaviour>();
        _collectablePrefabs = new List<IPooledMonoBehaviour>();

        _surfacePrefabs.AddRange(Resources.LoadAll<PooledMonoBehaviour>("Prefabs/Surfaces"));
        _obstaclePrefabs.AddRange(Resources.LoadAll<PooledMonoBehaviour>("Prefabs/Obstacles"));
        _collectablePrefabs.AddRange(Resources.LoadAll<PooledMonoBehaviour>("Prefabs/Collectables"));
    }

    protected override float CreateMidSection(GameObject parent, float pathLength)
    {
        int sectionStartPoint = Mathf.CeilToInt(pathLength / 2);
        pathLength = CreateSurface(parent, pathLength);
        int sectionEndPoint = Mathf.FloorToInt(pathLength - sectionStartPoint);

        var positions = CreateObstacles(parent, sectionStartPoint, sectionEndPoint);
        CreateCollectables(parent, sectionStartPoint, sectionEndPoint, positions);

        return pathLength;
    }

    private float CreateSurface(GameObject parent, float pathLength)
    {
        float lastLength = 0;

        while (true)
        {
            var surface = _surfacePrefabs[Random.Range(0, _surfacePrefabs.Count)].Get<ISurface>(parent.transform);
            surface.transform.position = new Vector3(0, 0, pathLength);
            lastLength = surface.Length;
            pathLength += surface.Length;

            if (_levelLength <= pathLength)
                break;
        }

        return pathLength;
    }

    private List<int> CreateObstacles(GameObject parent, int sectionStartPoint, int sectionEndPoint)
    {
        List<int> obstaclePositions = new List<int>();
        int minObstacleDistance = 5;
        int lastObstaclePosition = 0;
        float puttingObstacleChance = .3f;

        for (int i = sectionStartPoint; i < sectionEndPoint; i++)
        {
            if (lastObstaclePosition + minObstacleDistance > i)
                continue;

            if (Random.Range(0, 1f) > puttingObstacleChance)
                continue;

            var obstacle = _obstaclePrefabs[Random.Range(0, _obstaclePrefabs.Count)].Get<IObstacle>(parent.transform);
            obstacle.transform.position = new Vector3(_possibleXCoordinates[Random.Range(0, _possibleXCoordinates.Length)], 0, i);
            lastObstaclePosition = i;
            obstaclePositions.Add(i);
        }

        return obstaclePositions;
    }

    private void CreateCollectables(GameObject parent, int sectionStartPoint, int sectionEndPoint, List<int> obstaclePositions)
    {
        float puttingCollectableChance = .75f;

        for (int i = sectionStartPoint; i < sectionEndPoint; i++)
        {
            if (obstaclePositions.Contains(i))
                continue;

            if (Random.Range(0, 1f) > puttingCollectableChance)
                continue;

            var collectable = _collectablePrefabs[Random.Range(0, _collectablePrefabs.Count)].Get<ICollectable>(parent.transform);
            collectable.transform.position = new Vector3(_possibleXCoordinates[Random.Range(0, _possibleXCoordinates.Length)], .5f, i);
        }
    }
}