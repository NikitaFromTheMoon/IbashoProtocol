using Assets.Scripts.Model;
using System.Collections.Generic;

namespace Assets.Scripts.Effect
{
    public class StatChangeEffect : AbstractEffect
    {
        int armorPenetration;
        int hpChange;
        string type;

        public StatChangeEffect(Dictionary<string, object> chars) : base(chars) 
        {
            turnsLeft = (int)chars["duration"];
            armorPenetration = (int)chars["armorPenetration"];
            hpChange = (int)chars["hpChange"];
            type = (string)chars["type"];
        }

        override public EntityChar OnNewTurn(EntityChar entityChar)
        {
            if (hpChange > 0)
                entityChar.baseHp += hpChange;
            else
                entityChar.baseHp += (entityChar.baseArmor + hpChange >= 0) 
                    ? 0 
                    : entityChar.baseArmor + hpChange;
            turnsLeft -= 1;
            return entityChar;
        }
    }
}
