using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform target;
    public float lagSpeed = .125f;
    public Vector3 camOffset;


    void FixedUpdate()
    {
        Vector3 newPos = target.position + camOffset;
        Vector3 lagPos = Vector3.Lerp(transform.position, newPos, lagSpeed);
        transform.position = lagPos;
    }

}
