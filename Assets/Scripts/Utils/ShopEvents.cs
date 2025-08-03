using System;
using UnityEngine;

public class ShopEvents : MonoBehaviour
{
    public static event Action<string, string> CosmeticPurchased;
    public static void SendCosmeticPurchased(string cosmeticCategory, string cosmeticId)
    {
        CosmeticPurchased?.Invoke(cosmeticCategory, cosmeticId);
    }

    public static event Action<string, string, string> CosmeticEquipped;
    public static void SendCosmeticEquipped(string cosmeticCategory, string lastEquippedCosmeticId, string cosmeticId)
    {
        CosmeticEquipped?.Invoke(cosmeticCategory, lastEquippedCosmeticId, cosmeticId);
    }
}