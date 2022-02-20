using System;

[Serializable]
public class LevelFactory
{
    public static ILevelCreator GetLevelCreator(LevelType type)
    {
        switch (type)
        {
            case LevelType.Section: return new SectionLevelCreator();
            case LevelType.Random: return new RandomLevelCreator();
        }

        return null;
    }   
}