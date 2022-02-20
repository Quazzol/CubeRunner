using UnityEngine;

public class GameManager : MonoBehaviour
{
    private CameraController _cameraController = null;
    private LevelManager _levelManager = null;
    private Player _player = null;
    private GameUIManager _uiManager = null;
    private GameState _state = GameState.Ready;

    private void Awake()
    {
        _cameraController = GetComponent<CameraController>();
        _levelManager = GetComponent<LevelManager>();
        _uiManager = GetComponent<GameUIManager>();

        CreatePlayer();

        InputController.Click += OnClicked;

        _uiManager.BackClicked += OnMenuBackClicked;
        _uiManager.PlayClicked += OnMenuPlayClicked;
    }

    private void Start()
    {
        PrepareLevel();
    }

    private void OnDisable()
    {
        InputController.Click -= OnClicked;

        _player.Collected -= OnPlayerCollected;
        _player.Damaged -= OnPlayerDamaged;
        _player.Won -= OnPlayerWon;
        _player.Lost -= OnPlayerLost;

        _uiManager.BackClicked -= OnMenuBackClicked;
        _uiManager.PlayClicked -= OnMenuPlayClicked;

        PoolOwner.ClearPool();
    }

    private void PrepareLevel()
    {
        ParticleCreator.Instance.Reset();
        _levelManager.CreateLevel();

        var character = _player.CreatePlayableCharacter(CharacterType.Standard);

        _cameraController.SetObjectToFollow(character.transform);
        _cameraController.StartFollowing();

        _uiManager.UpdateLives(_player.Health);
        _uiManager.UpdateCollected(0);
    }

    private void CreatePlayer()
    {
        _player = new Player();
        _player.Collected += OnPlayerCollected;
        _player.Damaged += OnPlayerDamaged;
        _player.Won += OnPlayerWon;
        _player.Lost += OnPlayerLost;
    }

    private void SetState(GameState state)
    {
        _state = state;
    }

    private void OnClicked()
    {
        if (_state == GameState.Play)
            return;

        _player.Start();
        SetState(GameState.Play);
    }

    private void OnPlayerCollected(int collected)
    {
        _uiManager.UpdateCollected(collected);
    }

    private void OnPlayerDamaged(int health)
    {
        _uiManager.UpdateLives(health);
    }

    private void OnPlayerWon(int collected)
    {
        _cameraController.StartRotating();
        _uiManager.ShowVictoryMenu(collected, _player.Level, _player.TotalCollected);
    }

    private void OnPlayerLost(int lostCollectable)
    {
        _uiManager.ShowLoseMenu(lostCollectable, _player.Level, _player.TotalCollected);
    }

    private void OnMenuBackClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNames.Menu);
    }

    private void OnMenuPlayClicked()
    {
        PrepareLevel();
        SetState(GameState.Ready);
    }
}