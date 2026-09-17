using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void SwitchScenes(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
