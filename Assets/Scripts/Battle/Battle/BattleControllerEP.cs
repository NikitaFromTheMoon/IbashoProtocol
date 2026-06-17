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
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleControllerEP : MonoBehaviour
{
    string sceneName;
    public static BattleControllerEP Instance;
    public BattleSettingsData settings;
    public TeamController goods;
    public TeamController bads;
    public LivingEntity selected;
    private BGController bgc;
    private LivingEntity player;
    private AbilityMenuController abilityMenu;
    private List<LivingEntity> allEntities => new List<LivingEntity>(goods.team.members.Concat(bads.team.members));
    private int round;
    private List<EnemyTeamData> enemyTeamsLeft;

    private void Start()
    {
        settings = BattleSettingsStatic.settings;

        enemyTeamsLeft = new List<EnemyTeamData>(settings.EnemyTeams);
        Debug.Log($"Info about { enemyTeamsLeft.Count } teams recieved");
        enemyTeamsLeft.Reverse();
        abilityMenu = GetComponentInChildren<AbilityMenuController>();

        bgc = GetComponentInChildren<BGController>();
        bgc.SetBGImage(settings.Background);


        //Debug.Log($"Trying to add player {}");
        goods.AddEntity(settings.PlayerName, out player);
        abilityMenu.player = player;
        player.SetAbilities(BattleSettingsStatic.unlockedAbilities.Keys.ToList());
        player.SetPlayerControlled();
        abilityMenu.SetAbilities(player.baseChars.abilities);


        //abilityMenu.SetAbilities(BattleSettingsStatic.unlockedAbilities.Keys.ToList());



        //
        //
        //
        //

        //allEntities.Sort((e1, e2) => e1.initiative - e2.initiative);

        StartCoroutine(StartBattle());
    }

    private IEnumerator StartBattle()
    {
        while (enemyTeamsLeft.Count > 0 && !player.IsDead)
        {
            yield return new WaitForSeconds(1/*+(Random.value * 3)*/);
            Debug.Log("Battle started!");
            bgc.enabled = false;
            player.Animator.SetBool("inBattle", true);

            bads.AddEntities(new List<string>(enemyTeamsLeft.Last().enemies));
            Debug.Log($"Added enemies: {string.Join(' ', bads.team.members.Select(member => member.baseChars.name))}");
            enemyTeamsLeft.Remove(enemyTeamsLeft.Last());

            while (bads.team.members.Count > 0 && !player.IsDead)
            {
                goods.getEntities().ForEach(e => e.setEnemies(bads.team));
                bads.getEntities().ForEach(e => e.setEnemies(goods.team));
                var AllEntities = allEntities;
                //Everyone is Moving
                foreach (var entity in AllEntities)
                {
                    if (entity.IsDead)
                        continue;

                    yield return StartCoroutine(entity.MakeTurn());

                    //Removing dead
                    foreach (var enemy in bads.team.members)
                    {
                        if (enemy.IsDead)
                        {
                            bads.RemoveEntity(enemy);
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
        if (enemyTeamsLeft.Count == 0)
        {
            ActivateVictoryMenu();
            Debug.Log("No enemies left!");
        }
        if (player.IsDead)
        {
            ActivateLoseMenu();
            Debug.Log("Player is dead!");
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
        menuC.ShowExternalMenu();
    }

    private void ActivateVictoryMenu()
    {
        var menuC = GetComponent<MenuController>();
        menuC.ShowExternalMenu();
    }

    private void ActivateLoseMenu()
    {
        var menuC = GetComponent<MenuController>();
        menuC.ShowExternalMenu();
    }
}
