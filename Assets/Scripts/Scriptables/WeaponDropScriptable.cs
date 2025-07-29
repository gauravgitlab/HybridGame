using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WeaponDropScriptable", order = 1)]
public class WeaponDropScriptable : ScriptableObject
{
    [System.Serializable]
    public class DropAndRate
    {
        public Droppable m_droppableGameObject;
        [Range(0, 1f)] public float m_chance;
        public float m_dropSpread;
        public int m_amount;
    }

    public DropAndRate[] m_droppableGameObject;

    private List<DropAndRate> GetDropSpawnables()
    {
        List<DropAndRate> droppables = new List<DropAndRate>();
        for (int i = 0; i < m_droppableGameObject.Length; i++)
        {
            if (m_droppableGameObject[i].m_chance >= Random.value)
            {
                for (int j = 0; j < m_droppableGameObject[i].m_amount; j++)
                {
                    droppables.Add(m_droppableGameObject[i]);
                }
            }
        }
        return droppables;
    }

    public void Spawn(Vector3 dropPos)
    {
        List<DropAndRate> dropAndRates = GetDropSpawnables();
        foreach (DropAndRate dropSpawnable in dropAndRates)
        {
            Droppable drop = Instantiate(dropSpawnable.m_droppableGameObject, SceneRoot.Root);
            Vector2 unitCircle = Random.insideUnitCircle.normalized * dropSpawnable.m_dropSpread;
            drop.HandleDrop(dropPos + new Vector3(unitCircle.x, 0, unitCircle.y));
        }
    }
}