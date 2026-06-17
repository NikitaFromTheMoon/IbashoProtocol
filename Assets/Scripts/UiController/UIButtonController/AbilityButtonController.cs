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
        
        public bool isEmpty => entity is null;

        public void UseAbility()
        {
            entity.UseAbility(bc.selected, abilityName);
            Debug.Log(@"Ability {abilityName} was clicked!");
        }

        public void SetAbility(string newAbility)
        {
            //var n = Resources.Load("ScriptableObject/"+newAbility);
            abilityName = newAbility;
        }
    }
}