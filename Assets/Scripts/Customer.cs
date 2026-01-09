using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    public CustomerSpawner spawner;
    public DrinkRecipe desiredItem;
    [SerializeField] private TMP_Text orderText;
    [SerializeField] private SpriteRenderer orderSprite;
    public Color defaultColor = Color.white;
    public Color highlightColor = Color.gray;

    public Transform registerPoint;
    public float moveSpeed = 2f;
    public float interactDistance = 2f;
    public float customerDistance = 2f;

    public bool hasPlacedOrder = false;

    private void Update()
    {
        if (registerPoint == null) return;

        Customer prev = spawner.GetPrevCustomerInList(this);

        Vector3 destination;

        if (prev)
        {
            destination = new Vector3(prev.transform.position.x + customerDistance, 
                                      prev.transform.position.y, transform.position.z);
        }
        else
        {
            destination = registerPoint.position;
        }

        transform.position = Vector3.MoveTowards(
            transform.position, destination, moveSpeed * Time.deltaTime
        );
    }

    public bool CanInteract()
    {
        return (Vector2.Distance(transform.position, registerPoint.position) < interactDistance);
    }

    public void PlaceOrder()
    {
        if (!hasPlacedOrder)
        {
            //Debug.Log("Customer wants " + desiredItem.name);
            OrderManager.Instance.AddOrder(desiredItem);
            hasPlacedOrder = true;

            orderText.text = desiredItem.drinkName;
            orderSprite.sprite = desiredItem.sprite;
        }
    }

    public bool IsCorrectItem(Item item)
    {
        return item.MatchesOrder(desiredItem);
    }
}
