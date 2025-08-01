using UnityEngine;

public class StartScreen : MonoBehaviour
{
   public void OnStartPressed()
   {
      GameplayEvents.SendGameStarted();
      ScreenManager.Instance.GoToScreen(ScreenManager.Screen.GAME_SCREEN);
   }
   
   public void OnShopPressed()
   {
      ScreenManager.Instance.GoToScreen(ScreenManager.Screen.SHOP_SCREEN);
   }
}
