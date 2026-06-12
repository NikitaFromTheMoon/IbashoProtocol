using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.SceneControll
{

    public class BattleSettings : ScriptableObject
    {
        public List<GameObject> EnemyTeams;
        public Sprite Background;
        public GameObject Player;

    }
}
