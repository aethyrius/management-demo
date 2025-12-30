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
                drink.AttemptIngredientAddition(ingredient);
            }

            ContainerItem container = player.holding.GetComponent<ContainerItem>();
            if (container) {
                container.AttemptIngredientAddition(ingredient);
            }
        }
    }
}
