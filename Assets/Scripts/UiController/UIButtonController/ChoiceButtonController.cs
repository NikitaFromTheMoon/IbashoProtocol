using Assets.Scripts.Model;
using System.Collections;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UiController.UIButtonController
{
    public class ChoiceButtonController : MonoBehaviour
    {
        private Image image;
        public string tagValue;
        public Button button;
        public TextMeshProUGUI TMPro;
        
        private void Awake()
        {
            image = GetComponentInChildren<Image>(true);
            button = GetComponentInChildren<Button>(true);
            TMPro = GetComponentInChildren<TextMeshProUGUI>(true);
        }
    }
}