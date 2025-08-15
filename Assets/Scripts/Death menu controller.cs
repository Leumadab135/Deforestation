using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
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
