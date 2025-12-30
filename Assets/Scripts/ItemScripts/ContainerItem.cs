using UnityEngine;

public class ContainerItem : Item
{
    public IngredientData ingredient;
    public int maxServings;
    public int currServings;

    public void Serve(Item into)
    {
        if (currServings <= 0)
        {
            Debug.Log("Not enough servings");
            return;
        }
        
        /*if (into is ContainerItem)
        {
            ContainerItem container = (ContainerItem) into;
            Debug.Log("add later");

            foreach (ItemData.IngredientConversion c in into.data.conversions)
            {
                if ((ingredient == c.one && container.ingredient == c.two) ||
                    (container.ingredient == c.one && .ingredient == c.two))
                {
                    currServings--;
                    container.ingredient = c.result;
                }
            }
        }*/
        
        if (into.AttemptIngredientAddition(ingredient))
        {
            currServings--;
        }

        if (currServings <= 0)
        {
            currServings = 0;
            ingredient = null;
        }
    }

    public override bool AttemptIngredientAddition(IngredientData newIngredient)
    {
        bool allowed = data.allowedAdditions.Contains(newIngredient);
        if (!allowed) return false;

        foreach (ItemData.IngredientConversion c in data.conversions)
        {
            if (ingredient == c.one && newIngredient == c.two ||
                newIngredient == c.one && ingredient == c.two)
            {
                ingredient = c.result;
                return true;
            }
        }

        if (allowed && !ingredient ||
            allowed && (newIngredient == ingredient))
        {

            ingredient = newIngredient;
            currServings = maxServings;
            return true;
        }

        Debug.Log("Addition not allowed");
        return false;
    }
}
