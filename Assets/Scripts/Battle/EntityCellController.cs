using Assets.Scripts.Model;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Battle
{
    public class EntityCellController : MonoBehaviour
    {
        public LivingEntity entity;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AddEntity(GameObject newEntity) 
        {
            entity = newEntity.GetComponent<LivingEntity>();
        }
    }
}