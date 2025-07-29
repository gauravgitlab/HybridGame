using UnityEngine;

public class StateBase : MonoBehaviour
{
    public virtual void OnStateEnter()
    {
    }    
    
    public virtual void OnStateUpdate()
    {
    }   
    
    public virtual void OnStateExit()
    {
    }
}