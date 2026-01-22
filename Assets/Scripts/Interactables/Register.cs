using UnityEditor;
using UnityEngine;

public class Register : Interactable
{
    public CustomerSpawner spawner;
    public Transform orderPoint;
    public Transform waitingArea;
    private float customerReach = 0.5f;

    public override void OnInteract(PlayerInteraction player)
    {
        if (player.holding)
        {
            Item heldItem = player.holding.GetComponent<Item>();
            if (!heldItem) return;

            Customer matchedCustomer = null;

            foreach (Customer c in spawner.activeCustomers) {

                if (heldItem.MatchesOrder(c.order)) {
                    matchedCustomer = c;
                    break;
                }
            }

            if (matchedCustomer)
            {
                OrderManager.Instance.CompleteOrder(matchedCustomer.order);
                matchedCustomer.StartCoroutine(matchedCustomer.Leave());

                Destroy(player.holding.gameObject);
                player.holding = null;

                spawner.activeCustomers.Remove(matchedCustomer);
                matchedCustomer = null;
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
            SetHighlighted(false);
            currentCustomer.PlaceOrder();
        }
    }

    public Customer NextToOrder()
    {
        foreach (Customer c in spawner.activeCustomers)
        {
            if (c.state == Customer.CustomerState.Queueing)
            {
                return c;
            }
        }

        return null;
    }

    public bool CanInteract(Customer customer)
    {
        if (customer.state == Customer.CustomerState.Queueing &&
            Vector3.Distance(customer.transform.position, orderPoint.transform.position) < customerReach)
        {
            return true;
        }

        return false;
    }

    public override void SetHighlighted(bool highlighted)
    {
        Customer customer = NextToOrder();
        if (customer && Vector3.Distance(customer.transform.position, orderPoint.transform.position) < customerReach)
        {
            customer.GetComponent<SpriteRenderer>().color = highlighted ? customer.highlightColor : customer.defaultColor;
        }
    }
}
