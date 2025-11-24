using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel; // assign GameOverPanel
    [SerializeField] private Button restartButton;     // assign RestartButton (Button)

    void Awake()
    {
        if (gameOverPanel) gameOverPanel.SetActive(false);

        if (restartButton)
        {
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(RestartLevel);
        }

        // make sure this canvas renders on top (useful if Screen Space - Camera)
        var canvas = GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.overrideSorting = true;
            canvas.sortingOrder = 1000;
        }
    }

    public void ShowGameOver()
    {
        if (gameOverPanel) gameOverPanel.SetActive(true);

        // If there's a CanvasGroup, ensure it's visible & clickable
        var cg = gameOverPanel ? gameOverPanel.GetComponent<CanvasGroup>() : null;
        if (cg) { cg.alpha = 1f; cg.interactable = true; cg.blocksRaycasts = true; }

        Time.timeScale = 0f;                 // pause the game
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Optional: return to main menu
    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main_Menu");
    }
}
