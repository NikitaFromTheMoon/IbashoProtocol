using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Bg_mover : MonoBehaviour
{
    public RectTransform anchor;
    public RectTransform[] bg;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isActiveAndEnabled) {
            bg[0].position = new Vector3(bg[0].position.x - (int)(bg[0].rect.width * 0.001), bg[0].position.y, bg[0].position.z);
            bg[1].position = new Vector3(bg[1].position.x - (int)(bg[1].rect.width * 0.001), bg[1].position.y, bg[1].position.z);
            if (bg[0].position.x < anchor.rect.xMin - bg[0].rect.width)
            {
                bg[0].position = new Vector3(bg[0].position.x + bg[0].rect.width, bg[0].position.y, bg[0].position.z);
                var temp = bg[0];
                bg[0] = bg[1];
                bg[1] = temp;
            }
        }
    }


}
