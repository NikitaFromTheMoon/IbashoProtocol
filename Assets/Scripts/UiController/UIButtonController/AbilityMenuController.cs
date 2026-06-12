using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.UiController.UIButtonController
{
    public class AbilityMenuController : MonoBehaviour
    {
        public List<AbilityButtonController> buttons;
        public List<Ability> abilities;
        
        void Start()
        {
            SetAbilities();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetAbilities()
        {
            foreach (var button in buttons)
            {
                if (abilities.Count != 0)
                {
                    button.ability = abilities.Last();
                    abilities.RemoveAt(abilities.Count - 1);
                }
                else
                {
                    break;
                }
            }
        }
    }
}