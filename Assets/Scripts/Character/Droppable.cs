using System;
using System.Collections;
using UnityEngine;

public class Droppable : MonoBehaviour
{
   [SerializeField] protected LayerMask m_floorLayermask;
   [SerializeField] protected Vector3 m_floorOffset;
   [SerializeField] protected Hover m_hover;
   [SerializeField] protected SphereCollider m_sphereCollider;

   protected Vector3 m_dropPosition;
   
   private bool m_isCollectable = true;

   public virtual void HandleDrop(Vector3 dropPostion)
   {
      m_hover.enabled = true;
      m_sphereCollider.enabled = true;
      m_dropPosition = dropPostion;
      RaycastHit hit;
      if (Physics.Raycast(dropPostion, -transform.up, out hit, Mathf.Infinity, m_floorLayermask))
      {
         m_dropPosition = hit.point + m_floorOffset;
      }
      transform.position = m_dropPosition;
      m_hover.SetStartingY();
   }

   public virtual void HandleCollected()
   {
      m_hover.enabled = false;
      m_sphereCollider.enabled = false;
      m_hover.Reset();
   }
}