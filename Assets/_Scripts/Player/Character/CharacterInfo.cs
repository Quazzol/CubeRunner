public class CharacterInfo
{
    public int Health { get; private set; }
    public int Collected { get; private set; }
    public int CollectionMultiplier { get; private set; }
    public bool IsDead => Health <= 0;

    public CharacterInfo(int health, int collectionMultiplier)
    {
        Health = health;
        Collected = 0;
        CollectionMultiplier = collectionMultiplier;
    }

    public void IncreaseCollected(int reward)
    {
        Collected += reward * CollectionMultiplier;
    }

    public void DecreaesHealth(int damage)
    {
        Health -= damage;
    }
}