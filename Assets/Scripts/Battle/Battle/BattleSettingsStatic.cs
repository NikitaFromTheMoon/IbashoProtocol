using Assets.Scripts.SceneControll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Battle
{
    public static class BattleSettingsStatic
    {
        public static BattleSettingsData settings;
        public static Dictionary<string, bool> unlockedAbilities = new Dictionary<string, bool>();

        public static void SetData(BattleSettingsData data)
        {
            Debug.Log($"Battle dats is set on {data}");
            settings = data;
            unlockedAbilities["soul_blast"] = true;
        }
    }
}
