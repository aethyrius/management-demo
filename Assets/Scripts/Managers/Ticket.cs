using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Ticket : MonoBehaviour
{
    public TMP_Text text;
    public TMP_Text numText;
    public Image image;

    public void Draw(ItemOrder order)
    {
        string temperatureText;
        switch (order.temperature)
        {
            case (Item.Temperature.Hot):
                temperatureText = "Hot ";
                break;
            case (Item.Temperature.Iced):
                temperatureText = "Iced ";
                break;
            default: 
                temperatureText = "";
                break;
        }
        
        text.text = temperatureText + order.recipe.drinkName;

        numText.text = order.orderNumber.ToString();
        image.sprite = order.recipe.sprite;
    }
}
