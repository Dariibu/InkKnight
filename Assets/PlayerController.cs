using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb;

    [SerializeField] float velocity;
    [SerializeField] float jumpSpeed;

    [SerializeField] GameObject brc_01, brc_02, brc_03; //bottom
    [SerializeField] GameObject trc_01, trc_02, trc_03; //top
    [SerializeField] LayerMask floorMask;
    [SerializeField] float raycastLimit;

    float coyoteTime;
    [SerializeField] float limitCoyote;

    [SerializeField] bool alreadyJumped = false;

    Queue<KeyCode> inputBuffer;
    [SerializeField] float timeBuffering;

    [SerializeField] float distanceMoved;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inputBuffer = new Queue<KeyCode>();
    }

    private void Update()
    {
        rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * velocity, rb.velocity.y, 0);
 
        RaycastHit2D headRaycast_L = Physics2D.Raycast(trc_01.transform.position, Vector2.up, raycastLimit, floorMask);
        RaycastHit2D headRaycast = Physics2D.Raycast(trc_02.transform.position, Vector2.up, raycastLimit, floorMask);
        RaycastHit2D headRaycast_R = Physics2D.Raycast(trc_03.transform.position, Vector2.up, raycastLimit, floorMask);
        if (headRaycast_L && !headRaycast && !headRaycast_R)
        {
            transform.position += new Vector3(distanceMoved, 0);
        }
        else if (!headRaycast_L && !headRaycast && headRaycast_R)
        {
            transform.position -= new Vector3(distanceMoved, 0);
        }


        RaycastHit2D floorRaycastL = Physics2D.Raycast(brc_01.transform.position, Vector2.down, raycastLimit, floorMask);
        RaycastHit2D floorRaycast = Physics2D.Raycast(brc_02.transform.position, Vector2.down, raycastLimit, floorMask);
        RaycastHit2D floorRaycastR = Physics2D.Raycast(brc_03.transform.position, Vector2.down, raycastLimit, floorMask);
        if (floorRaycast|| floorRaycastL || floorRaycastR)
        {
            coyoteTime = 0;
            alreadyJumped = false;

            if (inputBuffer.Count > 0)
            {
                if (inputBuffer.Peek() == KeyCode.Space)
                {                   
                    rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                    alreadyJumped = true;
                }
            }
        }
        else
        {
            coyoteTime += Time.deltaTime;
            if (inputBuffer.Count > 0) 
            {
                if (inputBuffer.Peek() == KeyCode.Space)
                {
                    if (coyoteTime < limitCoyote && !alreadyJumped)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                        alreadyJumped = true;
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inputBuffer.Enqueue(KeyCode.Space);
            Invoke("removeQueue", timeBuffering);
        }
    }

    void removeQueue()
    {
        inputBuffer.Dequeue();
    }

}
