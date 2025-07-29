using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IJoyControllable
{
    //Animations
    private const int AIM_AND_ANIM_LAYER = 0; //Arm animations
    private const int LEG_LAYER = 1; //Leg Animations
    private int LEG_WALK_SPEED_PROPERTY = Animator.StringToHash("BlendSide");
   
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
    [SerializeField] private float m_movementSpeed;
    [SerializeField] private float m_rotationSpeed;
    [SerializeField] private Rigidbody m_rb;
    [SerializeField] private LayerMask m_floorLayermask;
    [SerializeField] private DamageableDetector m_visionArea;
    [SerializeField] private Weapon m_activeWeapon;

    private Vector3 m_offset;
    private bool m_isGrounded = false;
    private bool m_isControllable = false;
    private VirtualJoystick m_virtualJoystick;
    private Transform m_closetTarget = null;
    private float m_colliderRadius; 

    private void Awake()
    {
        GameplayEvents.GameStarted += GameplayEventsOnGameStarted;
        SetInitAnimations();
        SetUpController();
    }

    private void OnDestroy()
    {
        GameplayEvents.GameStarted -= GameplayEventsOnGameStarted;
    }

    private void GameplayEventsOnGameStarted()
    {
        m_isControllable = true;
        PlayAnimation(ANIM_ANIM_PISTOL);
    }

    private void SetUpController()
    {
        m_virtualJoystick = GetComponent<VirtualJoystick>();
        m_virtualJoystick.RegisterControllable(this);
        m_colliderRadius = GetComponent<CapsuleCollider>().radius;
    }

    private void SetInitAnimations()
    {
        PlayAnimation(ANIM_ANIM_PISTOL_DOWN);
        PlayAnimation(ANIM_ANIM_WALK, LEG_LAYER);
    }

    public void ControlChanged(Vector2 offset)
    {
        m_offset = new Vector3(offset.y * -1, 0, offset.x);
    }

    private void Update()
    {
        if (m_isControllable)
        {
            Ray ray = new Ray(transform.position + Vector3.up , -transform.up);
            if (Physics.SphereCast(ray, m_colliderRadius, out RaycastHit hit, Mathf.Infinity,m_floorLayermask))
            {
                m_rb.useGravity = false;
                m_isGrounded = true;
                m_isControllable = true;
                PlayAnimation(ANIM_ANIM_PISTOL);
            }
            else
            {
                m_rb.useGravity = true;
                m_isGrounded = false;
                m_isControllable = false;
                HandleFalling();
                m_activeWeapon.SetAiming(false);
            }
        }

        TryTarget();
    }
    
    private void TryTarget()
    {
        //Update the vision area
        //Then, see if we can target anything
        m_visionArea.UpdateVision();
        
        List<IDamageable> damageables = m_visionArea.GetDamageables();
        damageables.Sort(SortByDistance);
        if (damageables.Count > 0)
        {
            if (damageables[0].gameObject != null)
            {
                m_closetTarget = damageables[0].gameObject.transform;
                m_activeWeapon.SetAiming(true);
            }
        }
        else
        {
            m_closetTarget = null;
            m_activeWeapon.SetAiming(false);
        }
    }

    private int SortByDistance(IDamageable x, IDamageable y)
    {
        Vector3 currentPosition = transform.position;
        float distanceX = Vector3.Distance(currentPosition, x.gameObject.transform.position);
        float distanceY = Vector3.Distance(currentPosition, y.gameObject.transform.position);
        return distanceX.CompareTo(distanceY);
    }

    private void HandleFalling()
    {
        PlayAnimation(ANIM_ANIM_WALK_ARMS);
        m_anim.SetFloat(LEG_WALK_SPEED_PROPERTY, 0);
    }

    private void FixedUpdate()
    {
        if (m_isGrounded && m_isControllable)
        {
            float walkSpeed = m_offset.magnitude/m_virtualJoystick.GetFollowDistance();
            m_anim.SetFloat(LEG_WALK_SPEED_PROPERTY, walkSpeed);
            m_rb.AddForce(m_offset * m_movementSpeed * Time.deltaTime);
            
            if (m_offset.magnitude > 0)
            {
                if (m_closetTarget)
                {
                    Quaternion lookRotation = Quaternion.LookRotation((m_closetTarget.position - transform.position).normalized);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * m_rotationSpeed);
                }
                else
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(m_rb.velocity), Time.deltaTime * m_rotationSpeed);
                }
            }
        }
    }

    public void ControlStop()
    {
        m_offset = Vector3.zero;
    }

    public void ControlStart()
    {
    }

    public void OnLevelComplete()
    {
        m_isControllable = false;
        PlayAnimation(ANIM_ANIM_CELEBRATION_DANCE01);
        m_activeWeapon.SetAiming(false);
    }

    private void PlayAnimation(string anim, int layer = AIM_AND_ANIM_LAYER)
    {
        m_anim.Play(anim, layer);
    }

    public void MoveToPosition(Vector3 transformPosition, float duration = 0.7f)
    {
        StartCoroutine(MoveAndRotateTo(transform, transform.position, transformPosition, Quaternion.Euler(0, 90f, 0), duration));
    }
    
    private IEnumerator MoveAndRotateTo(Transform transform, Vector3 origin, Vector3 target, Quaternion targetRot, float duration, System.Action finishedCallback = null)
    {
        Quaternion rot = transform.rotation;
        float delta = 0f;
        while (delta <= duration)
        {
            delta += Time.deltaTime;
            float percent = (delta / duration);
            Vector3 posToMoveTo = Vector3.Lerp(origin, target, percent);
            transform.position = posToMoveTo;
            
            Quaternion rotToMoveTo = Quaternion.Lerp(rot, targetRot, percent);
            transform.rotation = rotToMoveTo;
            
            yield return null;
        }
        
        transform.position = target;
        transform.rotation = targetRot;
        
        if (finishedCallback != null)
        {
            finishedCallback();
        }
    }

    public void HandleWeaponSwap(Weapon newWeapon)
    {
        Transform weaponTransform = m_activeWeapon.transform.parent;
        Vector3 localPos = m_activeWeapon.transform.localPosition;

        m_activeWeapon.transform.parent = null;
        m_activeWeapon.HandleDrop(transform.position);
        m_activeWeapon.SetAiming(false);
        
        m_activeWeapon = newWeapon;
        m_activeWeapon.HandleCollected();
        m_activeWeapon.transform.parent = weaponTransform;
        m_activeWeapon.transform.localPosition = localPos;
        m_activeWeapon.transform.localRotation = Quaternion.identity;
    }
}