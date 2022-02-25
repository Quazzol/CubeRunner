using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class GameUIManager : MonoBehaviour
{
    public event Action PlayClicked;
    public event Action BackClicked;

    [Header("Menu")]
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private TextMeshProUGUI _txtGameStatus;
    [SerializeField] private TextMeshProUGUI _txtCurrentLevel;
    [SerializeField] private TextMeshProUGUI _txtTotalCollected;
    [Header("Buttons")]
    [SerializeField] private Button _btnPlay;
    [SerializeField] private Button _btnBack;
    [Header("Text Fields")]
    [SerializeField] private TextMeshProUGUI _txtLives;
    [SerializeField] private TextMeshProUGUI _txtCollected;

    [Header("Images")]
    [SerializeField] private Image _imgMapCoveredAmount;
    

    private void Start()
    {
        _btnPlay.onClick.AddListener(OnPlayClicked);
        _btnBack.onClick.AddListener(OnBackClicked);
    }

    private void OnDisable()
    {
        _btnPlay.onClick.RemoveListener(OnPlayClicked);
        _btnBack.onClick.RemoveListener(OnBackClicked);
    }

    public void UpdateCollected(int collected)
    {
        _txtCollected.text = collected.ToString();
    }

    public void UpdateLives(int lives)
    {
        _txtLives.text = lives.ToString();
    }

    public void UpdateMapPercent(float percent)
    {
        _imgMapCoveredAmount.fillAmount = percent;
    }

    public void ShowLoseMenu(int lostCollected, int currentLevel, int totalCollected)
    {
        ShowMenu($"You LOST! {lostCollected} gem returned to vault.", currentLevel, totalCollected);
    }

    public void ShowVictoryMenu(int wonCollected, int currentLevel, int totalCollected)
    {
        ShowMenu($"You WON! {wonCollected} added to your wallet.", currentLevel, totalCollected);
    }

    private void ShowMenu(string status, int currentLevel, int totalCollected)
    {
        _txtGameStatus.text = status;
        _txtCurrentLevel.text = currentLevel.ToString();
        _txtTotalCollected.text = totalCollected.ToString();
        _menuPanel.SetActive(true);
    }

    public void HideMenu()
    {
        _menuPanel.SetActive(false);
    }

    public void OnPlayClicked()
    {
        HideMenu();
        PlayClicked?.Invoke();
    }

    private void OnBackClicked()
    {
        HideMenu();
        BackClicked?.Invoke();
    }
}
