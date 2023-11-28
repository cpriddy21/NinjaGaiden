fusing System.Collections;
using System.Collections.Generic;
using UnityEngine;
using 

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class CharacterController2D : MonoBehaviour
{
    // Move player in 2D space
    public float maxSpeed = 6.5f;
    public float jumpHeight = 9.2f;
    public float gravityScale = 2.0f;
    public Camera mainCamera;
    public Animator animator;
    //for ledgegrab
    public Transform ledgeGrabPoint;
    public float grabRange = 0.3f;
    public LayerMask platformLayer;

    bool facingRight = true;
    float moveDirection = 0;
    bool isGrounded = false;
    Vector3 cameraPos;
    Rigidbody2D r2d;
    CapsuleCollider2D mainCollider;
    Transform t;

    //knockback and damage
    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;
    public bool KBRight;
    public int playerHealth = 20;
    public int lives = 3;

    //connecting to UI
    public gameObject health;
    TextMeshProUGUI healthReadout;
    public gameObject lives;
    TextMeshProUGUI livesReadout;

    // Use this for initialization
    void Start()
    {
        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<CapsuleCollider2D>();
        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
        facingRight = t.localScale.x > 0;
        if (mainCamera)
        {
            cameraPos = mainCamera.transform.position;
        }

        
        healthReadout = score.GetComponent<TextMeshProUGUI>();
        updateHealth(0);
        livesReadout = score..GetComponent<TextMeshProUGUI>();
        updateLives();

    }

    // Update is called once per frame
    void Update()
    {
        if(KBCounter < 0)
        {
            KBCounter = 0;
        }
        /*if(KBCounter != 0) 
        {
            gameObject.GetComponent<CapsuleCollider2D>().isTrigger = false;
        }
        else
        {
            gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
        }*/
        // Movement controls
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && (isGrounded || Mathf.Abs(r2d.velocity.x) > 0.01f))
        {
            moveDirection = Input.GetKey(KeyCode.A) ? -1 : 1;
        }
        else
        {
            if (isGrounded || r2d.velocity.magnitude < 0.01f)
            {
                moveDirection = 0;
            }
        }
        animator.SetFloat("Speed", Mathf.Abs(moveDirection));
        // Change facing direction
        if (moveDirection != 0)
        {
            if (moveDirection > 0 && !facingRight)
            {
                facingRight = true;
                t.localScale = new Vector3(Mathf.Abs(t.localScale.x), t.localScale.y, transform.localScale.z);
            }
            if (moveDirection < 0 && facingRight)
            {
                facingRight = false;
                t.localScale = new Vector3(-Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
            }
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
            animator.SetTrigger("Jump");
        }

        // Camera follow
        if (mainCamera)
        {
            //mainCamera.transform.position = new Vector3(t.position.x, cameraPos.y, cameraPos.z);
            mainCamera.transform.position = new Vector3(t.position.x, (t.position.y+2), cameraPos.z);
        }
        animator.SetBool("Jumping", !isGrounded);
    }

    void FixedUpdate()
    {
        Bounds colliderBounds = mainCollider.bounds;
        float colliderRadius = mainCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
        // Check if player is grounded
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);
        //Check if any of the overlapping colliders are not player collider, if so, set isGrounded to true
        isGrounded = false;
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != mainCollider)
                {
                    isGrounded = true;
                    break;
                }
            }
        }

        // Apply movement velocity and crouching
        if (KBCounter <= 0)
        {
            if (!Input.GetKey(KeyCode.S) && KBCounter == 0)
            {
                r2d.velocity = new Vector2((moveDirection) * maxSpeed, r2d.velocity.y);
                animator.SetBool("isCrouching", false);
                animator.SetBool("isCrouchWalking", false);
            }
            else
            {
                if (KBCounter == 0)
                {


                    r2d.velocity = new Vector2((moveDirection) * (maxSpeed / 2), r2d.velocity.y);
                    if (moveDirection != 0)
                    {
                        animator.SetBool("isCrouching", false);
                        animator.SetBool("isCrouchWalking", true);
                    }
                    else
                    {
                        animator.SetBool("isCrouching", true);
                        animator.SetBool("isCrouchWalking", false);
                    }
                }
            }
        }
        else
        {
            if(KBRight == true)
            {
                r2d.velocity = new Vector2(KBForce, KBForce/2);
            }
            else
            {
                r2d.velocity = new Vector2(-KBForce, KBForce/2);
            }
            if(isGrounded == true)
            {
                KBCounter = 0;
            }
            else
            {
                KBCounter -= Time.deltaTime; 
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && KBCounter == 0)
        {
            r2d.velocity = new Vector2(0, 0);
            animator.SetTrigger("damage");
            Debug.Log("Ryu has been hit");
            KBCounter = KBTotalTime;
            if (transform.position.x >= collision.transform.position.x) {
            Debug.Log("kb right");
            KBRight = true;
            }
            if (transform.position.x <= collision.transform.position.x)
            {
            KBRight = false;
            Debug.Log("kb not right");
            }
            playerHealth--;
            if (playerHealth == 0)
            {
                //add here later
            }
        }
    }

    public void updateHealth(int damage) {
        playerHealth = playerHealth - damage;
        
    }

    public void updateLives() {
        lives = lives - 1;
        livesReadout.text = lives.ToString();
    }
}
