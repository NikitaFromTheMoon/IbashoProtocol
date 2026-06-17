using Assets.Scripts.Model;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace Assets.Scripts.Battle
{
    public class TeamController : MonoBehaviour
    {
        public LivingEntityTeam team;
        public List<EntityCellController> cells;


        void Awake()
        {
            team = new LivingEntityTeam();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public bool AddEntities(List<string> entities)
        {
            foreach (var entity in entities)
            {
                if (!AddEntity(entity))
                {
                    Debug.LogWarning("UNEXPECTED, entitiy wasn't added");
                    return false;
                }
            }
            return true;
        }

        public bool AddEntity(string entityName)
        {
            var nullEntity = new LivingEntity();
            return AddEntity(entityName, out nullEntity);
        }

        public bool AddEntity(string entityName, out LivingEntity entOut)
        {
            foreach (var cell in cells)
            {
                if (!cell.isOccupied())
                {
                    Debug.Log($"Entity added: {entityName}, cell: {cell.name}");
                    entOut = cell.AddEntity(entityName).GetComponent<LivingEntity>();
                    entOut.team = this.team;
                    team.members.Add(entOut);
                    return true;
                }
            }
            entOut = null;
            return false;
        }

        public bool RemoveEntity(LivingEntity toLeave)
        {
            foreach(var cell in cells)
            {
                cell.RemoveEntity(toLeave);
            }
            toLeave.team = null;
            team.members.Remove(toLeave);
            return true;
        }

        public List<LivingEntity> getEntities()
        {
            return team.members;
        }

    }
}