using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    [SerializeField] private string loadMainMenu;
    [SerializeField] private string loadSettings;
    [SerializeField] private string loadLevel1;
    [SerializeField] private string loadLevel2;
    [SerializeField] private string loadLevel3;
    [SerializeField] private string loadEnding;
    [SerializeField] private string loadCredits;

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
}
