using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model
{
    public interface IActor
    {
        public void UseAbility(List<LivingEntity> attacked, Ability ability) { foreach (var a in attacked) { UseAbility(a, ability); } }
        public void UseAbility(LivingEntity attacked, Ability ability);
        public void MakeTurn();
    }
}
