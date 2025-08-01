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
    
    public static void AddCurrency(string currencyId, int amount)
    {
        if (m_bank.ContainsKey(currencyId))
        {
            m_bank[currencyId] += amount;
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
    
    public static bool CanAfford(string currencyId, int amount)
    {
        if (m_bank.ContainsKey(currencyId))
        {
            return m_bank[currencyId] >= amount;
        }

        Debug.LogError($"Currency Id {currencyId} has not been registered");
        return false;
    }
    
    public static bool Purchase(string currencyId, int amount)
    {
        if (m_bank.ContainsKey(currencyId))
        {
            if (!CanAfford(currencyId, amount))
                return false;
            
            m_bank[currencyId] -= amount;
            GameplayEvents.SendCurrencyChanged(currencyId, m_bank[currencyId]);
            return true;
        }

        Debug.LogError($"Currency Id {currencyId} has not been registered");
        return false;
    }
    
}
