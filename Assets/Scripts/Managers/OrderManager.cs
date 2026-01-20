using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance;
    public List<DrinkRecipe> drinkOrders;
    public TMP_Text score;

    [SerializeField] int orderNum = 1;

    public GameObject ticketLine;
    public GameObject ticketPrefab;
    public List<Ticket> tickets;

    private void Awake()
    {
        Instance = this;
    }

    public void AddOrder(DrinkRecipe recipe)
    {
        drinkOrders.Add(recipe);
        CreateTicket(recipe);
    }

    public void CreateTicket(DrinkRecipe recipe)
    {
        GameObject newTicket = Instantiate(ticketPrefab, ticketLine.transform);
        Ticket ticket = newTicket.GetComponent<Ticket>();
        ticket.orderNum = orderNum++;
        ticket.Draw(recipe);
        tickets.Add(ticket);
    }

    public void CompleteOrder(DrinkRecipe recipe)
    {
        int ticketIndex = drinkOrders.IndexOf(recipe);

        Ticket ticket = tickets[ticketIndex];
        tickets.RemoveAt(ticketIndex);
        drinkOrders.Remove(recipe);

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
}
