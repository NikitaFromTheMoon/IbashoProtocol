using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Assets.Scripts.Dialogue
{
    public class Dialogue : MonoBehaviour
    {
        Story story;
        public UnityEngine.TextAsset inkText;
        public List<Character> characters;

        private void Start()
        {
            story = new Story(inkText.text);


            StartDialogue();
        }
        public void StartDialogue()
        {
            
            while (story.canContinue)
            {
                var c = story.Continue();
                
            }

            ExitDialogue();
        }

        public void ContinueDialogue()
        {
            
        }

        public void ShowChoice()
        {
            
        }

        public void ShowDialogue()
        {
            
        }

        public void HideDialogue()
        {
            
        }

        public void ExitDialogue()
        {
            
        }
    }
}