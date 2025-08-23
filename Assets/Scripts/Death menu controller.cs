using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenuController : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _retry;
    [SerializeField] private Button _quitButton;

    private void Start()
    {
        _retry.onClick.AddListener(RetryGame);
        _quitButton.onClick.AddListener(QuitGame);

        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.None;

    }

    private void RetryGame()
    {
        SceneManager.LoadScene("Main Menu");
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
