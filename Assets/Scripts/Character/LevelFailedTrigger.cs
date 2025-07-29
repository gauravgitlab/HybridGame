using UnityEngine;

public class LevelFailedTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagsAndLayers.Tag.PLAYER))
        {
            GameManager.Instance.OnLevelFailed();
        }
    }
}
