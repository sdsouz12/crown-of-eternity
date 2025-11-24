using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] string gameSceneName = "DemoLevel";
    public void StartGame()
    {
        Debug.Log("Start pressed");
        Time.timeScale = 1f;
        SceneManager.LoadScene(gameSceneName);
    }
}
