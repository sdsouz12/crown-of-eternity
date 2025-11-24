using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] string gameSceneName = "GameLevel";
    public void StartGame()
    {
        Debug.Log("Start pressed");
        Time.timeScale = 1f;
        SceneManager.LoadScene(gameSceneName);
    }
}
