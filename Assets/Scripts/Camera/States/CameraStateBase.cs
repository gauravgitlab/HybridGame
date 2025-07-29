using UnityEngine;

public class CameraStateBase : StateBase
{
    [SerializeField] protected Vector3 m_targetOffset;
    [SerializeField] protected float m_smoothTime = 0.3f;
    protected Vector3 velocity = Vector3.zero;
    protected Transform m_target;

    public override void OnStateUpdate()
    {
        if (m_target == null)
        {
            m_target = GameManager.Instance.GetPlayer();
        }

        if (m_target)
        {
            SetPosition();
        }
    }

    protected virtual void SetPosition()
    {
        Vector3 targetPosition = m_target.position + m_targetOffset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, m_smoothTime);
    }
}