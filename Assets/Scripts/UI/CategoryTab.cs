using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CategoryTab : MonoBehaviour
{
    [SerializeField] private Text m_tabText;
    [SerializeField] private Button m_tabButton;

    public CosmeticCategory m_category;
    
    public void SetTab(CosmeticCategory category, UnityAction<CosmeticCategory> onClickCallback)
    {
        m_category = category;
        name = $"{category}_Tab";
        m_tabText.text = category.ToString();
        m_tabButton.onClick.RemoveAllListeners();
        m_tabButton.onClick.AddListener(() => onClickCallback(category));
        gameObject.SetActive(true);
    }

    public void ChangeTabColor(Color color)
    {
        m_tabButton.GetComponent<Image>().color = color;
    }
}
