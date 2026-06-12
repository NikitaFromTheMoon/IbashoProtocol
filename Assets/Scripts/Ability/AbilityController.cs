using Assets.Scripts.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts
{
    public static class AbilityController
    {
        private static Dictionary<string, Ability> abilities = new Dictionary<string, Ability>();


        public static Ability GetAbility(string name)
        {
            return abilities[name];
        }

        public static void InsertAbilities()
        {
            abilities["soul_blast"] = new Ability(SoulBlast);
            abilities["fire_blast"] = new Ability(FireBall);
        }

        public static bool SoulBlast(LivingEntity actor, LivingEntity victim)
        {
            if (victim is null) return false;
            victim.TakeDamage((actor.chars.baseDamage + 15));
            return true;
        }

        public static bool FireBall(LivingEntity actor, LivingEntity victim)
        {
            if (victim is null) return false;
            victim.TakeDamage((actor.chars.baseDamage + 20));
            return true;
        }
    }
}