using UnityEngine;
using System.Collections;

public class PopupAnimator : MonoBehaviour
{
    [SerializeField] private float m_duration = 0.2f;

    private void Start()
    {
        StartCoroutine(AnimatePopup());
    }

    private IEnumerator AnimatePopup()
    {
        Vector3 start = Vector3.zero;
        Vector3 end = Vector3.one;
        float time = 0;

        while (time < m_duration)
        {
            time += Time.deltaTime;
            float t = time / m_duration;
            transform.localScale = Vector3.LerpUnclamped(start, end, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }

        transform.localScale = end;
    }
}
