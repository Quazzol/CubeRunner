using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    [Header("Text Fields")]
    [SerializeField] private TextMeshProUGUI _txtCollected;
    [SerializeField] private TextMeshProUGUI _txtLevel;
    [SerializeField] private TextMeshProUGUI _txtHealth;
    [SerializeField] private TextMeshProUGUI _txtCollectionMultiplier;

    [Header("Buttons")]
    [SerializeField] private Button _btnIncreaseHealth;
    [SerializeField] private Button _btnIncreaseCollectionMultiplier;
    [SerializeField] private Button _btnPlay;

    private UpgradeController _upgradeController = null;
    
    private void Start()
    {
        _upgradeController = new UpgradeController();

        _btnIncreaseHealth.onClick.AddListener(OnIncreaseHealth);
        _btnIncreaseCollectionMultiplier.onClick.AddListener(OnIncreaseMultiplier);
        _btnPlay.onClick.AddListener(OnPlayClicked);

        UpdateTextFields();
        SetButtonEnability();
    }

    private void UpdateTextFields()
    {
        _txtCollected.text = _upgradeController.TotalCollected.ToString();
        _txtLevel.text = _upgradeController.Level.ToString();
        _txtHealth.text = _upgradeController.Health.ToString();
        _txtCollectionMultiplier.text = _upgradeController.Multiplier.ToString();
    }

    private void SetButtonEnability()
    {
        _btnIncreaseHealth.interactable = _upgradeController.IsHealthUpgradable();
        _btnIncreaseCollectionMultiplier.interactable = _upgradeController.IsMultiplierUpgradable();
    }

    private void OnIncreaseHealth()
    {
        _upgradeController.UpgradeHealth();
        UpdateTextFields();
        SetButtonEnability();
    }

    private void OnIncreaseMultiplier()
    {
        _upgradeController.UpgradeMultiplier();
        UpdateTextFields();
        SetButtonEnability();
    }

    private void OnPlayClicked()
    {
        SceneManager.LoadScene(SceneNames.Game);
    }
}
