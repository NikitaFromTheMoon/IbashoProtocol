using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Battle.Battle
{
    [CreateAssetMenu(fileName = "NewEnemyTeam", menuName = "Ibasho Protocol/Enemy Team")]
    public class EnemyTeamData : ScriptableObject
    {
        public List<string> enemies;
    }
}
