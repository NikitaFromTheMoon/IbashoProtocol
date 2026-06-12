using Assets.Scripts.Model;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UiController.UIButtonController
{
    public class AbilityButtonController : MonoBehaviour
    {
        private BattleControllerEP bc;
        [InspectorName(displayName:"Player")] public LivingEntity entity;
        public string abilityName;
        public Ability ability;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UseAbility(LivingEntity enemy)
        {
            if (ability is null) 
                ability = AbilityController.GetAbility(abilityName);
            entity.UseAbility(enemy, ability);
            Debug.Log(@"Ability {abilityName} was clicked!");
        }
    }
}