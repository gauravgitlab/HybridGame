using UnityEngine;

public class Weapon : WeaponDroppable
{
    [SerializeField] protected float m_hitDamagePerAttack = 1;
    [SerializeField] protected float m_timeBetweenAttacks = 0.5f;
    [SerializeField] protected float m_timeBetweenFirstAttack = 0.2f;
    [SerializeField] protected Animator m_anim;
    [SerializeField] protected Bullet m_bullet;
    [SerializeField] protected Transform m_barrelEnd;
    [SerializeField] protected VFXPlayer[] m_VFXPlayers;

    private float m_attackTimer = 0;
    private bool m_aiming = false;
    
    void Start()
    {
        m_attackTimer = m_timeBetweenFirstAttack;
    }

    public void SetAiming(bool isAiming)
    {
        //the weapon was aiming, but it isnt now. Reset the timer, ready for next time
        if (!isAiming)
        {
            m_attackTimer = m_timeBetweenFirstAttack;
        }

        m_aiming = isAiming;
    }

    void Update()
    {
        if (m_aiming)
        {
            m_attackTimer -= Time.deltaTime;
            if (m_attackTimer <= 0)
            {
                Attack();
            }
        }
    }

    protected void PlayShootAnim()
    {
        if (m_anim)
        {
            m_anim.Play(GetShootAnimation());
        }
    }
    
    protected void PlayIdleAnimation()
    {
        if (m_anim)
        {
            m_anim.Play(GetIdleAnimation());
        }
    }

    protected virtual string GetShootAnimation()
    {
        return "";
    }    
    
    protected virtual string GetIdleAnimation()
    {
        return "";
    }

    protected virtual void Attack()
    {
        Bullet bullet = Instantiate(m_bullet, m_barrelEnd.position, Quaternion.LookRotation(transform.forward), SceneRoot.Root);
        bullet.OnFire(m_hitDamagePerAttack);
        PlayVFX();
        PlayShootAnim();
        ResetTimer();
    }

    protected void PlayVFX()
    {
        foreach (VFXPlayer vfxPlayer in m_VFXPlayers)
        {
            vfxPlayer.PlayVFX();
        }
    }

    protected void ResetTimer()
    {
        m_attackTimer = m_timeBetweenAttacks;
    }
}