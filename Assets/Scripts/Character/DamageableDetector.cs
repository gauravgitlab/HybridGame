using System.Collections.Generic;
using UnityEngine;

public class DamageableDetector : MonoBehaviour
{
    private List<IDamageable> m_damageables;

    private void Awake()
    {
        m_damageables = new List<IDamageable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            if (!m_damageables.Contains(damageable))
            {
                m_damageables.Add(damageable);
            }
        }
    }

    public void UpdateVision()
    {
        for (int i = m_damageables.Count - 1; i >= 0; i--)
        {
            if (m_damageables[i] as MonoBehaviour == null)
            {
                m_damageables.RemoveAt(i);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            if (m_damageables.Contains(damageable))
            {
                m_damageables.Remove(damageable);
            }
        }
    }

    public List<IDamageable> GetDamageables()
    {
        return m_damageables;
    }
}