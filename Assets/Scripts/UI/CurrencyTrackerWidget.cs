using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyTrackerWidget : MonoBehaviour
{
    [SerializeField] private Text m_text;
    [SerializeField] private string m_currencyId = GameManager.CURRENCY_COIN;
    private void Awake()
    {
        GameplayEvents.CurrencyChanged += GameplayEventsOnCurrencyChanged;
    }

    private void OnDestroy()
    {
        GameplayEvents.CurrencyChanged -= GameplayEventsOnCurrencyChanged;
    }

    void Start()
    {
        UpdateText();   
    }
    
    private void GameplayEventsOnCurrencyChanged(string currencyId, int newValue)
    {
        if (m_currencyId == currencyId)
        {
            UpdateText();
        }
    }

    void UpdateText()
    {
        m_text.text = GameBank.GetCurrency(m_currencyId).ToString();
    }

}
