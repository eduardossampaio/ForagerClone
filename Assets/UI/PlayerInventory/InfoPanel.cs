using UnityEngine;
using UnityEngine.UI;
public class InfoPanel : MonoBehaviour
{
    public Image panelInfoImage;
    public Text panelInfoNameText;
    public Text panelInfoTypeText;
    public Text panelInfoShortDescText;
    public Text panelInfoLongDescText;

    public void ShowItemInfo(Item item)
    {
        panelInfoImage.sprite = item.image;
        panelInfoNameText.text = item.name;
        panelInfoTypeText.text = item.category.ToString();
        panelInfoShortDescText.text = item.shortDescription;
        panelInfoLongDescText.text = item.description;
    }
}
