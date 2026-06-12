using Assets.Scripts.Model;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Battle
{
    public class HPBarController : MonoBehaviour
    {
        private float maxLength;
        private float currentLength;
        private SpriteRenderer sprite;
        private RectTransform Transform;

        public LivingEntity entity;


        void Awake()
        {
            Transform = GetComponent<RectTransform>();
            var parent = GetComponentInParent<LivingEntity>();
            sprite = GetComponent<SpriteRenderer>();
            maxLength = sprite.size.x;
            currentLength = maxLength;
            Debug.Log(Transform);
            Debug.Log($"player HP: {parent.HP}");
            Debug.Log(currentLength);
            Debug.Log(maxLength);

        }

        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AdjustHPBar()
        {
            Debug.Log($"Adjusting HP for {entity.name}");
            if (!entity.IsDead)
                currentLength = (((entity.HP + 0f) / entity.chars.baseHp) * maxLength);
            else 
                currentLength = 0;
            Debug.Log($"New length is {currentLength}");
            sprite.size = new Vector2 (currentLength, sprite.size.y);
            //Transform.sizeDelta = new Vector2(currentLength, Transform.sizeDelta.y);

        }
    }
}