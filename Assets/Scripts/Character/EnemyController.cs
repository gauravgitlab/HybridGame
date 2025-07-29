using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{

    //Animations
    private const int AIM_AND_ANIM_LAYER = 0; //Arm animations
    private const int LEG_LAYER = 1; //Leg Animations
    
    private const string ANIM_ANIM_PISTOL = "Anim-Pistol";
    private const string ANIM_ANIM_MACHINEGUN = "Anim-Machinegun";
    private const string ANIM_ANIM_SHOTGUN = "Anim-Shotgun";
    private const string ANIM_ANIM_RUN_LEGS = "Anim-Run-Legs";
    private const string ANIM_ANIM_RUNARMS = "Anim-RunArms";
    private const string ANIM_ANIM_LAUNCHER = "Anim_Launcher";
    private const string ANIM_ANIM_WALK = "Anim-Walk";
    private const string ANIM_ANIM_WALK_ARMS = "Anim-Walk-Arms";
    private const string ANIM_ANIM_PISTOL_DOWN = "Anim-Pistol-Down";
    private const string ANIM_ANIM_MACHINEGUN_DOWN = "Anim-Machinegun-Down";
    private const string ANIM_ANIM_SHOTGUN_DOWN = "Anim-Shotgun-Down";
    private const string ANIM_ANIM_SIDESTEP = "Anim-Sidestep";
    private const string ANIM_ANIM_LEGS_IDLE = "Anim-Legs-Idle";
    private const string ANIM_ANIM_CELEBRATION_DANCE01 = "Anim-Celebration-Dance01";
    private const string ANIM_ANIM_NEW_WALK = "Anim-New-Walk";
    private const string ANIM_ANIM_NEW_PISTOL_DOWN = "Anim-New-Pistol-Down";
    private const string ANIM_ANIM_NEW_PISTOL = "Anim-New-Pistol";
    private const string ANIM_ANIM_NEW_PISTOL_ALT = "Anim-New-Pistol-Alt";
    private const string ANIM_ANIM_NEW_SIDESTEP = "Anim-New-Sidestep";
    private const string ANIM_ANIM_NEW_LEGS_IDLE = "Anim-New-Legs-Idle";
    private const string ANIM_ANIM_NEW_ENEMY_WALK = "Anim-New-Enemy-Walk";
    private const string ANIM_CHARACTER_BLEND_TREE = "Character Blend Tree";
    private const string ANIM_ANIM_WALK_ENEMY = "Anim-Walk-Enemy";
    
    [SerializeField] private Animator m_anim;
    [SerializeField] private float m_health;
    [SerializeField] private Collider m_mainCollider;
    [SerializeField] private Collider[] m_ragDollColliders;
    [SerializeField] private Rigidbody m_mainRB;
    [SerializeField] private Rigidbody[] m_ragDollRbs;
    [SerializeField] private WeaponDropScriptable m_weaponDropScriptable;

    private void Awake()
    {
        m_anim.Play(ANIM_ANIM_WALK_ARMS, AIM_AND_ANIM_LAYER);
    }

    public void OnHit(float damage)
    {
        m_health -= damage;
        if (m_health <= 0)
        {
            TurnIntoRagdoll();

            TryDropDroppables();

            EnemyManager.Instance.DeRegisterEnemy(this);

            Destroy(this);
        }
    }

    private void TurnIntoRagdoll()
    {
        //turn the animator off and the colliders/rigidbodies on
        m_anim.enabled = false;
        m_mainCollider.enabled = false;
        foreach (Collider ragDollCollider in m_ragDollColliders)
        {
            ragDollCollider.enabled = true;
            ragDollCollider.gameObject.layer = LayerMask.NameToLayer(TagsAndLayers.Layer.RAGDOLL);
        }

        foreach (Rigidbody m_rb in m_ragDollRbs)
        {
            m_rb.isKinematic = false;
        }
    }

    private void TryDropDroppables()
    {
        if (m_weaponDropScriptable)
        {
            m_weaponDropScriptable.Spawn(transform.position);
        }
    }

    public void Config(WeaponDropScriptable weaponDropScriptable)
    {
        m_weaponDropScriptable = weaponDropScriptable;
    }
}