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
