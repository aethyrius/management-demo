using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public List<IngredientData> allowedAdditions;

    [System.Serializable]
    public struct IngredientConversion
    {
        public IngredientData one;
        public IngredientData two;
        public IngredientData result;
    }

    public List<IngredientConversion> conversions;
}
