using Assets.Scripts.Battle;
using Assets.Scripts.Model;
using Assets.Scripts.SceneControll;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    public GameObject externalMenu;

    Canvas currentCanvas;

    public void Start()
    {
        currentCanvas = GetComponent<Canvas>();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void EndTurn()
    {
        LivingEntity.Flag = false;
    }

    public void MoveToScene(string name)
    {
        //Debug.Log($"Moved to Scene {name}");
        SceneManager.LoadSceneAsync(name);
    }

    public void SetBattleData(BattleSettingsData data)
    {
        BattleSettingsStatic.SetData(data);
    }

    public void ShowExternalMenu()
    {
        externalMenu.SetActive(true);
    }
}
