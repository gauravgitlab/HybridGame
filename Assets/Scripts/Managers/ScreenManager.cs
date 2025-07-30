using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : Singleton<ScreenManager>
{
    //Enum order MUST match the order of the screens added to the list in awake
    public enum Screen
    {
        START_SCREEN = 0,
        GAME_SCREEN = 1,
        LEVEL_COMPLETE_SCREEN = 2,
        LEVEL_FAILED_SCREEN = 3,
        SHOP_SCREEN = 4,
    }
    
    [Header("Screens")] 
    [SerializeField] private GameObject m_startScreen;
    [SerializeField] private GameObject m_gameScreen;
    [SerializeField] private GameObject m_levelCompleteScreen;
    [SerializeField] private GameObject m_levelFailedScreen;
    [SerializeField] private GameObject m_shopScreen;
    
    [Header("Canvas")]
    [SerializeField] private Canvas m_mainCanvas;

    private List<GameObject> m_screens = new List<GameObject>();

    private GameObject m_currentScreen;
    
    private void Awake()
    {
        m_screens.Add(m_startScreen);
        m_screens.Add(m_gameScreen);
        m_screens.Add(m_levelCompleteScreen);
        m_screens.Add(m_levelFailedScreen);
        m_screens.Add(m_shopScreen);
    }

    public void GoToScreen(Screen screen)
    {
        if (m_currentScreen != null)
        {
            Destroy(m_currentScreen);
        }
        m_currentScreen = Instantiate(m_screens[(int)screen], m_mainCanvas.transform);
    }
}