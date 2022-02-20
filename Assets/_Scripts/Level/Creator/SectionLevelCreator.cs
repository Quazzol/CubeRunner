using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SectionLevelCreator : BaseLevelCreator
{
    private List<IPooledMonoBehaviour> _sectionPrefabs;

    public SectionLevelCreator() : base()
    {}

    protected override void FillPrefabsList()
    {
        base.FillPrefabsList();

        _sectionPrefabs = new List<IPooledMonoBehaviour>();
        _sectionPrefabs.AddRange(Resources.LoadAll<PooledMonoBehaviour>("Prefabs/MidSections"));
    }

    protected override float CreateMidSection(GameObject parent, float pathLength)
    {
        float lastLength = 0;

        while (true)
        {
            var section = _sectionPrefabs[Random.Range(0, _sectionPrefabs.Count)].Get<ILevelSection>(parent.transform);
            section.transform.position = new Vector3(0, 0, pathLength);
            lastLength = section.Length;
            pathLength += section.Length;

            if (_levelLength <= pathLength)
                break;
        }

        return pathLength;
    }

}