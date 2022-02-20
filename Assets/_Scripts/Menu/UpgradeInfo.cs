using System;

public class UpgradeInfo
{
    private const int BaseHealthIncreaseCost = 50;
    private const int BaseMultiplierIncreaseCost = 60;

    public int GetHealthIncreaseCost(int currentLevel)
    {
        currentLevel++;
        return (int)Math.Pow(currentLevel, 2) * BaseHealthIncreaseCost;
    }

    public int GetMultiplierIncreaseCost(int currentLevel)
    {
        currentLevel++;
        return (int)Math.Pow(currentLevel, 3) * BaseHealthIncreaseCost;
    }
}