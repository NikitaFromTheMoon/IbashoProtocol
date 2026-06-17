using Assets.Scripts.Model;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UiController.UIButtonController
{
    public class AbilityButtonController : MonoBehaviour
    {
        private AbilityMenuController menu;
        private BattleControllerEP bc;
        private SpriteRenderer image;
        [InspectorName(displayName:"Player")] public LivingEntity entity;
        public string abilityName;
        
        public bool isEmpty => entity is null;

        private void Awake()
        {
            menu = GetComponentInParent<AbilityMenuController>();
            bc = GetComponentInParent<BattleControllerEP>();
        }

        public void UseAbility()
        {
            if (!menu.playersTurn)
            {
                Debug.Log(@"False start!");
            }
            if (bc.bads.team.members.Count > 0)
                entity.UseAbility(bc.bads.team.members.Last(), abilityName);
            else
                LivingEntity.Flag = false;
            Debug.Log(@"Ability {abilityName} was clicked!");
        }

        public void SetAbility(string newAbility)
        {
            abilityName = newAbility;
        }
    }
}