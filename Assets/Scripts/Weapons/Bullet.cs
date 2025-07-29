using UnityEngine;

public class Bullet : MonoBehaviour
{
   [SerializeField] private Rigidbody m_rb;
   [SerializeField] private float m_movementSpeed = 1f;
   [SerializeField] private float m_lifeTime = 1f;
   [SerializeField] private VFXPlayer m_player;
   private float m_damage = 0;
   
   public void OnFire(float damage)
   {
      m_damage = damage;
      Destroy(gameObject, m_lifeTime);      
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.TryGetComponent(out IDamageable damageable))
      {
         OnHit(damageable);
      }
      else if(!other.isTrigger)
      {
         HitObject();
      }
   }

   private void HitObject()
   {
      m_player.PlayVFX();
      Destroy(gameObject);
   }

   protected virtual void OnHit(IDamageable damageable)
   {
      damageable.OnHit(m_damage);
      HitObject();
   }

   private void Update()
   {
      m_rb.MovePosition(m_rb.position + transform.forward * m_movementSpeed * Time.deltaTime);
   }
}