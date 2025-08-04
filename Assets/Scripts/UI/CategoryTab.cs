using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CategoryTab : MonoBehaviour
{
    [SerializeField] private Text m_tabText;
    [SerializeField] private Button m_tabButton;
    public string m_cosmeticCategory { get; private set; }
    
    public void SetTab(string cosmeticCategory, UnityAction<string> onClickCallback)
    {
        m_cosmeticCategory = cosmeticCategory;
        name = $"{cosmeticCategory}_Tab";
        m_tabText.text = cosmeticCategory;
        m_tabButton.onClick.RemoveAllListeners();
        m_tabButton.onClick.AddListener(() => onClickCallback(cosmeticCategory));
        gameObject.SetActive(true);
    }

    public void ChangeTabColor(Color color)
    {
        m_tabButton.GetComponent<Image>().color = color;
    }
}
