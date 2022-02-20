using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] LevelType _levelType = LevelType.Section;

    ILevelCreator _levelCreator = null;

    private void Awake()
    {
        _levelCreator = LevelFactory.GetLevelCreator(_levelType);
    }

    public void CreateLevel()
    {
        _levelCreator.CreateLevel();
    }
}
