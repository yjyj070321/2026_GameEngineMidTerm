using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingUI : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}