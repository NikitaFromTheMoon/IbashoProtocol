using Assets.Scripts.SceneControll;
using Assets.Scripts.UiController.UIButtonController;
using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

namespace Assets.Scripts.Dialogues
{
    public class DialogueManager : MonoBehaviour
    {
        public bool skippable;
        public string characterName;
        public string listedTag;
        public UnityEngine.TextAsset inkText;
        public GameObject choicePanel;
        public Image character;
        public TextMeshProUGUI dialogueText;
        public TextMeshProUGUI nameText;

        private string backgroundName;
        private Story story;
        private ChoiceMenuController cmController;
        private Image background;
        private List<Sprite> backgroundSprites;
        private List<Sprite> charSprites;
        private VariablesState globals;
        private AudioSource audio;
        private List<AudioClip> audioClips;
        private List<AudioClip> music;


        private void Awake()
        {
            inkText = StaticNextSceneSetting.inkFile;
            //Debug.Log(inkText.text);
            story = new Story(inkText.text);
            choicePanel.SetActive(false);
            skippable = true;

            audio = GetComponent<AudioSource>();
            background = GetComponent<Image>();
            cmController = GetComponentInChildren<ChoiceMenuController>(true);
            cmController.dialogue = this;
            backgroundSprites = new List<Sprite>(Resources.LoadAll<Sprite>("Textures/background/StaticBackgrounds"));
            charSprites = new List<Sprite>(Resources.LoadAll<Sprite>("Textures/Dialogue/dialogueCharSprite"));
            audioClips = new List<AudioClip>(Resources.LoadAll<AudioClip>("SFX/Sounds"));
            music = new List<AudioClip>(Resources.LoadAll<AudioClip>("SFX/Music"));

            StartDialogue();
            foreach (AudioClip clip in music) 
                Debug.Log(clip.name);
            foreach (AudioClip clip in audioClips) 
                Debug.Log(clip.name);
        }
        public void StartDialogue()
        {
            ContinueDialogue();
        }

        public void ContinueDialogue()
        {
            Debug.Log("Continue");
            if (story.canContinue)
            {
                dialogueText.text = (story.Continue());
                ShowChoice();

            }
            else if (skippable) 
            {
                ExitDialogue();
            }
            UpdateFromTags();
        }

        public void UpdateFromTags()
        {
            UpdateBackground();
            UpdateCharacter();
            UpdateSFX();
        }

        public void UpdateBackground()
        {
            
            foreach (string tag in story.currentTags)
            {
                if (tag.StartsWith("show:"))
                {
                    backgroundName = tag.Split(':')[1].Trim();
                    var newBg = (backgroundSprites.Find(b => backgroundName == b.texture.name.Trim()));
                    if (newBg is not null)
                    {
                        background.sprite = newBg;
                        Debug.Log($"NEW BG: {backgroundName}");
                        ChangeVisibility(background, true, false);
                        break;
                    }
                }
            }
        }

        public void UpdateSFX()
        {
            foreach (string tag in story.currentTags)
            {
                if (tag.StartsWith("sfx:"))
                {
                    
                    var audioName = tag.Split(':')[1].Trim();
                    var newClip = (audioClips.Find(c => audioName == c.name.Trim()));
                    if (newClip is not null)
                    {
                        audio.PlayOneShot(newClip);
                        Debug.Log($"SOUND ALARM: {backgroundName}");
                        break;
                    }
                }
                if (tag.Trim().StartsWith("play:"))
                {
                    
                    var musicName = tag.Split(':')[1].Trim();
                    var newMusic = (music.Find(c => musicName == c.name.Trim()));
                    if (newMusic is not null)
                    {
                        audio.clip = newMusic;
                        audio.Play();
                        Debug.Log($"NEW Music: {backgroundName}");
                        break;
                    }
                }
            }
        }

        public void UpdateCharacter()
        {
            foreach (string tag in story.currentTags)
            {
                if (tag.StartsWith("show:"))
                {
                    characterName = tag.Split(':')[1].Trim();
                    var newChar = (charSprites.Find(b => characterName == b.texture.name.Trim()));
                    if (newChar is not null)
                    {
                        character.sprite = newChar;
                        ChangeVisibility(character, true, false);
                        Debug.Log($"NEW CHAR: {characterName}");
                    }
                }
                if (tag.StartsWith("hide:")
                    && characterName == tag.Split(':')[1].Trim())
                {
                    //character.sprite = (charSprites.Find(b => backgroundName == b.texture.name.Trim()));
                    Debug.Log($"old CHAR: {characterName}");
                    ChangeVisibility(character, false, false);
                    break;
                }
            }
        }

        public void ChangeVisibility(Image image, bool enable, bool turnBlack)
        {
            var alpha = 0f;
            if (enable)
            {
                alpha = 1f;
            }
            if (turnBlack)
            {
                image.color = new Color(0, 0, 0, alpha);
            } else
            {
                image.color = new Color(1, 1, 1, alpha);
            }
        }

        public void ShowChoice()
        {
            if (story.currentChoices.Count > 0)
            {
                skippable = false;
                choicePanel.SetActive(true);
                cmController.SetActions(story.currentChoices);
            }
        }

        public void Choose(int index)
        {
            story.ChooseChoiceIndex(index);
        }

        public void HideDialogue()
        {
            
        }

        public void ExitDialogue()
        {
            StaticNextSceneSetting.MoveToNextScene();
        }
    }
}