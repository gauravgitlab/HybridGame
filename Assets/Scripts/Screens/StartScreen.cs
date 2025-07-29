using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
   public void OnStartPressed()
   {
      GameplayEvents.SendGameStarted();
      ScreenManager.Instance.GoToScreen(ScreenManager.Screen.GAME_SCREEN);
   }
}
