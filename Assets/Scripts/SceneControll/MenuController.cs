using Assets.Scripts.Battle;
using Assets.Scripts.Model;
using Assets.Scripts.SceneControll;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject loseMenu;
    public GameObject victoryMenu;
    public TextAsset asset0;
    public TextAsset asset1;

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

    public void MoveToNextGameplayScene()
    {
        SceneManager.LoadSceneAsync(StaticNextSceneSetting.SceneName);
    }

    public void MoveToScene(string name)
    {
        SceneManager.LoadSceneAsync(name);
    }

    public void SetBattleData(BattleSettingsData data)
    {
        BattleSettingsStatic.SetData(data);
    }

    public void SetDialogueData(string filename)
    {
        StaticNextSceneSetting.menu = this;
        StaticNextSceneSetting.Init();
        //StaticNextSceneSetting.SetInkData(filename);
    }

    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
    }

    public void ShowVictoryMenu()
    {
        victoryMenu.SetActive(true);
    }

    public void ShowLoseMenu()
    {
        loseMenu.SetActive(true);
    }

    public void NextScene()
    {

        StaticNextSceneSetting.MoveToNextScene();
    }
}
