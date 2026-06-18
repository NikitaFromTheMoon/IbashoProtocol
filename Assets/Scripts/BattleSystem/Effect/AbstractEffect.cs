using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public abstract class AbstractEffect
    {
        public int turnsLeft;
        public Texture2D icon;

        public AbstractEffect(Dictionary<string, object> chars)
        {
        }

        public virtual EntityChar OnNewTurn(EntityChar entityChar) { return entityChar; }
        public virtual EntityChar OnDurationStart(EntityChar entityChar) { return entityChar; }
        public virtual EntityChar OnDurationEnd(EntityChar entityChar) { return entityChar; }
    }
}