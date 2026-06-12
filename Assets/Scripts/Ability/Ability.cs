using Assets.Scripts.Model;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Ability
    {
        private Func<LivingEntity, LivingEntity, bool> action;
        public Func<LivingEntity, LivingEntity, bool> GetAction => action;

        public Ability(Func<LivingEntity, LivingEntity, bool> abilityAction)
        {
            action = abilityAction;
        }
    }
}