using UnityEngine;

public class ItemOrder
{
    public int orderNumber;
    public DrinkRecipe recipe;
    public Item.Temperature temperature;

    public ItemOrder(int orderNumber, DrinkRecipe recipe, Item.Temperature temperature)
    {
        this.orderNumber = orderNumber;
        this.recipe = recipe;
        this.temperature = temperature;
    }
}
