using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    [Header("Scene Names")]
    [SerializeField] private string loadMainMenu;
    [SerializeField] private string loadSettings;
    [SerializeField] private string loadLevel1;
    [SerializeField] private string loadLevel2;
    [SerializeField] private string loadLevel3;
    [SerializeField] private string loadEnding;
    [SerializeField] private string loadCredits;

    [Header("Main Menu Button Groups")]
    [SerializeField] private GameObject mainMenuButtons;
    [SerializeField] private GameObject levelSelectButtons;

    private void Start()
    {
        if (mainMenuButtons != null)
        {
            mainMenuButtons.SetActive(true);
        }

        if (levelSelectButtons != null)
        {
            levelSelectButtons.SetActive(false);
        }
    }

    public void ShowLevelSelect()
    {
        if (mainMenuButtons != null)
        {
            mainMenuButtons.SetActive(false);
        }

        if (levelSelectButtons != null)
        {
            levelSelectButtons.SetActive(true);
        }
    }

    public void BackToMainButtons()
    {
        if (levelSelectButtons != null)
        {
            levelSelectButtons.SetActive(false);
        }

        if (mainMenuButtons != null)
        {
            mainMenuButtons.SetActive(true);
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(loadMainMenu);
    }

    public void LoadSettings()
    {
        SceneManager.LoadScene(loadSettings);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene(loadLevel1);
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene(loadLevel2);
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene(loadLevel3);
    }

    public void LoadEnding()
    {
        SceneManager.LoadScene(loadEnding);
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene(loadCredits);
    }

    public void QuitGame()
    {
        Debug.Log("Quit button pressed.");
        Application.Quit();
    }
}