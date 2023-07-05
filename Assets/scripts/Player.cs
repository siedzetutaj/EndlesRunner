using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    
    //Movement staff
    private readonly float groundMaxXVelocity = 20;
    private readonly float groundMaxAcceleration = 1.5f;
    private float groundAcceleration = 1;
    private Vector2 velocity;
    private Vector2 move;
    private readonly float speed = 8;
    private Rigidbody2D rb;
    private Vector2 pos;
    public float currDistance;
    //Staff To Jump
    public LayerMask groundLayerMask;
    [SerializeField]
    private float jetVelocity = 0.3f;
    private bool isGrounded = false;
    private float holdJetTimer = 1;
    [SerializeField]
    private float maxHoldJetTime = 1.5f;
    private float Gravity;
    public float jumpImpulse;
    //gameLogic Staff
    private int PlayerHealth=3;
    private GameLogic gameLogic;
    private bool BeingInvincible=false;
    private readonly float InvincibilityTimer=0.5f;
    private float InvincibilityTime=0;
    private bool GodMode;

    //Upgrades Staff
    public bool UpgradeFasterFuelReharge = false;
    public bool UpgradeHigherJump = false;
    public bool UpgradeTimeSlow = false;
    public bool UpgradeMaxhealth = false;
    public bool UpgradeGun = false;
    


    void Start()
    {
        
        Time.timeScale = 1;
        gameLogic =GameObject.Find("GameLogic").GetComponent<GameLogic>();
        gameLogic.MaxFuelAmount = maxHoldJetTime;
        rb = GetComponent<Rigidbody2D>();
        Gravity = rb.gravityScale;
        Application.targetFrameRate = 60;
        PassiveUpgradeManager();
    }
    private void Update()
    {
        gameLogic.health = PlayerHealth;
        move.x = Input.GetAxis("Horizontal");
        Jump();
        Fall();
        //do wywalenia potem
        ActiveUpgradeManager();
        Invincibility();
        if (Input.GetKeyDown(KeyCode.G)) GodMode = !GodMode;
    }
    private void FixedUpdate()
    {
        RaycastingToJump();
        MovementBounds();
        //movement
        rb.velocity = new Vector2(move.x * speed, rb.velocity.y);
    }
    public void GettingDamage()
    {//potem to wywal
        if (GodMode)
        {
            BeingInvincible = true;
        }
        if (!BeingInvincible)
        {
            PlayerHealth -= 1;
            groundAcceleration = 1f;
            velocity.x /= 2;
            BeingInvincible= true;
            InvincibilityTime = InvincibilityTimer;
        }
    }
    private void Invincibility()
    {
        if(InvincibilityTime > 0)
        {
            InvincibilityTime-=Time.deltaTime;
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.4f,0.7f,0.35f,0.5f)  ;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.4f, 0.7f, 0.35f, 1);
            BeingInvincible =false;
        }
        
    }
    //Jump and jet functionality
    private void Jump()
    {
        
        if ( Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.AddForce(Vector2.up * jumpImpulse, ForceMode2D.Impulse);
                isGrounded = false;
            }

            if (holdJetTimer > 0 && !isGrounded) 
            {
                rb.AddForce(Vector2.up * jetVelocity,ForceMode2D.Impulse);
                holdJetTimer -= Time.deltaTime;
            }
        }else if( Input.GetKeyUp(KeyCode.W)||Input.GetKeyUp(KeyCode.Space)) 
        {
            if (holdJetTimer < maxHoldJetTime)
                holdJetTimer += Time.deltaTime / 2;
            else holdJetTimer = maxHoldJetTime;
        }
        gameLogic.FuelAmount= holdJetTimer;
        //if ( Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.Space))
        //{
        //    isHoldingJump--;
        //    holdJumpTimer = maxHoldJumpTime;
        //}
    }
    private void Fall()
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

    private void RaycastingToJump()
    {
        currDistance += velocity.x * Time.fixedDeltaTime;
        gameLogic.GameLogicDistance = currDistance;
        pos = transform.position;
#pragma warning disable IDE0090
        Vector2 rayOrigin = new Vector2(pos.x - 0.1f, pos.y);
#pragma warning restore IDE0090
        Vector2 rayDirection = Vector2.down;
        float rayDistance = 0.7f;
        RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance, groundLayerMask);
        if (hit2D.collider != null)
        {
            isGrounded = true;
            if (maxHoldJetTime > holdJetTimer)
                holdJetTimer += Time.deltaTime * 1.2f;
            else holdJetTimer = maxHoldJetTime;
     
        }
        if (hit2D.collider==null) { 
        
            isGrounded=false;
        }
        Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);
    }
    //liczenie prêdkoœci z jak¹ poruszaj¹ sie pod³ogi
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
    //ograniczenie ruchu ¿eby postaæ nie wypad³a za mape
    private void MovementBounds()
    {
        if(this.gameObject.transform.position.x < -0.25f)
            this.gameObject.transform.position = new Vector2(-0.25f, transform.position.y);
        
        else if (this.gameObject.transform.position.x > 45.25f)
            this.gameObject.transform.position = new Vector2(45.25f, transform.position.y);
       
        if(this.gameObject.transform.position.y > 23.5f)
            this.gameObject.transform.position = new Vector2(transform.position.x,23.5f);


    }
    //¯eby dodaæ skilla
    //Dodaæ boola z skillem dodac funkcje dodac buttona w UpgradeButtons dodac w bazie danych 
    private void PassiveUpgradeManager()
    {
        if (UpgradeFasterFuelReharge)
        {

        }
        if (UpgradeHigherJump)
        {
            HigherJump();
        }
        if (UpgradeMaxhealth)
        {
            Maxhealth();
        }
        
        
    }
    //zamys³ jest taki ¿eby to co tam u góry jest dodawa³o potem do pliku staty :o
    //potem ta funkcja tu o tu bedzie zerowa³a boole ¿eby to sie nie nabija³o w nieskoñczonoœæ :-)
    //funkcja która resetuje boole typu Upgrade ¿eby umiejêtnoœci nie nabija³y siê w nieskoñczonoœæ
    private void PassiveUpgardeFunction()
    {

    }
    private void ActiveUpgradeManager()
    {
        if (UpgradeTimeSlow)
        {
            TimeSlow();
        }
        if (UpgradeGun)
        {
            Gun();
        }
    }
    private void FasterFuelReharge()
    {

    }
    private void HigherJump()
    {
        jetVelocity +=1 ;
    }
    private void Maxhealth()
    {
        PlayerHealth += 1;
    }
    private void TimeSlow()
    {

    }
    private void Gun()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {

        }
    }
}
