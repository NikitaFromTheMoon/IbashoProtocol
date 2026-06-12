using Assets.Scripts.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public class LivingEntity : MonoBehaviour, IDamagable, IActor
    {
        public EntityChar chars;
        private bool isDead => currentHP <= 0;
        private List<Effect> underEffects;
        private LivingEntityTeam team;
        private LivingEntityTeam enemyTeam;
        private Animator animator;
        private int inTeamPos;
        int currentHP;
        int initiative;
        HPBarController hpbc;



        public Animator Animator => animator;
        public LivingEntityTeam Team => team;
        public LivingEntityTeam Enemies => enemyTeam;
        public int HP => (currentHP > 0 ? currentHP : 0);

        void Awake()
        {
            currentHP = chars.baseHp;
            underEffects = new List<Effect>();
            animator = GetComponent<Animator>();
            hpbc = GetComponentInChildren<HPBarController>();
        }
        public void TakeDamage(int value)
        {
            if (isDead) return;
            currentHP -=value;
            hpbc.AdjustHPBar();
        }

        public void UseAbility(LivingEntity attacked, Ability ability) 
        {
            ability.GetAction.Invoke(this, attacked);
        }

        public void MakeTurn()
        {

        }

    }
}
