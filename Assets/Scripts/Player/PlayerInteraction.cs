using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class PlayerInteraction : MonoBehaviour {

    public float interactRadius = 4f;
    public Transform holding;
    public Interactable interactable;

    void Update()
    {
        Interactable closest = GetClosestInteractable();

        if (closest != interactable)
        {
            if (interactable)
                interactable.SetHighlighted(false);

            interactable = closest;
        }

        if (interactable)
            interactable.SetHighlighted(true);

        /* Interact */

        if (interactable && Input.GetKeyDown(KeyCode.J))
        {
            interactable.OnInteract(this);
        }

        /* Drop item */

        if (holding && Input.GetKeyDown(KeyCode.X))
        {
            DropHeldItem();
        }
    }

    private Interactable GetClosestInteractable()
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactRadius);
        Interactable closest = null;

        foreach (Collider2D c in colliders) {

            Interactable curr = c.GetComponent<Interactable>();
            
            if (!curr) continue;

            if (closest == null ||
                Vector2.Distance(transform.position, c.transform.position) < 
                Vector2.Distance(transform.position, closest.transform.position))
            {
                closest = curr;
            }
        }
        
        return closest;
    }

    private void DropHeldItem()
    {
        holding.SetParent(null);
        holding.position = transform.position;
        holding.GetComponent<Collider2D>().enabled = true;
        holding = null;
    }
}
