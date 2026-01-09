using UnityEngine;
using TMPro;

public class ContainerItem : Item
{
    public IngredientData ingredient;
    public int maxServings;
    public int currServings = 0;
    private ServingsIndicator indicator;

    private void Start()
    {
        indicator = UIManager.Instance.CreateServingsIndicator(transform);
        indicator.SetValue(currServings);
    }

    public void Serve(Item into)
    {
        if (currServings <= 0)
        {
            Debug.Log("Not enough servings");
            return;
        }
        
        if (into is ContainerItem)
        {
            ContainerItem holding = into as ContainerItem;
            Debug.Log("Attempting to put " + holding.ingredient + " into " + name + " from " + holding.name);

            if (data.allowedAdditions.Contains(holding.ingredient))
            {
                foreach (ItemData.IngredientConversion c in data.conversions)
                {
                    if ((ingredient == c.one && holding.ingredient == c.two) ||
                        (holding.ingredient == c.one && ingredient == c.two))
                    {
                        if (AttemptIngredientAddition(holding.ingredient))
                        {
                            holding.currServings--;
                            holding.UpdateVisual();
                        }

                        ingredient = c.result;
                    }
                }

                if (holding.currServings <= 0)
                {
                    holding.currServings = 0;
                    holding.ingredient = null;
                    holding.UpdateVisual();
                }

                return;
            }
        }
        
        if (into.AttemptIngredientAddition(ingredient))
        {
            currServings--;
            UpdateVisual();
        }

        if (currServings <= 0)
        {
            currServings = 0;
            ingredient = null;
            UpdateVisual();
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
                UpdateVisual();
                return true;
            }
        }

        if (allowed && !ingredient && newIngredient)
            //allowed && (newIngredient == ingredient))
        {
            ingredient = newIngredient;
            currServings = maxServings;
            UpdateVisual();
            return true;
        }

        Debug.Log("Addition not allowed");
        return false;
    }

    public override void UpdateVisual()
    {
        indicator.SetValue(currServings);

        if (!ingredient)
        {
            spriteRenderer.sprite = data.visuals[0].sprite;
            return;
        }

        foreach (var visual in data.visuals)
        {
            if (visual.combination.Contains(ingredient))
            {
                spriteRenderer.sprite = visual.sprite;
            }
        }
    }
}
