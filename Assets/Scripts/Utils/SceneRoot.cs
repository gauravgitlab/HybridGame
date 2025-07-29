using System;
using UnityEngine;

public class SceneRoot : MonoBehaviour
{
    private static Transform s_root;
    
    public static Transform Root
    {
        get => s_root;
    }
    
    private void Awake()
    {
        s_root = transform;
    }

    private void OnDestroy()
    {
        s_root = null;
    }
}
