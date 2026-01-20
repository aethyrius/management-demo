using UnityEditor;
using UnityEngine;

public class Register : Interactable
{
    public CustomerSpawner spawner;
    public Transform orderPoint;
    public Transform waitingArea;

    private Customer highlightedCustomer;

    public override void OnInteract(PlayerInteraction player)
    {
        if (player.holding)
        {
            Item heldItem = player.holding.GetComponent<Item>();
            if (!heldItem) return;

            Customer matchedCustomer = null;

            foreach (Customer c in spawner.activeCustomers) {

                if (c.IsCorrectItem(heldItem)) {
                    matchedCustomer = c;
                    highlightedCustomer = c;
                    break;
                }
            }

            if (matchedCustomer)
            {
                OrderManager.Instance.CompleteOrder(matchedCustomer.desiredItem);
                matchedCustomer.StartCoroutine(matchedCustomer.Leave());

                Destroy(player.holding.gameObject);
                player.holding = null;

                spawner.activeCustomers.Remove(matchedCustomer);
                return;
            }
            else
            {
                Debug.Log("Nobody ordered that.");
            }
        }

        Customer currentCustomer = NextToOrder();
        if (!currentCustomer) return;

        if (CanInteract(currentCustomer))
        {
            currentCustomer.PlaceOrder();
        }
    }

    public Customer NextToOrder()
    {
        foreach (Customer c in spawner.activeCustomers)
        {
            if (c.state == Customer.CustomerState.Queueing)
            {
                highlightedCustomer = c;
                return c;
            }
        }

        return null;
    }

    public bool CanInteract(Customer customer)
    {
        if (customer.state == Customer.CustomerState.Queueing &&
            customer.state != Customer.CustomerState.Leaving &&
            Vector3.Distance(orderPoint.transform.position, customer.transform.position) < 1f)
        {
            highlightedCustomer = customer;
            return true;
        }

        SetHighlighted(false);
        return false;
    }

    public override void SetHighlighted(bool highlighted)
    {
        if (highlightedCustomer) {
            SpriteRenderer customerSprite = highlightedCustomer.GetComponent<SpriteRenderer>();
            customerSprite.color = highlighted ? highlightedCustomer.highlightColor : highlightedCustomer.defaultColor;
        }
    }
}
