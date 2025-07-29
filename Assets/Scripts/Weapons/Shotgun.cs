using UnityEngine;

public class Shotgun : Weapon
{
    private const string ANIM_IDLE = "Idle-Shotgun";
    private const string ANIM_SHOOT = "Shoot";
    [SerializeField] private float m_spreadAmount = 0.2f;
    [SerializeField] private float m_shotCount = 10f;

    protected override void Attack()
    {
        //shoot in a spread
        for (int i = 0; i < m_shotCount; i++)
        {
            Vector2 insideUnitCircle = Random.insideUnitCircle * m_spreadAmount;
            Bullet bullet = Instantiate(m_bullet, m_barrelEnd.position, Quaternion.LookRotation(transform.forward + new Vector3(0, insideUnitCircle.x, insideUnitCircle.y)), SceneRoot.Root); 
            bullet.OnFire(m_hitDamagePerAttack);
        }
        PlayVFX();
        PlayShootAnim();
        ResetTimer();
    }

    protected override string GetIdleAnimation()
    {
        return ANIM_IDLE;
    }

    protected override string GetShootAnimation()
    {
        return ANIM_SHOOT;
    }
}
