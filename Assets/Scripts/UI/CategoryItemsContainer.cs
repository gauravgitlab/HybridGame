using UnityEngine;

public class CategoryItemsContainer : MonoBehaviour
{
    public Transform m_content;

    public void SetContainer(string cosmeticCategory)
    {
        name = $"{cosmeticCategory}_Items";
        gameObject.SetActive(false);
    }
}
