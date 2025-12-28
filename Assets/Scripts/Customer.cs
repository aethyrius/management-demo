using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public DrinkRecipe desiredItem;
    public Color defaultColor = Color.white;
    public Color highlightColor = Color.gray;

    public Transform targetPoint;
    public float moveSpeed = 2f;
    public float interactDistance = 2f;

    public bool hasPlacedOrder = false;

    private void Update()
    {
        if (targetPoint == null) return;

        transform.position = Vector3.MoveTowards(
            transform.position, targetPoint.position, moveSpeed * Time.deltaTime
        );
    }

    public bool CanInteract()
    {
        return (Vector2.Distance(transform.position, targetPoint.position) < interactDistance);
    }

    public void PlaceOrder()
    {
        if (!hasPlacedOrder)
        {
            Debug.Log("Customer wants " + desiredItem.name);
            OrderManager.Instance.AddOrder(desiredItem);
            hasPlacedOrder = true;
        }
    }

    public bool IsCorrectItem(Item item)
    {
        return item.MatchesOrder(desiredItem);
    }
}
