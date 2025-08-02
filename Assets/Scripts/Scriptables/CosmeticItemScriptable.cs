using UnityEngine;

public enum CosmeticCategory
{
   Hats, 
   Beards
}

[CreateAssetMenu(fileName = "CosmeticItem", menuName = "ScriptableObjects/Shop/CosmeticItemScriptable", order = 1)]
public class CosmeticItemScriptable : ScriptableObject
{
   public string m_id;
   public string m_displayName;
   public CosmeticCategory m_cosmeticCategory;
   public int m_cost;
   public Sprite m_icon;

   private void OnValidate()
   {
      m_id = m_id.Trim().ToLowerInvariant();
   }
}
