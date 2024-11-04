using UnityEngine;
using UnityEngine.UI;

public class UIPopup_PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _popup;
    [SerializeField] private Button _retryButton;
    [SerializeField] private Button _exitButton;

    private void Start()
    {
        DisplayPauseMenu(false);
    }

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(ExitGame);
        _retryButton.onClick.AddListener(() => DisplayPauseMenu(false));
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(ExitGame);
        _retryButton.onClick.RemoveAllListeners();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_popup.activeSelf)
            {
                DisplayPauseMenu(false);
            }
            else
            {
                DisplayPauseMenu(true);
            }
        }

        if(Time.timeScale != 0)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void DisplayPauseMenu(bool enable)
    {
        _popup.SetActive(enable);

        Time.timeScale = enable ? 0f : 1f;
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
