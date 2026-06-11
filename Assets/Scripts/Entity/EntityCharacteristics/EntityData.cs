using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Model.EnemyData
{
    [CreateAssetMenu(fileName = "NewEnemyData", menuName = "Ibasho Protocol/Enemy Data")]
    public class EntityData : ScriptableObject
    {
        //public Sprite sprite;
        public string name;
        public EntityChar characteristics;
    }
}