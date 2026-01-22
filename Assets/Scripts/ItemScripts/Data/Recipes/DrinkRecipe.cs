using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "RecipeData", menuName = "Scriptable Objects/RecipeData")]
public class DrinkRecipe : ScriptableObject
{
    public string drinkName;
    public List<IngredientData> requiredAdditions;
    public Sprite sprite;
    public Item.Temperature[] temperatures;
}
