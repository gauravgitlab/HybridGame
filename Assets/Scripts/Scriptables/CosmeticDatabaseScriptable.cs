using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CosmeticDatabase", menuName = "ScriptableObjects/Shop/Cosmetic Database")]
public class CosmeticDatabaseScriptable : ScriptableObject
{
    public List<CosmeticItemScriptable> m_cosmeticItems;
}