using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    [SerializeField]
    private float m_fSpringConst = 0f;
    [SerializeField]
    private float m_fOrigPos = 0f;
    [SerializeField]
    private float m_fPressedPos = 0f;
    [SerializeField]
    private float m_fSpringDamp = 0f;
    
    [SerializeField]
    private KeyCode m_DoorInput;

    private HingeJoint m_hingeJoint = null;
    private JointSpring m_jointSpring;

    public int locky;
    public bool isDoorOpen = false;



    private void Start()
    {
        m_hingeJoint = GetComponent<HingeJoint>();
        m_hingeJoint.useSpring = true;

        m_jointSpring = new JointSpring();
        m_jointSpring.spring = m_fSpringConst;
        m_jointSpring.damper = m_fSpringDamp;

        m_hingeJoint.spring = m_jointSpring;
    }

    private void OpenDoor()
    {
        m_jointSpring.targetPosition = m_fPressedPos;
        m_hingeJoint.spring = m_jointSpring;
    }


    private void Update()
    {
        if (isDoorOpen == true)
        {
            OpenDoor();
        }
    }
}