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
        public bool playersTurn;

        void Awake()
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
            Debug.Log($"NO more buttons :{ buttons.Count }");
            foreach (var button in buttons)
            {
                if (abs.Count != 0)
                {
                    var data = Resources.Load<AbilityData>("ScriptableObjects/Abilities/" + abs?.Last());
                    Debug.Log($"Resorce {data.abilityName} loaded, common image: {data.sprite.name}, mana cost: {data.manaCost}");
                    button.abilityName = data.abilityName;
                    button.entity = player;
                    var im = button.GetComponentsInChildren<Image>().Where((i) => (i.gameObject != gameObject)).Last();
                    im.sprite = data.sprite;
                    im.color = Color.white;

                    var text = button.GetComponentInChildren<TextMeshProUGUI>(true);
                    text.text = data.manaCost + "";
                    text.gameObject.SetActive(true);
                    abs.Remove(abs.Last());
                }
                else
                {
                    break;
                }
            }
        }
    }
}