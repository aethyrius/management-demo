using Unity.VisualScripting;
using UnityEngine;

public class Surface : Interactable
{
    public Transform placedItem;

    private void Start()
    {
        if (placedItem)
        {
            placedItem.transform.localPosition = Vector3.zero;
        }
    }

    public override void OnInteract(PlayerInteraction player)
    {
        if (player.holding && !placedItem)
        {
            PlaceItem(player);
        }

        else if (!player.holding && placedItem)
        {
            PickUpItem(player);
        }
    }

    private void PlaceItem(PlayerInteraction player)
    {
        placedItem = player.holding;
        player.holding = null;

        Debug.Log("Placed item");

        placedItem.SetParent(transform);
        placedItem.localPosition = Vector3.zero;
    }

    private void PickUpItem(PlayerInteraction player)
    {
        Transform item = placedItem;
        placedItem = null;

        player.holding = item;

        item.SetParent(player.transform);
        item.localPosition = new Vector3(0, player.transform.localScale.y / 2, 0);
    }
}