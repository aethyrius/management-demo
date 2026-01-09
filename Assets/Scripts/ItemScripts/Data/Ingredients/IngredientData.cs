using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IngredientData", menuName = "Scriptable Objects/Ingredient")]
public class IngredientData : ScriptableObject
{
    public string ingredientName;
    public Sprite sprite;
    public Dictionary<UtilityData, IngredientData> convertsTo;

    [System.Serializable]
    public struct UtilityConversion
    {
        public UtilityData utility;
        public IngredientData result;
    }

    public List<UtilityConversion> utilityConversions;
}