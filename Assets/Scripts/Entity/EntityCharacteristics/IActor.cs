using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model
{
    public interface IActor
    {
        public void UseAbility(List<IDamagable> attacked) { foreach (var a in attacked) { UseAbility(a); } }
        public void UseAbility(IDamagable attacked);
        public void MakeTurn();
    }
}
