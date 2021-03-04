using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject restartPanel;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject defaultPanel;
    void start()
    {
        restartPanel.SetActive(false);
        winScreen.SetActive(false);
        defaultPanel.SetActive(true);
    }
    public void restartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void homeScene()
    {
        SceneManager.LoadScene(sceneBuildIndex: 0);
    }

    public void showRestartPanel()
    {
        defaultPanel.SetActive(false);
        restartPanel.SetActive(true);
    }

    public void showWinScreen()
    {
        defaultPanel.SetActive(false);
        winScreen.SetActive(true);
    }
    public void nextFunction()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1)%11);
    }
    
    public void ExitFunction()
    {
        Application.Quit();
    }
}
