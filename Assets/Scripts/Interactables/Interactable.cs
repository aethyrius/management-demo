using Unity.VisualScripting;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Color defaultColor;
    public Color highlightColor = Color.gray;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D collision;

    private void Awake()
    {
        collision = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
    }

    public virtual void OnInteract(PlayerInteraction player)
    {
        Debug.Log("Interacted");
    }

    public virtual void SetHighlighted(bool highlighted)
    {
        spriteRenderer.color = highlighted ? highlightColor : defaultColor;
    }
}
