using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button resumeButton;

    void Awake()
    {
        SetPaused(false);                    // <-- guarantees hidden at start
        if (resumeButton) {                  // optional: wire button in code
            resumeButton.onClick.RemoveAllListeners();
            resumeButton.onClick.AddListener(Resume);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }

    public void TogglePause() => SetPaused(!pausePanel.activeSelf);
    public void Resume() => SetPaused(false);

    private void SetPaused(bool pause)
    {
        pausePanel.SetActive(pause);
        Time.timeScale = pause ? 0f : 1f;
        Cursor.visible = pause;
        Cursor.lockState = pause ? CursorLockMode.None : CursorLockMode.Locked;
        if (pause && resumeButton) resumeButton.Select();
    }
}
