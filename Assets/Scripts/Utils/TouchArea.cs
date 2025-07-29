using UnityEngine;
using UnityEngine.EventSystems;


public class TouchArea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private bool m_supportMultiTouch = true; 
    static bool s_pressed = false;
    
    private int m_lastTouchID;

    public enum PressType
    {
        DOWN,
        UP
    }

    public delegate void InputEvent(PressType press);

    public static event InputEvent Input;

    public static bool IsInputDown => s_pressed;

    public static void SendInputEvent(PressType press)
    {
        if (Input != null)
        {
            Input(press);
        }
    }
    
    void UpActions()
    {
        s_pressed = false;
        SendInputEvent(PressType.UP);
    }
    
    void DownActions()
    {
        s_pressed = true;
        SendInputEvent(PressType.DOWN);
    }

    private void OnDisable()
    {
        if (s_pressed)
        {
            UpActions();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!m_supportMultiTouch)
        {
            if (!s_pressed)
            {
                m_lastTouchID = eventData.pointerId;
                DownActions();
            }
        }
        else
        {
            DownActions();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!m_supportMultiTouch)
        {
            if (m_lastTouchID == eventData.pointerId)
            {
                UpActions();
            }
        }
        else
        {
            UpActions();
        }
    }
}

