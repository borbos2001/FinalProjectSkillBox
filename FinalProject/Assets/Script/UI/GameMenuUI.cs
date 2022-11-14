
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuUI : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {    
        Application.Quit();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
