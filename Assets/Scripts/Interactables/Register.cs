using UnityEditor;
using UnityEngine;

public class Register : Interactable
{
    public CustomerSpawner spawner;

    public override void OnInteract(PlayerInteraction player)
    {
        Customer currentCustomer = GetCurrentCustomer();
        if (!currentCustomer) return;

        if (!currentCustomer.hasPlacedOrder)
        {
            currentCustomer.PlaceOrder();
        }
        else if (player.holding)
        {
            Item heldItem = player.holding.GetComponent<Item>();

            if (heldItem && currentCustomer.IsCorrectItem(heldItem))
            {
                Debug.Log("Yay");

                OrderManager.Instance.CompleteOrder(currentCustomer.desiredItem);
                spawner.activeCustomers.Remove(currentCustomer);

                Destroy(currentCustomer.gameObject);
                Destroy(player.holding.gameObject);
                player.holding = null;
            }
            else
            {
                Debug.Log("I don't want that");
            }
        }
    }

    public Customer GetCurrentCustomer()
    {
        if (!spawner) return null;

        Customer next = spawner.GetNextCustomer();
        if (next == null) return null;

        if (!next.CanInteract()) return null;

        return next;
    }

    public override void SetHighlighted(bool highlighted)
    {
        Customer customer = GetCurrentCustomer();
        if (customer) {
            SpriteRenderer customerSprite = customer.GetComponent<SpriteRenderer>();
            customerSprite.color = highlighted ? customer.highlightColor : customer.defaultColor;
        }
    }
}
