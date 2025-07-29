using UnityEngine;

public interface IJoyControllable
{
    void ControlChanged(Vector2 offset);
    void ControlStop();
    void ControlStart();
}

public class VirtualJoystick : MonoBehaviour
{
    [SerializeField] private float m_followDistance = 0.3f;
    [SerializeField] private float m_deadZone = 0.05f;
    [SerializeField] private float m_factor = 1f;
    [SerializeField] private Canvas m_canvas;

    private IJoyControllable m_controllable;
    private GameObject m_uiVisuals;

    private bool m_mouseDown;
    private Vector3 m_initialMousePress;
    private bool m_moving;

    private bool m_useFixedUpdate;

    public void RegisterControllable(IJoyControllable controllable, bool useFixedUpdate = false)
    {
        m_controllable = controllable;
        m_useFixedUpdate = useFixedUpdate;
    }

    public void DeregisterControllble()
    {
        m_controllable = null;
    }

    public void RegisterUIVisuals(GameObject uiVIsual)
    {
        m_uiVisuals = uiVIsual;
    }

    // Use this when you when you are blocked in one direction but don't want to move
    // the stick all the way back to move in the opposite way.
    public void ResetStickPosition()
    {
        m_controllable.ControlChanged(Vector2.zero);
        m_initialMousePress = Input.mousePosition;
    }

    private void Start()
    {
        m_controllable = GetComponent<IJoyControllable>();
        
        if (m_controllable != null && TouchArea.IsInputDown)
        {
            DownActions();  
        }
    }

    private void OnEnable()
    {
        TouchArea.Input += TouchArea_Input;
    }

    private void OnDisable()
    {
        if (m_mouseDown)
        {
            m_controllable.ControlStop();
        }

        TouchArea.Input -= TouchArea_Input;
        m_mouseDown = false;
        m_moving = false;
    }

    private void OnDestroy()
    {
        if (m_mouseDown)
        {
            m_controllable.ControlStop();
        }
    }
    
    void DownActions()
    {
        m_mouseDown = true;
        m_controllable.ControlStart();
        m_initialMousePress = Input.mousePosition;
        UIVisualSet();
    }

    void UpActions()
    {
        m_mouseDown = false;
        m_moving = false;
        m_controllable.ControlStop();
        UIVisualHide();
    }

    public float GetFollowDistance()
    {
        return m_followDistance;
    }
    
    private void TouchArea_Input(TouchArea.PressType inputType)
    {
        if (m_controllable != null)
        {
            if (inputType == TouchArea.PressType.DOWN)
            {
               DownActions();
            }
            else
            {
                UpActions();
            }
        }
    }

    private void Update()
    {
        if (!m_useFixedUpdate)
        {
            UpdateJoystick();
        }
    }

    private void FixedUpdate()
    {
        if (m_useFixedUpdate)
        {
            UpdateJoystick();
        }
    }

    private void UpdateJoystick()
    {
        if (m_mouseDown && m_controllable != null)
        {
            Vector3 offset = (Input.mousePosition - m_initialMousePress);
            offset /= Screen.width;

            float magnitude = offset.magnitude;
            if (magnitude > m_deadZone)
            {
                m_moving = true;
            }
            if (m_moving)
            {
                if (magnitude > m_followDistance)
                {
                    Vector3 normalizedOffset = offset.normalized;
                    offset = normalizedOffset * m_followDistance;
                    m_initialMousePress = Input.mousePosition - (normalizedOffset * m_followDistance * Screen.width);
                    UIVisualSet();
                }

                Vector3 factorizedOffset = offset * m_factor;
                m_controllable.ControlChanged(new Vector2(factorizedOffset.x, factorizedOffset.y));
            }
        }
    }

    private void UIVisualSet()
    {
        if (m_uiVisuals != null)
        {
            m_uiVisuals.SetActive(true);
        }
    }

    private void UIVisualHide()
    {
        if (m_uiVisuals != null)
        {
            m_uiVisuals.SetActive(false);
        }

    }
}
