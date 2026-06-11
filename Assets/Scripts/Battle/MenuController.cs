using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    public IPanel panel;

    Canvas currentCanvas;

    public void Start()
    {
        currentCanvas = GetComponent<Canvas>();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MoveToScene(string name)
    {
        SceneManager.LoadSceneAsync(name);
    }
}
