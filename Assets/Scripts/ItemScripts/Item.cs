using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;

public class Item : Interactable
{
    public ItemData data;

    public override void OnInteract(PlayerInteraction player)
    {
        if (player.holding) return;

        player.holding = transform;
        player.holding.GetComponent<Collider2D>().enabled = false;

        transform.SetParent(player.transform);
        transform.localPosition = new Vector3(0, player.transform.localScale.y / 2, 0);
    }

    public virtual bool MatchesOrder(DrinkRecipe recipe)
    {
        return false;
    }
}
