using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Battle
{
    public class TeamController : MonoBehaviour
    {

        // Use this for initialization
        void Awake()
        {
            AbilityController.InsertAbilities();

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}