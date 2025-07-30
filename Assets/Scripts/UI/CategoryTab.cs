using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CategoryTab : MonoBehaviour
{
    [SerializeField] private Text m_tabText;
    [SerializeField] private Button m_tabButton;

    public void SetTab(CosmeticCategory category, UnityAction<CosmeticCategory> onClickCallback)
    {
        name = $"{category}_Tab";
        m_tabText.text = category.ToString();
        m_tabButton.onClick.RemoveAllListeners();
        m_tabButton.onClick.AddListener(() => onClickCallback(category));
        gameObject.SetActive(true);
    }
}
