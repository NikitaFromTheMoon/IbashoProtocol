using Assets.Scripts.Model;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Battle
{
    public class EntityCellController : MonoBehaviour
    {
        public LivingEntity entity;


        void Awake()
        {
            SetVisibility(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public GameObject AddEntity(string entityName)
        {
            Object prefab = Resources.Load("Prefab/" + entityName);
            SetVisibility(true);
            Debug.Log($"Trying to add entity { prefab }");
            var obj = (GameObject)Instantiate(prefab, transform);
            entity = obj.GetComponent<LivingEntity>();
            return obj;
        }

        public LivingEntity RemoveEntity()
        {
            var e = entity;
            Destroy(entity);
            entity = null;
            SetVisibility(false);
            return e;
        }

        public LivingEntity RemoveEntity(LivingEntity toLeave)
        {
            if (entity is null)
                return null;
            if (toLeave == entity)
            {
                entity = null;
                Destroy(toLeave);
            }
            return entity;
        }

        public bool isOccupied()
        {
            Debug.Log($"Trying to see if cell { this } is Occupied");
            return (entity is not null && !entity.IsUnityNull());
        }

        public void SetVisibility(bool flag)
        {
            var sp = this.GetComponent<SpriteRenderer>();
            if (flag)
                sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 1f);
            else
                sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 0);

        }
    }
}