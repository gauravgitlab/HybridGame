using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public static string CURRENCY_COIN = "currency_coin";
    public static string CURRENCY_INGAME_COIN = "currency_ingame_coin";
    
    public const string PLAYER_PROGRESSION_DATA = "player_progression_data";
    
    [SerializeField] private List<string> m_levels;
    
    private int m_currentLevel = 0;
    private Transform m_playerObject;

    public PlayerProgressionData m_playerProgressionData;

    protected override void Init()
    {
        GameBank.RegisterCurrency(CURRENCY_COIN);
        GameBank.RegisterCurrency(CURRENCY_INGAME_COIN);
        
        m_playerProgressionData = SaveSystem.Load<PlayerProgressionData>(PLAYER_PROGRESSION_DATA);
        m_currentLevel = m_playerProgressionData.m_level;
    }

    private void Start()
    {
        ScreenManager.Instance.GoToScreen(ScreenManager.Screen.START_SCREEN);
        LoadLevel();
    }

    public void OnLevelComplete()
    {
        GameplayEvents.SendLevelComplete();
        ScreenManager.Instance.GoToScreen(ScreenManager.Screen.LEVEL_COMPLETE_SCREEN);
    }
    
    public void OnLevelFailed()
    {
        GameplayEvents.SendGameOver();
        ScreenManager.Instance.GoToScreen(ScreenManager.Screen.LEVEL_FAILED_SCREEN);
    }
    
    public void LevelCompleted()
    {
        UnloadCurrentLevel();
        
        m_currentLevel++;
        if(m_currentLevel >= m_levels.Count)
        {
            // Reset to first level if all levels are completed
            m_currentLevel = 0; 
        }
        
        // save level
        m_playerProgressionData.m_level = m_currentLevel;
        SaveSystem.Save(m_playerProgressionData, PLAYER_PROGRESSION_DATA);
        
        // load level
        LoadLevel();
        ScreenManager.Instance.GoToScreen(ScreenManager.Screen.START_SCREEN);
    }

    private void UnloadCurrentLevel()
    {
        string currentLevel = m_levels[m_currentLevel];
        SceneManager.UnloadSceneAsync(currentLevel);
    }

    private void LoadLevel()
    {
        GameBank.OverrideCurrency(CURRENCY_INGAME_COIN, 0);
        string currentLevel = m_levels[m_currentLevel];
        SceneManager.LoadScene(currentLevel, LoadSceneMode.Additive);
        GameplayEvents.SendLevelReset();
    }

    public void Reload()
    {
        UnloadCurrentLevel();
        LoadLevel();
    }

    public void SetPlayer(Transform player)
    {
        m_playerObject = player;
    }

    public Transform GetPlayer()
    {
        return m_playerObject;
    }

    public void OnPurchaseCosmeticItem(string cosmeticCategory, string cosmeticId)
    {
        m_playerProgressionData = SaveSystem.Load<PlayerProgressionData>(PLAYER_PROGRESSION_DATA);
        m_playerProgressionData.AddPurchasedCosmetic(cosmeticCategory, cosmeticId);
        SaveSystem.Save(m_playerProgressionData, PLAYER_PROGRESSION_DATA);
    }
    
    public void OnEquipCosmeticItem(string cosmeticCategory, string cosmeticId)
    {
        m_playerProgressionData = SaveSystem.Load<PlayerProgressionData>(PLAYER_PROGRESSION_DATA);
        m_playerProgressionData.EquipCosmetic(cosmeticCategory, cosmeticId);
        SaveSystem.Save(m_playerProgressionData, PLAYER_PROGRESSION_DATA);
    }

    #if UNITY_EDITOR
    private void OnDestroy()
    {
        GameBank.Save();
    }
    #endif

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            GameBank.Save();
        }
    }
}