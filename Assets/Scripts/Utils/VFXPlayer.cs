using UnityEngine;

public class VFXPlayer : MonoBehaviour
{
    [SerializeField] private Transform m_parent;
    [SerializeField] private GameObject m_VFX;
    [SerializeField] private float m_destroyTime = 1f;

    public void PlayVFX()
    {
        GameObject go = Instantiate(m_VFX, m_parent.transform.position, m_parent.transform.rotation, SceneRoot.Root);
        Destroy(go, m_destroyTime);
    }
}
