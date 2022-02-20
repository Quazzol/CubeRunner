using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player
{
    public event Action<int> Collected;
    public event Action<int> Damaged;
    public event Action<int> Won;
    public event Action<int> Lost;

    public int Level => _info.CurrentLevel;
    public int TotalCollected => _info.Collected;
    public int Health => _info.Health;
    public int CollectionMultiplier => _info.CollectionMultiplier;

    private Character _character;
    private List<Character> _characterPrefabs;
    private PlayerInfo _info;
    public Player()
    {
        _characterPrefabs = new List<Character>();
        LoadInfo();
        FillPrefabs();
    }

    private void LoadInfo()
    {
        _info = JsonDataSaver.Load<PlayerInfo>("player_data");
        if (_info == null)
        {
            _info = new PlayerInfo() {Collected = 0, CurrentLevel = 1, Health = 3, CollectionMultiplier = 1};
        }
    }

    private void FillPrefabs()
    {
        _characterPrefabs.AddRange(Resources.LoadAll<Character>("Prefabs/Characters"));
    }

    public void IncreaseHealth(int cost)
    {
        if (cost > _info.Collected)
            return;
        
        _info.Collected -= cost;
        _info.Health++;
        SaveInfo();
    }

    public void IncreaseCollectionMultiplier(int cost)
    {
        if (cost > _info.Collected)
            return;

        _info.Collected -= cost;
        _info.CollectionMultiplier++;
        SaveInfo();
    }

    private void SaveInfo()
    {
        JsonDataSaver.Save<PlayerInfo>("player_data", _info);
    }

    public Character CreatePlayableCharacter(CharacterType type)
    {
        if (_character == null || _character.CharacterType != type)
        {
            _character = _characterPrefabs.First(q => q.CharacterType == type).Get<Character>();
            _character.Collected += OnCollected;
            _character.Damaged += OnDamaged;
            _character.Finished += OnCharacterFinished;
            _character.Died += OnCharacterDied;
        }

        _character.Reset(new CharacterInfo(_info.Health, _info.CollectionMultiplier));
        return _character;
    }

    public void Start()
    {
        _character.Run();
    }

    private void OnCollected(int collected)
    {
        Collected?.Invoke(collected);
    }

    private void OnDamaged(int health)
    {
        Damaged?.Invoke(health);
    }

    private void OnCharacterFinished(int _collected)
    {
        _info.CurrentLevel++;
        _info.Collected += _collected;
        SaveInfo();
        Won?.Invoke(_collected);
    }

    private void OnCharacterDied(int _lostCollectable)
    {
        Lost?.Invoke(_lostCollectable);
    }
}