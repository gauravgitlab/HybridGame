using UnityEngine;
using UnityEngine.UI;

public class CosmeticItemCost : MonoBehaviour
{
    [SerializeField] private Text m_costText;
    private int m_cost = 0;
    
    private Color m_costAffordableColor = Color.green;
    private Color m_costUnaffordableColor = Color.gray;
    
    public void SetCost(int cost)
    {
        m_cost = cost;
        m_costText.text = $"{m_cost}";
        Refresh();
    }

    public void Enable(bool enable)
    {
        gameObject.SetActive(enable);
    }

    private void Refresh()
    {
        var totalCoins = GameBank.GetCurrency(GameManager.CURRENCY_COIN);
        m_costText.color = m_cost <= totalCoins ? m_costAffordableColor : m_costUnaffordableColor;
    }
}