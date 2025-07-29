using System;
using UnityEngine;

public class Hover : MonoBehaviour
{
    private float m_startingY;

    [SerializeField] private AnimationCurve m_bounceCurve;
    [SerializeField] private float m_scalar = 1f;
    [SerializeField] private float m_rotationSpeed = 1f;

    private Vector3 m_localPos;
    private Quaternion m_localRot;
    
    
    private void Awake()
    {
        Config();
    }

    private void Config()
    {
        m_localPos = transform.localPosition;
        m_localRot = transform.localRotation;
    }

    private void Start()
    {
        SetStartingY();
    }

    void Update()
    {
        Vector3 position = transform.localPosition;
        position = new Vector3(position.x, m_startingY + m_bounceCurve.Evaluate(Time.time % m_bounceCurve.length) * m_scalar, position.z);
        transform.localPosition = position;
      
        transform.Rotate(0, m_rotationSpeed * Time.deltaTime, 0); 
    }

    public void SetStartingY()
    {
        m_startingY = transform.localPosition.y;
    }

    public void Reset()
    {
        transform.localPosition = m_localPos;
        transform.localRotation = m_localRot;
    }
}