using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public abstract class LivingEntity : IDamagable, IActor
    {
        public EntityChar chars { get; set; }
        private bool isDead => currentHP > 0;
        private List<Effect> underEffects;
        private LivingEntityTeam team;
        private LivingEntityTeam enemyTeam;
        private Animator animator;
        private int inTeamPos;
        int currentHP;

        public LivingEntity(EntityChar ch) {
            currentHP = ch.baseHp;
            chars = ch;
            underEffects = new List<Effect>();
        }

        public Animator Animator => animator;
        public LivingEntityTeam Team => team;
        public LivingEntityTeam Enemies => enemyTeam;
        public int TeamIndex => inTeamPos;

        public void TakeDamage(int value)
        {
            if (isDead) return;
            currentHP -=value;
        }

        public void UseAbility(IDamagable attacked) 
        {

        }

        public void MakeTurn()
        {

        }
    }
}
