using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

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
}
