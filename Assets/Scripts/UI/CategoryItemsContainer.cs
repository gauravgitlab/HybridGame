using UnityEngine;

public class CategoryItemsContainer : MonoBehaviour
{
    public Transform m_content;

    public void SetContainer(CosmeticCategory category)
    {
        name = $"{category}_Items";
        gameObject.SetActive(false);
    }
}
