using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuScript : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ResumeGame()
    {
        var sceneNumber = PlayerPrefs.GetInt("savedScene", 1);
        if (sceneNumber == 5)
            sceneNumber = 1;
        SceneManager.LoadScene(sceneNumber);
    }
}
