using Assets.Scripts.Effect;
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
        private static Dictionary<string, Ability> abilities;


        public static IEnumerable ConjureAbility(LivingEntity caster, LivingEntity victim, AbilityData ability)
        {
            switch (ability.abilityName) {
                case "soul_blast":
                    SoulBlast(caster, victim);
                    yield return new WaitForSeconds(1.5f);
                    break;
                case "base_attack":
                    BaseAttack(caster, victim);
                    yield return new WaitForSeconds(1.5f);
                    break;
            }
        }

        public static bool SoulBlast(LivingEntity actor, LivingEntity victim)
        {
            if (victim is null) return false;
            victim.TakeDamage((actor.Characteristics.baseDamage + 15));
            return true;
        }

        public static bool MagicStrike(LivingEntity actor, LivingEntity victim)
        {
            if (victim is null) return false;
            victim.TakeDamage((actor.Characteristics.baseDamage + 10));
            return true;
        }

        public static bool BaseAttack(LivingEntity actor, LivingEntity victim)
        {
            if (victim is null) return false;
            victim.TakeDamage(actor.baseChars.baseDamage);
            return true;
        }

        public static bool CultistBuff(LivingEntity actor, LivingEntity victim)
        {
            var chars = new Dictionary<string, object>();
            chars["duration"] = 1;
            chars["armorPenetration"] = 0;
            chars["hpChange"] = 0;
            chars["type"] = "positive";
            actor.team.members.ForEach(m => m.TakeEffect(new StatChangeEffect(chars)));
            return true;
        }
    }
}