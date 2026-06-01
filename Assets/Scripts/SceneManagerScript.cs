using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    [SerializeField] private string sceneName1;
    [SerializeField] private string sceneName2;

    public void LoadLevel1()
    {
        SceneManager.LoadScene(sceneName1);
    }

    public void LoadSettings()
    {
        SceneManager.LoadScene(sceneName2);
    }
}
