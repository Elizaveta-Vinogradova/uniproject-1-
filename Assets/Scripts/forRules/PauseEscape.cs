using UnityEngine;

public class PauseEscape : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject allGameObjects;
    
    private bool paused;

    public void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        if (tutorialPanel.activeSelf)
            HideTutorial();
            
        else
        {
            paused = !paused;
            pausePanel.SetActive(paused);
            pauseButton.SetActive(!paused);
            allGameObjects.SetActive(!paused);
        }
    }

    public void Pause()
    {
        paused = true;
        pausePanel.SetActive(paused);
        pauseButton.SetActive(!paused);
        allGameObjects.SetActive(!paused);
    }

    private void HideTutorial()
    {
        tutorialPanel.SetActive(false);
        pausePanel.SetActive(true);
    }
}