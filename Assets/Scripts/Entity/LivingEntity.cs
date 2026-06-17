using Assets.Scripts.Battle;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public class LivingEntity : MonoBehaviour, IDamagable
    {
        private List<AbstractEffect> underEffects;
        private LivingEntityTeam enemyTeam;
        private Animator animator;
        private int timesToMove;
        private Logger logger;
        int currentHP;
        int currentMana;
        HPBarController hpbc;

        public EntityChar baseChars;
        public EntityChar Characteristics => ApplyEffects();
        public bool IsEnemy;
        public bool IsAIControlled;
        public int initiative = 0;
        public bool IsDead => (currentHP <= 0);
        public Animator Animator => animator;
        public LivingEntityTeam team;
        public LivingEntityTeam Enemies => enemyTeam;
        public int HP => (currentHP > 0 ? currentHP : 0);
        public static bool Flag = false;

        void Awake()
        {
            currentHP = baseChars.baseHp;
            Debug.Log($"Character {baseChars.name}, hp of living entity is set to {baseChars.baseHp}");
            currentMana = 100;
            IsAIControlled = true;
            underEffects = new List<AbstractEffect>();
            animator = GetComponent<Animator>();
            hpbc = GetComponentInChildren<HPBarController>();
            Debug.Log($"hpbc is {hpbc}", this);
        }

        void Update()
        {

        }

        public void TakeDamage(int value)
        {
            if (IsDead) this.gameObject.SetActive(false);
            currentHP -= value;
            hpbc.AdjustHPBar();
        }

        public void TakeEffect(AbstractEffect effect)
        {
            underEffects.Add(effect);
        }

        private EntityChar ApplyEffects()
        {
            if (underEffects.Count == 0) return baseChars;
            else if (underEffects.Count == 1) return underEffects[0].OnNewTurn(baseChars);
            foreach (var e in underEffects)
            {
                if (e.turnsLeft <= 0)
                    underEffects.Remove(e);
            }
            var newChars = baseChars;
            newChars.baseHp = currentHP;
            underEffects.ForEach(e => { newChars = e.OnNewTurn(newChars); });
            currentHP = newChars.baseHp;
            return null;
        }

        public bool UseAbility(LivingEntity attacked, string ability)
        {
            var data = Resources.Load<AbilityData>("ScriptableObject/" + ability);
            //attacked.TakeDamage(Characteristics.baseDamage);
            //animator.SetTrigger("attacking");
            //if (this.currentMana < data.manaCost)
                //return false;
            currentMana -= data.manaCost;
            AbilityController.ConjureAbility(this, attacked, data);
            return true;
        }

        public void setEnemies(LivingEntityTeam team)
        {
            enemyTeam = team;
        }

        public void SetAbilities(List<string> abs)
        {
            baseChars.abilities = abs;
        }

        public void SetPlayerControlled()
        {
            IsAIControlled = false;
        }

        public IEnumerator MakeTurn()
        {
            if (IsAIControlled)
            {
                timesToMove = 1;

                for (var i = timesToMove; i > 0; i--)
                {
                    int skip = (int)(UnityEngine.Random.value * 1000);
                    var target = enemyTeam.members[skip % enemyTeam.members.Count];
                    if (baseChars.abilities == null || baseChars.abilities.Count == 0)
                        break;
                    var attack = baseChars.abilities.First() /*[skip % chars.abilities.Count]*/;

                    UseAbility(target, attack);
                 yield return new WaitForSeconds(2);

                }
            }
            else
            {
                Flag = true;
                while (Flag)
                {
                    yield return new WaitForSeconds(0.05f);
                }
            }
        }
    }
}
