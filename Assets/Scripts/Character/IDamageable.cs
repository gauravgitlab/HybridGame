using UnityEngine;

public interface IDamageable
{
    GameObject gameObject { get ; } 
    void OnHit(float damage);
}