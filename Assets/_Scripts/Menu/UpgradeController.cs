public class UpgradeController
{
    public int TotalCollected => _player.TotalCollected;
    public int Level => _player.Level;
    public int Health => _player.Health;
    public int Multiplier => _player.CollectionMultiplier;

    private UpgradeInfo _info;
    private Player _player;

    public UpgradeController()
    {
        _player = new Player();
        _info = new UpgradeInfo();
    }

    public bool IsHealthUpgradable()
    {
        return _info.GetHealthIncreaseCost(_player.Health) <= _player.TotalCollected;
    }

    public bool IsMultiplierUpgradable()
    {
        return _info.GetMultiplierIncreaseCost(_player.CollectionMultiplier) <= _player.TotalCollected;
    }

    public void UpgradeHealth()
    {
        _player.IncreaseHealth(_info.GetHealthIncreaseCost(_player.Health));
    }

    public void UpgradeMultiplier()
    {
        _player.IncreaseCollectionMultiplier(_info.GetMultiplierIncreaseCost(_player.CollectionMultiplier));
    }
}
