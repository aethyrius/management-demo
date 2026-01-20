using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Ticket : MonoBehaviour
{
    public int orderNum;

    public TMP_Text text;
    public TMP_Text numText;
    public Image image;

    public void Draw(DrinkRecipe recipe)
    {
        text.text = recipe.drinkName;
        numText.text = orderNum.ToString();
        image.sprite = recipe.sprite;
    }
}
