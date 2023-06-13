using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float groundMaxXVelocity = 20;
    private float groundMaxAcceleration = 1.5f;
    private float groundAcceleration = 1;
    private GameLogic gameLogic;

    private Vector2 velocity;
    private Vector2 move;
    public float speed = 5;
    private Rigidbody2D rb;
    private Vector2 pos;

    public float jumpVelocity = 30;
    public LayerMask groundLayerMask;
    public bool isGrounded = false;
    public bool isHoldingJump = false;
    public float holdJumpTimer = 0;
    public float maxHoldJumpTime = 0.2f;
    public float Gravity;
    void Start()
    {
        gameLogic =GameObject.Find("GameLogic").GetComponent<GameLogic>();
        rb = GetComponent<Rigidbody2D>();
        Gravity = rb.gravityScale;
        Application.targetFrameRate = 60;
    }
    private void Update()
    {
        move.x = Input.GetAxis("Horizontal");
        Jump();
        fall();
    }
    private void FixedUpdate()
    {
        RaycastingToJump();
        
        //movement
        rb.velocity = new Vector2(move.x * speed, rb.velocity.y);
    }

    void Jump()
    {
        
        if ( Input.GetKey(KeyCode.W))
        {
            if (holdJumpTimer > 0 && isHoldingJump) 
            {
                rb.velocity = Vector2.up * jumpVelocity;
                holdJumpTimer -= Time.deltaTime;
            }
            else
            {
                isHoldingJump = false;
            }
        }
        if ( Input.GetKeyUp(KeyCode.W))
        {
            isHoldingJump = false;
        }
    }
    void fall()
    {
        if (!isGrounded && Input.GetKey(KeyCode.S)) 
        {
                rb.gravityScale=4;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            rb.gravityScale = Gravity;
        }
    }

    void RaycastingToJump()
    {
        gameLogic.distance += velocity.x * Time.fixedDeltaTime;
        pos = transform.position;
        Vector2 rayOrigin = new Vector2(pos.x-0.1f, pos.y);
        Vector2 rayDirection = Vector2.down;
        float rayDistance = 0.7f;
        RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance, groundLayerMask);
        if (hit2D.collider != null)
        {
            isGrounded = true;
       
                    isHoldingJump = true;
                    holdJumpTimer = maxHoldJumpTime;
  
     
        }
        if (hit2D.collider==null) { 
        
            isGrounded=false;
        }
        Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);
    }
    public Vector2 GroundMovement(Vector2 groundVelocity)
    {
        float velocityRatio = groundVelocity.x / groundMaxXVelocity;
        groundAcceleration = groundMaxAcceleration * (1 - velocityRatio) * 0.3f;
        velocity.x += groundAcceleration * Time.fixedDeltaTime;
        if (velocity.x >= groundMaxXVelocity)
        {
            velocity.x = groundMaxXVelocity;
        }
        return velocity;
    }
}
