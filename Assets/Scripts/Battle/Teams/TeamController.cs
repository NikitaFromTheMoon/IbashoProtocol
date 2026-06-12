using Assets.Scripts.Model;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Battle
{
    public class TeamController : MonoBehaviour
    {
        public LivingEntityTeam team;


        void Awake()
        {
            foreach(var a in GetComponents<EntityCellController>())
            {
                a.gameObject.SetActive(false);
            }

        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}