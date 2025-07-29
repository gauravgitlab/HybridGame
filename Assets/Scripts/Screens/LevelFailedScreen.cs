using UnityEngine;

public class LevelFailedScreen : MonoBehaviour
{
   public void OnHomePressed()
   {
      GameManager.Instance.Reload();
      ScreenManager.Instance.GoToScreen(ScreenManager.Screen.START_SCREEN);
   }
}