using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model
{
    public abstract class LivingEntity : IDamagable, IActor
    {
        public EntityChar chars { get; set; }
        private bool isDead = false;
        private List<Effect> underEffects;
        private LivingEntityTeam team;
        int currentHP;

        public LivingEntity(EntityChar ch) {
            currentHP = ch.baseHp;
            chars = ch;
            underEffects = new List<Effect>();
        }

        public LivingEntityTeam Team { get { return team; } set { team = value; } }

        public void TakeDamage(int value)
        {
            if (isDead) return;
            currentHP -=value;
            if (currentHP < 0) isDead = true;
        }


        void IActor.UseAbility(IDamagable attacked) { }
    }
}
