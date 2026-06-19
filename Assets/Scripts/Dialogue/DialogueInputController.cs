using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Dialogues
{
    internal class DialogueInputController : MonoBehaviour, NovelleActions.IDialogueActions
    {
        NovelleActions actions;
        DialogueManager dialogue;
        private void OnEnable()
        {
            if (actions != null) 
                return;
            actions = new NovelleActions();
            dialogue = FindAnyObjectByType<DialogueManager>();
            actions.Dialogue.SetCallbacks(this);
            actions.Dialogue.Enable();
            Debug.Log("actions enable");
        }

        private void OnDisable()
        {
            actions.Dialogue.Disable();
        }

        public void OnNextPhrase(InputAction.CallbackContext context)
        {
            Debug.Log("click");
            if (context.started && dialogue.skippable)
            {
                dialogue.ContinueDialogue();
            }

        }

        public void OnSkipPhrases(InputAction.CallbackContext context)
        {

        }
    }
}
