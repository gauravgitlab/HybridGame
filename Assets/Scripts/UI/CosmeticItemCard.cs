using UnityEngine;
using UnityEngine.UI;

public class CosmeticItemCard : MonoBehaviour
{
    [SerializeField] private Image m_image;
    [SerializeField] private Text m_nameText;
    [SerializeField] private Text m_statusText;
    [SerializeField] private CosmeticItemCost m_cost;
    
    public void SetItemCard(CosmeticItemScriptable cosmeticItem)
    {
        name = cosmeticItem.m_id;
        m_nameText.text = cosmeticItem.m_displayName;
        m_image.sprite = cosmeticItem.m_icon;
        m_cost.SetCost(cosmeticItem.m_cost);
    }
}