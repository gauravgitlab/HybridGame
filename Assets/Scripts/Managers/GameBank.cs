using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class GameBank 
{
    private static Dictionary<string, int> m_bank = new Dictionary<string, int>();

    public static void RegisterCurrency(string currencyId)
    {
        int initialValue = PlayerPrefs.GetInt(currencyId, 0);
        m_bank.Add(currencyId, initialValue);
    }

    public static void Save()
    {
        foreach (string key in m_bank.Keys)
        {
            PlayerPrefs.SetInt(key, m_bank[key]);
        }
    }
    
    public static void AddCurrency(string currencyId, int ammount)
    {
        if (m_bank.ContainsKey(currencyId))
        {
            m_bank[currencyId] += ammount;
            GameplayEvents.SendCurrencyChanged(currencyId, m_bank[currencyId]);
        }
        else
        {
            Debug.LogError($"Currency Id {currencyId} has not been registered");
        }
    }
    
    public static void OverrideCurrency(string currencyId, int newValue)
    {
        if (m_bank.ContainsKey(currencyId))
        {
            m_bank[currencyId] = newValue;
            GameplayEvents.SendCurrencyChanged(currencyId, m_bank[currencyId]);
        }
        else
        {
            Debug.LogError($"Currency Id {currencyId} has not been registered");
        }
    }
    
    public static int GetCurrency(string currencyId)
    {
        if (m_bank.ContainsKey(currencyId))
        {
            return m_bank[currencyId];
        }
        Debug.LogError($"Currency Id {currencyId} has not been registered");
        return 0;
    }
    
    public static bool CanAfford(string currencyId, int ammount)
    {
        //TODO: please, fill me
        return true;
    }
    
    public static bool Purchase(string currencyId, int ammount)
    {
        //TODO, please fill me
        GameplayEvents.SendCurrencyChanged(currencyId, m_bank[currencyId]);
        return true;
    }
    
}
