using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<DrinkRecipe> allDrinks;

    public DrinkRecipe GetRandomDrink()
    {
        return allDrinks[Random.Range(0, allDrinks.Count)];
    }
}
