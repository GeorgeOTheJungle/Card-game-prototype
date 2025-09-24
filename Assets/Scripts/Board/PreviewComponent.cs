using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewComponent : MonoBehaviour
{
    public static PreviewComponent Instance;

    [SerializeField] private SpriteRenderer m_spriteRenderer;

    private void Awake()
    {
        Instance = this;

        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void SetPreview()
    {
        SetPreview(new Vector2(100, 100), null);
    }
    public void SetPreview(Vector2 position, Sprite previewSprite)
    {
        transform.position = position;
        m_spriteRenderer.sprite = previewSprite;
    }
}
