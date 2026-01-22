using UnityEngine;
using System.Collections;
using Unity.Collections;

public class Utility : Surface
{
    public UtilityData data;
    public float useTime = 5.0f;

    private ContainerItem item;
    private IngredientData conversion;
    private Coroutine timer = null;

    public override void OnInteract(PlayerInteraction player)
    {
        base.OnInteract(player);

        if (!placedItem)
        {
            if (timer != null) StopCoroutine(timer);
            return;
        }

        item = placedItem.GetComponent<ContainerItem>();

        if (!item || !item.ingredient || player.holding) return;

        foreach (IngredientData.UtilityConversion c in item.ingredient.utilityConversions)
        {
            if (c.utility == data)
            {
                conversion = c.result;
                timer = StartCoroutine(Utilize());
            }
        }
    }

    public IEnumerator Utilize()
    {
        Debug.Log("Utility timer started");

        if (!item || !conversion) yield break;

        yield return new WaitForSeconds(useTime);

        Debug.Log("Utility timer ended");

        item.ingredient = conversion;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.yellow;

        yield return new WaitForSeconds(2);

        spriteRenderer.color = originalColor;
    }
}
