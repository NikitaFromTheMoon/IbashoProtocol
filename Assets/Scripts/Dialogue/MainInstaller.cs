using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Dialogues
{
    public class MainInstaller : Zenject.MonoInstaller
    {
        SpriteRenderer spriteRenderer;
        Image image;
        // Use this for initialization
        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}