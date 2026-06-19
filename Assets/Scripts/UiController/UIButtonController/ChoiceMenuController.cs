using Assets.Scripts.Dialogues;
using Ink.Runtime;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.UiController.UIButtonController
{
    public class ChoiceMenuController : MonoBehaviour
    {
        public List<ChoiceButtonController> buttons;
        public DialogueManager dialogue;
        public bool playersTurn;

        void Awake()
        {
            buttons = GetComponentsInChildren<ChoiceButtonController>(true).ToList();
        }

        void Update()
        {

        }

        public void Initialize(List<string> tags)
        {
            var q = new Queue<string>(tags);
            foreach (var button in buttons)
            {
                if (q.Count <= 0)
                {

                    return;
                }
                button.tagValue = q.Dequeue();
                button.gameObject.SetActive(true);
                button.button.onClick.AddListener(() => buttons.ForEach(b => b.gameObject.SetActive(false)));
            }
        }

        public void SetActions(List<Choice> choices)
        {
            int i = 0;
            foreach (var button in buttons)
            {
                if (i < choices.Count)
                {
                    var choice = choices[i];
                    button.gameObject.SetActive(true);
                    button.TMPro.text = choice.text;
                    int n = i;
                    button.button.onClick.AddListener(() => 
                    {
                        //dialogue.listedTag = tag;
                        dialogue.Choose(n);
                        dialogue.UpdateFromTags();
                        dialogue.skippable = true;
                        dialogue.ContinueDialogue();
                        buttons.ForEach((b) => b.button.onClick.RemoveAllListeners());
                        dialogue.choicePanel.SetActive(false);
                    });
                }
                else
                {
                    button.gameObject.SetActive(false);
                }
                i++;
            }
        }
    }
}