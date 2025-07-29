using System.Collections;
using UnityEngine;

public class Coin : CollectableBase
{
   [SerializeField] private VFXPlayer m_vfxPlayer;
   [SerializeField] private float m_moveToTargetTime = 0.5f;

   private bool m_isMovingToTarget = false;

   protected override void OnCollected(Transform playerController)
   {
      if (!m_isMovingToTarget)
      {
         m_vfxPlayer.PlayVFX();
         m_isMovingToTarget = true;
         StartCoroutine(MoveTo(playerController, m_moveToTargetTime, OnFinishMove));
      }
   }

   private void OnFinishMove()
   {
      GameBank.AddCurrency(GameManager.CURRENCY_INGAME_COIN, 1);
      GameplayEvents.SendCoinCollectedEvent();
      Destroy(gameObject);
   }

   private IEnumerator MoveTo(Transform target, float duration, System.Action finishedCallback = null)
   {
      Vector3 origin = transform.position;

      float delta = 0f;
      while (delta <= duration)
      {
         delta += Time.deltaTime;
         Vector3 posToMoveTo = Vector3.Lerp(origin, target.position, delta / duration);
         transform.position = posToMoveTo;
         yield return null;
      }
        
      transform.position = target.position;

      finishedCallback?.Invoke();
   }
}