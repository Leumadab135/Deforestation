using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;

    private void Start()
    {
        _playButton.onClick.AddListener(LoadGame);
        _quitButton.onClick.AddListener(QuitGame);
    }

    private void LoadGame()
    {
        SceneManager.LoadScene("Main Scene");
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}

