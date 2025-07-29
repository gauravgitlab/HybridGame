using System;

public class GameplayEvents
{
    public static event Action GameStarted;
    public static void SendGameStarted()
    {
        GameStarted?.Invoke();
    }    
    
    public static event Action LevelComplete;
    public static void SendLevelComplete()
    {
        LevelComplete?.Invoke();
    }    
    
    public static event Action LevelReset;
    public static void SendLevelReset()
    {
        LevelReset?.Invoke();
    }    
    
    public static event Action GameOver;
    public static void SendGameOver()
    {
        GameOver?.Invoke();
    } 
    
    public static event Action CoinCollected;
    public static void SendCoinCollectedEvent()
    {
        CoinCollected?.Invoke();
    } 
    
    public static event Action<int> EnemyCountUpdated;
    public static void SendEnemyCountUpdated(int count)
    {
        EnemyCountUpdated?.Invoke(count);
    }
    
    public static event Action<string, int> CurrencyChanged;
    public static void SendCurrencyChanged(string id, int newAmount)
    {
        CurrencyChanged?.Invoke(id, newAmount);
    }
}