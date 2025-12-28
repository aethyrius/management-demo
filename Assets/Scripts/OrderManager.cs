using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance;
    public List<DrinkRecipe> drinkOrders;

    private void Awake()
    {
        Instance = this;
    }

    public void AddOrder(DrinkRecipe recipe)
    {
        drinkOrders.Add(recipe);
    }

    public void CompleteOrder(DrinkRecipe recipe)
    {
        drinkOrders.Remove(recipe);
    }
}
