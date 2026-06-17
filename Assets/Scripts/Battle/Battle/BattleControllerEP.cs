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

public class BattleControllerEP : MonoBehaviour
{
    string sceneName;
    public static BattleControllerEP Instance;
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


        //Debug.Log($"Trying to add player {}");
        goods.AddEntity(settings.PlayerName, out player);
        abilityMenu.player = player;
        player.SetAbilities(BattleSettingsStatic.unlockedAbilities.Keys.ToList());
        player.SetPlayerControlled();
        abilityMenu.SetAbilities(player.baseChars.abilities);
        Debug.Log($"Abilities set to {string.Join(' ', player.baseChars.abilities)}");


        //abilityMenu.SetAbilities(BattleSettingsStatic.unlockedAbilities.Keys.ToList());



        //
        //
        //
        //

        //allEntities.Sort((e1, e2) => e1.initiative - e2.initiative);

        corutine = StartCoroutine(StartBattle());
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
            Debug.Log($"Added enemies: {string.Join(' ', bads.team.members.Select(member => member.baseChars.name))}");
            Debug.LogWarning($"All commands: {enemyTeamsLeft.Count}");
            enemyTeamsLeft.Remove(enemyTeamsLeft.Last());
            Debug.LogWarning($"All commands: {enemyTeamsLeft.Count}");

            while (bads.team.members.Count > 0 && !player.IsDead)
            {
                goods.getEntities().ForEach(e => e.setEnemies(bads.team));
                bads.getEntities().ForEach(e => e.setEnemies(goods.team));
                Debug.LogWarning($"Badass team: { string.Join(' ', bads.team.members.Select(m => m.name)) }");
                Debug.LogWarning($"Friendy cuky team: { string.Join(' ', goods.team.members.Select(m => m.name)) }");
                var AllEntities = allEntities;
                AllEntities.ForEach(e => e.RechargeMana());

                //Everyone is Moving
                foreach (var entity in AllEntities)
                {
                    if (entity.IsDead)
                        continue;
                    if (!entity.IsAIControlled)
                        abilityMenu.playersTurn = true;
                    yield return StartCoroutine(entity.MakeTurn());
                    abilityMenu.playersTurn = false;

                    //Removing dead
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
        menuC.ShowPauseMenu();
    }

    private void SetCoroutineActivity()
    {
        
    }

    private void ActivateVictoryMenu()
    {
        var menuC = GetComponent<MenuController>();
        menuC.ShowPauseMenu();
    }

    private void ActivateLoseMenu()
    {
        var menuC = GetComponent<MenuController>();
        menuC.ShowPauseMenu();
    }
}
