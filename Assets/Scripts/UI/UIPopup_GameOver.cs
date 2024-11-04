using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPopup_GameOver : MonoBehaviour
{
    [SerializeField] private GameObject _popup;
    [SerializeField] private Button _restartButton;
    [SerializeField] private TextMeshProUGUI _resultText;
    [SerializeField] private string _lossResultTitle;
    [SerializeField] private string _victoryResultTitle;

    [Space]
    [SerializeField] private PlayerUnit _player;
    [SerializeField] private BossFattyUnit _bossFatty;

    private void Start()
    {
        _popup.SetActive(false);

        _player.Health.OnUnitDefeated += DisplayPopup;
        _bossFatty.Health.OnUnitDefeated += DisplayPopup;
    }

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(RestartGame);
    }

    private void OnDisable()
    {
        _player.Health.OnUnitDefeated -= DisplayPopup;
        _bossFatty.Health.OnUnitDefeated -= DisplayPopup;

        _restartButton.onClick.RemoveListener(RestartGame);
    }

    private void DisplayPopup()
    {
        _popup.SetActive(true);

        if(_player.Health.HealthPoints == 0)
        {
            _resultText.text = _lossResultTitle;
            _resultText.color = Color.red;
        }
        else
        {
            _resultText.text = _victoryResultTitle;
            _resultText.color = Color.green;
        }

        Time.timeScale = 0f;
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // rough variant
    }
}
