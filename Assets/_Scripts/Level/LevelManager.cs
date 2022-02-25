using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float LevelLength => _totalLength;
    [SerializeField] LevelType _levelType = LevelType.Section;

    ILevelCreator _levelCreator = null;

    private float _totalLength;

    private void Awake()
    {
        _levelCreator = LevelFactory.GetLevelCreator(_levelType);
    }

    public void CreateLevel()
    {
        _totalLength = _levelCreator.CreateLevel();
    }
}
