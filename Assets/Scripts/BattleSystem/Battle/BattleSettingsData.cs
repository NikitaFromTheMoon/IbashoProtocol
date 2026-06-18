using Assets.Scripts.Battle.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.SceneControll
{
    [CreateAssetMenu(fileName = "NewBattle", menuName = "Ibasho Protocol/Battle")]
    public class BattleSettingsData : ScriptableObject
    {
        public List<EnemyTeamData> EnemyTeams;
        public Sprite Background;
        public string PlayerName;


    }
}
