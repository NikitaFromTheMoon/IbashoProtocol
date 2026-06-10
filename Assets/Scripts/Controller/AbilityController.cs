using Assets.Scripts.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts
{
    public class AbilityController
    {
        private Dictionary<string, Ability> abilities { get; set; }
        public AbilityController()
        {
            abilities = new Dictionary<string, Ability>();
        }

        public Ability GetAbility(string name)
        {
            return abilities[name];
        }

        public void InsertAbilities()
        {
            abilities["soul_blast"] = new Ability(SoulBlast);
        }

        public bool SoulBlast(LivingEntity actor, LivingEntity victim)
        {
            if (victim is null) return false;
            victim.TakeDamage((actor.chars.baseDamage + 15));
            return true;
        }
    }
}