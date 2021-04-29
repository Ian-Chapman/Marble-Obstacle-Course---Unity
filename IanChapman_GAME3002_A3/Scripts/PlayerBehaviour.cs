using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    Rigidbody body;

    public float m_fSpeed;

    public static bool isInAir = false;
    bool is2ndJump = false;
    bool onPlat = false;

    [SerializeField]
    private float m_fMinVel = 5f;
    
    private Vector3 m_vPrevVel;
    private Vector3 m_vDirection;


    public static int key = 0;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {

        keyboardInput();

        float velocity = Mathf.Abs(body.velocity.x);
        m_vPrevVel = body.velocity;
    }

    void keyboardInput()
    {

        if ((Input.GetKeyDown(KeyCode.Space)) && (is2ndJump == false))
        {
            if (isInAir == false)
            {
                isInAir = true;
                body.velocity = new Vector3(0f, 5.5f, 0f);
            }
            else
            {
                is2ndJump = true; //double jump
                body.velocity = new Vector3(0f, 3.5f, 0f);
            }
        }

        if ((Input.GetKey(KeyCode.A)) && (onPlat == false))
        {
            body.velocity = new Vector3(-3.2f, body.velocity.y, body.velocity.z);
        }

        if ((Input.GetKey(KeyCode.D)) && (onPlat == false))
        {
            body.velocity = new Vector3(3.2f, body.velocity.y, body.velocity.z);
        }

    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Ground")
        {
            isInAir = false;
            is2ndJump = false;

        }


        if ((other.gameObject.tag == "Spikes") || (other.gameObject.tag == "SpinTrap") || (other.gameObject.tag == "Kill Floor1")) // respawn checkpoint
        {
            gameObject.transform.position = new Vector3(0, 0.162f, 0);
            body.velocity = new Vector3(0, 0, 0); //reset velocity
        }

        if ((other.gameObject.tag == "Spikes2") || (other.gameObject.tag == "SpinTrap2") || (other.gameObject.tag == "Kill Floor2"))// respawn checkpoint
        {
            gameObject.transform.position = new Vector3(10f, -3.1404f, 0);
            body.velocity = new Vector3(0, 0, 0);//reset velocity
        }

        if ((other.gameObject.tag == "Spikes3") || (other.gameObject.tag == "SpinTrap3") || 
            (other.gameObject.tag == "Kill Floor3") || (other.gameObject.tag == "Deathball")) // respawn checkpoint
        {
            gameObject.transform.position = new Vector3(19.6411f, -9.145f, 0);
            body.velocity = new Vector3(0, 0, 0);//reset velocity
        }

        if (other.gameObject.tag == "BounceWall")
        {
            Bounce(other.contacts[0].normal);
        }

        if (other.gameObject.tag == "Door1")
        {
            if (other.gameObject.GetComponent<DoorBehaviour >().locky == key)
            other.gameObject.GetComponent<DoorBehaviour>().isDoorOpen = true;
        }

        if (other.gameObject.tag == "Ice Key")
        {
            key++;
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Speed Up Zone")
        {
            body.velocity = new Vector3(8.0f, body.velocity.y, body.velocity.z);
            onPlat = true;
        }

        if (other.tag == "Slow Down Zone")
        {
            if (Input.GetKey(KeyCode.A))
            {
                body.velocity = new Vector3(-0.4f, body.velocity.y, body.velocity.z);
            }

            if (Input.GetKey(KeyCode.D))
            {
                body.velocity = new Vector3(0.4f, body.velocity.y, body.velocity.z);
            }
            onPlat = true;
            isInAir = true;
            is2ndJump = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Speed Up Zone")
        {
            onPlat = false;
        }

        if (other.tag == "Slow Down Zone")
        {
            onPlat = false;
            isInAir = false;
            is2ndJump = false;
        }
    }


    private void Bounce(Vector3 collisionNormal) // For extra bounce effect in the last 'hallway' of the level
    {
        m_fSpeed = m_vPrevVel.magnitude;
        m_vDirection = Vector3.Reflect(m_vPrevVel.normalized, collisionNormal);

        body.velocity = m_vDirection * Mathf.Max(m_fSpeed, m_fMinVel);
    }

}
