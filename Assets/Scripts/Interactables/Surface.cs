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

        else if (player.holding && placedItem)
        {
            Item placed = placedItem.GetComponent<Item>();
            Item playerItem = player.holding.GetComponent<Item>();

            if (placed is ContainerItem)
            {
                if (playerItem is CupItem || playerItem is ContainerItem)
                {
                    (placed as ContainerItem).Serve(playerItem);
                }
            }
            else if (playerItem is ContainerItem && placed is CupItem)
            {
                (playerItem as ContainerItem).Serve(placed);
            }
        }
    }

    private void PlaceItem(PlayerInteraction player)
    {
        placedItem = player.holding;
        player.holding = null;

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