using NUnit.Framework;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    public CustomerSpawner spawner;
    public ItemOrder order;
    [SerializeField] private TMP_Text orderText;
    [SerializeField] private SpriteRenderer orderSprite;

    public Transform registerPoint;
    private Vector3 destination;

    public Color defaultColor = Color.white;
    public Color highlightColor = Color.gray;

    public float moveSpeed = 3f;
    public float customerDistance = 2f;

    public enum CustomerState
    {
        Queueing,
        Waiting,
        Leaving
    }

    public CustomerState state = CustomerState.Queueing;

    private void Update()
    {
        if (!spawner.register.orderPoint) return;
        Customer prev = spawner.GetPrevCustomerInQueue(this);

        switch (state)
        {
            case (CustomerState.Queueing):
                if (prev)
                {
                    destination = prev.transform.position + Vector3.right * customerDistance;
                }
                else
                {
                    destination = registerPoint.position;
                }
                break;

            case (CustomerState.Waiting):
                break;

            case (CustomerState.Leaving):
                break;

        }

        transform.position = Vector3.MoveTowards(
                    transform.position, destination, moveSpeed * Time.deltaTime);
    }

    public void PlaceOrder()
    {
        if (state != CustomerState.Queueing) return;

        OrderManager.Instance.AddOrder(order);
        destination = spawner.GetRandomWaitingPosition();
        state = CustomerState.Waiting;
    }

    /*public bool IsCorrectItem(Item item)
    {
        return item.MatchesOrder(order); // && (item.temperature == order.temperature));
    }*/

    public IEnumerator Leave()
    {
        state = CustomerState.Leaving;

        if (transform.position.y > registerPoint.transform.position.y)
        {
            destination = transform.position + (Vector3.up * 20f);
        }
        else
        {
            destination = transform.position + (Vector3.up * -20f);
        }

        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
