using Assets.Scripts.Model;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UiController.UIButtonController
{
    public class AbilityMenuController : MonoBehaviour
    {
        public List<AbilityButtonController> buttons;
        public List<string> abilities;
        public LivingEntity player;
        
        void Start()
        {
            buttons = GetComponentsInChildren<AbilityButtonController>().ToList();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetAbilities(List<string> abs)
        {
            abilities = abs.ToArray().ToList();
            foreach (var button in buttons)
            {
                if (abilities.Count != 0)
                {
                    var data = Resources.Load<AbilityData>("ScriptableObject/Abilities/"+abs.Last());

                    button.abilityName = data.abilityName;
                    button.entity = player;
                    button.GetComponent<Image>().sprite = data.sprite;
                    var text = button.GetComponent<TextMeshPro>();
                    text.text = (data.manaCost+"");
                    text.gameObject.SetActive(true);
                    abs.Remove(data.abilityName);
                }
                else
                {
                    break;
                }
            }
        }
    }
}