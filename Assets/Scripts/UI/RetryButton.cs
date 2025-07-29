using UnityEngine;

public class RetryButton : MonoBehaviour
{
    public void OnButtonPressed()
    {
        GameManager.Instance.Reload();
        ScreenManager.Instance.GoToScreen(ScreenManager.Screen.START_SCREEN);
    }
}