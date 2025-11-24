using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryController : MonoBehaviour
{
    [SerializeField] private GameObject victoryPanel;   // assign VictoryPanel (disabled by default)
    [SerializeField] private Button restartButton;      // assign RestartButton (Button)

    void Awake()
    {
        if (victoryPanel) victoryPanel.SetActive(false);

        if (restartButton)
        {
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(RestartLevel);
        }

        // make sure this canvas draws on top
        var canvas = GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.overrideSorting = true;   // important when using Screen Space - Camera
            canvas.sortingOrder   = 1000;
        }
    }

    public void ShowVictory()
    {
        Debug.Log("[VictoryController] ShowVictory()");
        if (victoryPanel)
        {
            victoryPanel.SetActive(true);

            // If there's a CanvasGroup, ensure it's visible/clickable
            var cg = victoryPanel.GetComponent<CanvasGroup>();
            if (cg != null) { cg.alpha = 1f; cg.interactable = true; cg.blocksRaycasts = true; }
        }
        else
        {
            Debug.LogWarning("[VictoryController] VictoryPanel not assigned!");
        }

        Time.timeScale = 0f;                 // pause gameplay
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Test key: press V to verify UI pops even without the boss
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) ShowVictory();
    }
}
