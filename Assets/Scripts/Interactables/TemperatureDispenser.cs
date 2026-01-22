using UnityEngine;

public class TemperatureDispenser : Interactable
{
    public Item.Temperature temperature;

    public override void OnInteract(PlayerInteraction player)
    {
        if (player.holding)
        {
            CupItem drink = player.holding.GetComponent<CupItem>();
            if (drink)
            {
                drink.ChangeTemperature(temperature);
            }
        }
    }
}
