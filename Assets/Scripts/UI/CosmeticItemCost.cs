using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CosmeticItemCost : MonoBehaviour
{
    [SerializeField] private Button m_costButton;
    [SerializeField] private Text m_costText;
    
    private Color m_costAffordableColor = Color.black;
    private Color m_costUnaffordableColor = Color.gray;
    
    public void SetCost(int cost, UnityAction onClickCallback)
    {
        m_costText.text = $"{cost}";
        m_costButton.onClick.RemoveAllListeners();
        m_costButton.onClick.AddListener(onClickCallback);
    }

    public void RefreshUI(bool isAffordable)
    {
        gameObject.SetActive(true);
        if (isAffordable)
        {
            m_costButton.interactable = true;
            m_costText.color = m_costAffordableColor;
        }
        else
        {
            m_costButton.interactable = false;
            m_costText.color = m_costUnaffordableColor;
        }
    }
}