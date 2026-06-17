using Assets.Scripts.Effect;
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
        private static Dictionary<string, Ability> abilities;
        //If I'l ever remake it into a Singleton
        //private static static AbilityController thisController;
        //private static AbilityController() { abilities = new Dictionary<string, Ability>(); thisController = this; }


        public static IEnumerator ConjureAbility(LivingEntity caster, LivingEntity victim, AbilityData ability)
        {
            Debug.Log($"Caster {caster} is using an ability {ability} on a victim {victim}");
            switch (ability.abilityName)
            {
                case "soul_blast":
                    SoulBlast(caster, victim);
                    victim.Animator.SetTrigger("damaged");
                    break;
                case "base_attack":
                    BaseAttack(caster, victim);
                    break;
            }
            yield return new WaitForSeconds(1.52f);
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