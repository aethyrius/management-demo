using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<DrinkRecipe> allDrinks;

    public ItemOrder GetRandomOrder(int orderNumber)
    {
        DrinkRecipe randomRecipe = allDrinks[Random.Range(0, allDrinks.Count)];

        Item.Temperature temp = randomRecipe.temperatures[Random.Range(0, randomRecipe.temperatures.Length)];

        /*ItemOrder order = new()
        {
            recipe = randomRecipe,
            temperature = randomRecipe.temperatures[Random.Range(0, randomRecipe.temperatures.Length)]

        };*/

        return new ItemOrder(orderNumber, randomRecipe, temp);
    }
}
