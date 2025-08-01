using System;
using UnityEngine;

public class ShopEvents : MonoBehaviour
{
    public static event Action<CosmeticCategory, string> CosmeticPurchased;
    public static void SendCosmeticPurchased(CosmeticCategory category, string cosmeticId)
    {
        CosmeticPurchased?.Invoke(category, cosmeticId);
    }

    public static event Action<CosmeticCategory, string, string> CosmeticEquipped;
    public static void SendCosmeticEquipped(CosmeticCategory cosmeticCategory, string lastEquippedCosmeticId, string cosmeticId)
    {
        CosmeticEquipped?.Invoke(cosmeticCategory, lastEquippedCosmeticId, cosmeticId);
    }
}
