using UnityEngine;

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

    public virtual bool AttemptIngredientAddition(IngredientData ingredient)
    {
        return false;
    }

    public bool CheckForContainer(Item item)
    {
        if (item is ContainerItem)
        {
            Debug.Log("Is container");
            return true;
        }

        return false;
    }
}
