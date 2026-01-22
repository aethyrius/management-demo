using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance;
    public List<ItemOrder> drinkOrders = new List<ItemOrder>();
    public TMP_Text score;

    [SerializeField] int orderNum = 1;

    public GameObject ticketLine;
    public GameObject ticketPrefab;
    public List<Ticket> tickets;

    private void Awake()
    {
        Instance = this;
    }

    public void AddOrder(ItemOrder order)
    {
        drinkOrders.Add(order);
        CreateTicket(order);
    }

    public void CreateTicket(ItemOrder order)
    {
        GameObject newTicket = Instantiate(ticketPrefab, ticketLine.transform);
        Ticket ticket = newTicket.GetComponent<Ticket>();
        ticket.Draw(order);
        tickets.Add(ticket);
    }

    public void CompleteOrder(ItemOrder order)
    {
        int ticketIndex = drinkOrders.IndexOf(order);
        if (ticketIndex == -1)
        {
            Debug.Log("Order not found in drinkOrders");
            return;
        }

        Ticket ticket = tickets[ticketIndex];
        tickets.RemoveAt(ticketIndex);
        drinkOrders.Remove(order);

        Destroy(ticket.gameObject);

        if (int.TryParse(score.text, out int scoreVal))
        {
            score.text = (++scoreVal).ToString();
        }
        else
        {
            score.text = "1";
        }

    }

    public int GetNextOrderNum()
    {
        return orderNum++;
    }
}
