using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpinTrap : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_vTorque = Vector3.zero;

    private Rigidbody m_rb = null;


    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_rb.maxAngularVelocity = 50f;
    }

    private void FixedUpdate()
    {
        m_rb.AddTorque(m_vTorque);
    }
}
