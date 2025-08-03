using UnityEngine;

[CreateAssetMenu(fileName = "CosmeticItem", menuName = "ScriptableObjects/Shop/CosmeticItemScriptable", order = 1)]
public class CosmeticItemScriptable : ScriptableObject
{
   public string m_cosmeticId;
   public string m_displayName;
   public string m_cosmeticCategory;
   public int m_cost;
   public Sprite m_icon;

   private void OnValidate()
   {
      m_cosmeticId = m_cosmeticId.Trim().ToLowerInvariant();
      m_cosmeticCategory = m_cosmeticCategory.Trim().ToLowerInvariant();
   }
}
