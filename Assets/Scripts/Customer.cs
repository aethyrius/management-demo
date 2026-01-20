using NUnit.Framework;
using System.Collections;
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

    public Transform registerPoint;
    private Vector3 destination;

    public Color defaultColor = Color.white;
    public Color highlightColor = Color.gray;

    public float moveSpeed = 3f;
    public float interactDistance = 2f;
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
        if (spawner.register.orderPoint == null) return;
        Customer prev = spawner.GetPrevCustomerInQueue(this);

        switch (state)
        {
            case (CustomerState.Queueing):
                if (prev)
                {
                    destination = new Vector3(prev.transform.position.x + customerDistance,
                                              prev.transform.position.y, transform.position.z);
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

        OrderManager.Instance.AddOrder(desiredItem);
        destination = spawner.GetRandomWaitingPosition();
        state = CustomerState.Waiting;
    }

    public bool IsCorrectItem(Item item)
    {
        return item.MatchesOrder(desiredItem);
    }

    public IEnumerator Leave()
    {
        state = CustomerState.Leaving;

        int random = Random.Range(0, 2);

        if (random == 0)
        {
            destination = new Vector3(5f, 20f, 0f);
        }
        else
        {
            destination = new Vector3(5f, -20f, 0f);
        }

        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
