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
            ContainerItem container = placedItem.GetComponent<ContainerItem>();
            if (container)
            {
                Item playerItem = player.holding.GetComponent<Item>();
                
                if (playerItem is CupItem || playerItem is ContainerItem)
                {
                    container.Serve(playerItem);
                }
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