using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteScreen : MonoBehaviour
{
   

   public void OnHomePressed()
   {
      int earnings = GameBank.GetCurrency(GameManager.CURRENCY_INGAME_COIN);
      GameBank.AddCurrency(GameManager.CURRENCY_COIN, earnings);
      
      GameManager.Instance.LevelCompleted();
   }
}
