using UnityEngine;

public class Credits : MonoBehaviour
{
    public float scrollSpeed = 50f;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        rectTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

        if (rectTransform.anchoredPosition.y > Screen.height)
        {
            Destroy(gameObject);
        }
    }
}
