using UnityEngine;

public class ItemDispenser : Interactable
{
    public GameObject item;

    public override void OnInteract(PlayerInteraction player)
    {
        if (!player.holding)
        {
            GameObject newItem = Instantiate(item);
            newItem.GetComponent<Collider2D>().enabled = false;
            newItem.transform.SetParent(player.transform);
            newItem.transform.localPosition = new Vector3(0, player.transform.localScale.y / 2, 0);
            player.holding = newItem.transform;
        }
    }
}
