using UnityEngine;
using UnityEngine.UI;
public class InfoPanel : MonoBehaviour
{
    public Image panelInfoImage;
    public Text panelInfoNameText;
    public Text panelInfoTypeText;
    public Text panelUseItemShortDescText;
    public Text panelInfoLongDescText;

    public void ShowItemInfo(Item item)
    {
        panelInfoImage.sprite = item.image;
        panelInfoNameText.text = item.name;
        panelInfoTypeText.text = item.category.Description();
        panelInfoLongDescText.text = item.description;
        ShowItemUse(item);
    }

    private void ShowItemUse(Item item)
    {
        panelUseItemShortDescText.text = "";

        if (item.recoveryEnergy)
        {
            panelUseItemShortDescText.text += $"Recupera <color=#FFFF00>{item.energyAmount}</color> de Energia\n";
        }

        if (item.recoveryHealth)
        {
            panelUseItemShortDescText.text += $"Recupera <color=#FFFF00>{item.healthAmount}</color> de Vida\n";
        }

        if (item.recoveryMana)
        {
            panelUseItemShortDescText.text += $"Recupera <color=#FFFF00>{item.manaAmount}</color> de Mana\n";
        }
    }
}
