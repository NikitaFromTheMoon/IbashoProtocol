using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BGController : MonoBehaviour
{
    public Transform anchor;
    public RectTransform[] bg;
    public float speed;
    private float length;


    void Start()
    {
        bg = GetComponentsInChildren<RectTransform>();
        length = bg[0].rect.width * bg[0].localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var el in bg)
        {
            MoveBGElement(el, speed * Time.deltaTime);
            var corners = new Vector3[4];
            el.GetWorldCorners(corners);
            if (anchor.position.x > corners[2].x)
            {
                Debug.Log($"Trying to move {el} from {el.anchoredPosition} on {length}");
                el.anchoredPosition += new Vector2(length*2 - 4, 0);
            }
        }
    }

    public void SetBGImage(Sprite texture)
    {
        var im = GetComponentsInChildren<SpriteRenderer>();
        foreach (var el in im)
        {
            el.sprite = texture;
            
        }
    }

    void MoveBGElement(RectTransform t, float speed)
    {
        t.position += new Vector3(-speed, 0, 0);
    }
}
