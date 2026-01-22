using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Item : Interactable
{
    public ItemData data;
    public enum Temperature
    {
        Normal,
        Hot,
        Iced
    }

    public Temperature temperature;

    private Animator animator;

    public void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void ChangeTemperature(Temperature temperature)
    {
        if (animator)
        {
            switch (temperature)
            {
                case (Temperature.Normal):
                    return;

                case (Temperature.Hot):

                    if (this.temperature == Temperature.Iced)
                    {
                        this.temperature = Temperature.Normal;
                        animator.runtimeAnimatorController = null;
                        return;
                    }

                    animator.runtimeAnimatorController = data.hotAnim;
                    break;

                case (Temperature.Iced):

                    animator.runtimeAnimatorController = data.icedAnim;
                    break;
            }

            this.temperature = temperature;
            return;
        }
    }

    public override void OnInteract(PlayerInteraction player)
    {
        if (player.holding) return;

        player.holding = transform;
        player.holding.GetComponent<Collider2D>().enabled = false;

        transform.SetParent(player.transform);
        transform.localPosition = new Vector3(0, player.transform.localScale.y / 2, 0);
    }

    public virtual bool MatchesOrder(ItemOrder order)
    {
        return false;
    }

    public virtual bool AttemptIngredientAddition(IngredientData ingredient)
    {
        return false;
    }

    public bool CheckForContainer(Item item)
    {
        if (item is ContainerItem)
        {
            Debug.Log("Is container");
            return true;
        }

        return false;
    }

    public virtual void UpdateVisual()
    {
        return;
    }

    public virtual bool MatchesCombination()
    {
        return false;
    }
}
