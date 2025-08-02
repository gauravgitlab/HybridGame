using UnityEngine;

[CreateAssetMenu(fileName = "EnemyType", menuName = "ScriptableObjects/Enemy/EnemyTypeScriptable", order = 1)]
public class EnemyTypeScriptable : ScriptableObject
{
    public float m_health = 2.0f;
    public Vector3 m_scale = Vector3.one;
}
