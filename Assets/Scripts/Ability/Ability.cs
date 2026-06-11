using Assets.Scripts.Model;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Ability
    {
        private Func<LivingEntity, LivingEntity, bool> action { get; set; }

        public Ability(Func<LivingEntity, LivingEntity, bool> abilityAction)
        {
            action = abilityAction;
        }
    }
}