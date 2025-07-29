using UnityEngine;

public class StateBehaviour : MonoBehaviour
{
    private StateBase[] m_states;
    private StateBase m_currentBase;
    
    protected virtual void Awake()
    {
        m_states = GetComponents<StateBase>();
    }

    protected void GoToState(int state)
    {
        if (m_currentBase)
        {
            m_currentBase.OnStateExit();
        }
        m_currentBase = m_states[state];
        m_currentBase.OnStateEnter();
    }

    protected void UpdateCurrentState()
    {
        m_currentBase.OnStateUpdate();
    }
}