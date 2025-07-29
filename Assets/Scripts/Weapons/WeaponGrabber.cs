using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGrabber : MonoBehaviour
{
    [SerializeField] private float m_gracePeriod = 2f;
    [SerializeField] private PlayerController m_player;
    [SerializeField] private Collider m_collider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Weapon weapon))
        {
            m_player.HandleWeaponSwap(weapon);
            StartCoroutine(GracePeriodForCollection());
        }
    }
    
    private IEnumerator GracePeriodForCollection()
    {
        m_collider.enabled = false;
        yield return new WaitForSeconds(m_gracePeriod);
        m_collider.enabled = true;
    }
}
