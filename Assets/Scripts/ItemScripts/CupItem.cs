using System.Collections.Generic;
using UnityEngine;

public class CupItem : Item
{
    public List<IngredientData> currentAdditions = new();

    public override bool MatchesOrder(DrinkRecipe recipe)
    {
        if (currentAdditions.Count != recipe.requiredAdditions.Count) return false;

        foreach (IngredientData ingredient in currentAdditions)
        {
            if (!recipe.requiredAdditions.Contains(ingredient)) return false;
        }

        return true;
    }

    public override bool AttemptIngredientAddition(IngredientData newIngredient)
    {
        if (data.allowedAdditions.Contains(newIngredient) &&
                    !currentAdditions.Contains(newIngredient))
        {

            foreach (ItemData.IngredientConversion c in data.conversions)
            {
                foreach (IngredientData i in currentAdditions)
                {
                    if (i == c.one && newIngredient == c.two ||
                        newIngredient == c.one && i == c.two)
                    {
                        currentAdditions.Remove(i);
                        currentAdditions.Add(c.result);
                        return true;
                    }
                }
            }

            currentAdditions.Add(newIngredient);
            return true;
        }

        Debug.Log("Addition not allowed");
        return false;
    }
}
