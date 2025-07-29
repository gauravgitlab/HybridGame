using UnityEngine;

public class FinishAreaHitBox : MonoBehaviour
{
    [SerializeField] private GameObject[] m_confettiVFX;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagsAndLayers.Tag.PLAYER))
        {
            if (other.TryGetComponent(out PlayerController controller))
            {
                controller.OnLevelComplete();
                controller.MoveToPosition(transform.position);
                foreach (GameObject vfx in m_confettiVFX)
                {
                    vfx.SetActive(true);
                }
            }
            GameManager.Instance.OnLevelComplete();
        }
    }
}