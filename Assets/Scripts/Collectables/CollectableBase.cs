using UnityEngine;

public class CollectableBase : Droppable
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController controller))
        {
            OnCollected(controller.transform);
        }
    }

    protected virtual void OnCollected(Transform player)
    {
    }
}
