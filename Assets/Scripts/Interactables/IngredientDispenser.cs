using UnityEngine;

public class IngredientDispenser : Interactable
{
    public IngredientData ingredient;

    public override void OnInteract(PlayerInteraction player)
    {
        if (player.holding)
        {
            CupItem drink = player.holding.GetComponent<CupItem>();

            if (drink)
            {
                if (drink.data.allowedAdditions.Contains(ingredient) &&
                    !drink.currentAdditions.Contains(ingredient))
                {
                    drink.GetComponent<CupItem>().currentAdditions.Add(ingredient);
                }
                else
                {
                    Debug.Log("Addition not allowed");
                }
            }
        }
    }
}
