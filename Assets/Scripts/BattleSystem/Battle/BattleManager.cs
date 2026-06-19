using Assets.Scripts;
using Assets.Scripts.Battle;
using Assets.Scripts.Battle.Battle;
using Assets.Scripts.Model;
using Assets.Scripts.SceneControll;
using Assets.Scripts.UiController.UIButtonController;
using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    string sceneName;
    public static BattleManager Instance;
    public BattleSettingsData settings;
    public TeamController bads;
    public TeamController goods;
    public LivingEntity selected;
    private BGController bgc;
    private LivingEntity player;
    private AbilityMenuController abilityMenu;
    private List<LivingEntity> allEntities => new List<LivingEntity>(goods.team.members.Concat(bads.team.members));
    private int round;
    private List<EnemyTeamData> enemyTeamsLeft;
    private Coroutine corutine;

    private void Start()
    {
        settings = BattleSettingsStatic.settings;

        enemyTeamsLeft = new List<EnemyTeamData>(settings.EnemyTeams);
        Debug.Log($"Info about { enemyTeamsLeft.Count } teams recieved");
        enemyTeamsLeft.Reverse();
        abilityMenu = GetComponentInChildren<AbilityMenuController>();

        bgc = GetComponentInChildren<BGController>();
        bgc.SetBGImage(settings.Background);

        goods.AddEntity(settings.PlayerName, out player);
        abilityMenu.player = player;
        player.SetAbilities(BattleSettingsStatic.unlockedAbilities.Keys.ToList());
        player.SetPlayerControlled();
        abilityMenu.SetAbilities(player.baseChars.abilities);
        Debug.Log($"Abilities set to {string.Join(' ', player.baseChars.abilities)}");

        corutine = StartCoroutine(StartBattle());
    }

    public LivingEntity GetPlayer()
    {
        return player;
    }
    private IEnumerator StartBattle()
    {
        while (enemyTeamsLeft.Count > 0 && !player.IsDead)
        {
            yield return new WaitForSeconds(2 + (Random.value * 3));
            Debug.Log("Battle started!");
            bgc.enabled = false;
            player.Animator.SetBool("inBattle", true);

            bads.AddEntities(new List<string>(enemyTeamsLeft.Last().enemies));
            enemyTeamsLeft.Remove(enemyTeamsLeft.Last());

            while (bads.team.members.Count > 0 && !player.IsDead)
            {
                goods.getEntities().ForEach(e => e.setEnemies(bads.team));
                bads.getEntities().ForEach(e => e.setEnemies(goods.team));
                var AllEntities = allEntities;
                AllEntities.ForEach(e => e.RechargeMana());

                foreach (var entity in AllEntities)
                {
                    if (entity.IsDead)
                        continue;
                    if (!entity.IsAIControlled)
                        abilityMenu.playersTurn = true;
                    yield return StartCoroutine(entity.MakeTurn());
                    abilityMenu.playersTurn = false;

                    for (var i = bads.team.members.Count - 1; i > 0; i--)
                    {
                        if (bads.team.members[i].IsDead)
                        {
                            Debug.Log($"DELETING enemy {i}");
                            bads.RemoveEntity(bads.team.members[i]);
                        }
                    }
                }
                if (bads.team.members.Count == 0) 
                    break;
            }
            Debug.Log("Continue walking");
            bgc.enabled = true;
            player.Animator.SetBool("inBattle", false);
        }
        if (player.IsDead)
        {
            ActivateLoseMenu();
            Debug.Log("Player is dead!");
        }
        else if (enemyTeamsLeft.Count == 0)
        {
            ActivateVictoryMenu();
            Debug.Log("No enemies left!");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ActivatePauseMenu();
    }

    private void ActivatePauseMenu()
    {
        var menuC = GetComponent<MenuController>();
        menuC.ShowPauseMenu();
    }

    private void ActivateVictoryMenu()
    {
        var menuC = GetComponent<MenuController>();
        menuC.ShowVictoryMenu();
    }

    private void ActivateLoseMenu()
    {
        if (settings.name == "Prologue")
        {
            StaticNextSceneSetting.MoveToNextScene();
            return;
        }
        var menuC = GetComponent<MenuController>();
        menuC.ShowLoseMenu();
    }
}
