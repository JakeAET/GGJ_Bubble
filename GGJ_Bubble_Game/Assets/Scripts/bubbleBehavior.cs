using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubbleBehavior : MonoBehaviour
{
    [SerializeField] Animator animator;

    public AudioSource audioSource;
    public AudioClip sfx1;
    public AudioClip sfx2;
    public AudioClip sfx3;
    public AudioClip sfx4;

    public Vector2 rbDebug;

    private Rigidbody2D rb;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] SpriteRenderer spriteHighlight;

    public bool playerActive = false;
    public int playerTeam = 0; // 0 = null, 1 = p1, 2 = p2
    public player targetPlayer;

    public bool inOppEndzone = false;

    [SerializeField] float activeGravity;
    [SerializeField] float inactiveGravity;

    [SerializeField] float activeMass;
    [SerializeField] float inactiveMass;

    [SerializeField] PhysicsMaterial2D activeMaterial;
    [SerializeField] PhysicsMaterial2D inactiveMaterial;

    private bool continueVelocity = false;
    private Vector2 lingeringVelocity = Vector2.zero;
    static float interpX = 0f;
    static float interpY = 0f;
    [SerializeField] float decelerationAmount;

    [SerializeField] Sprite bubbleSprite;
    [SerializeField] Sprite boySprite;

    public bool jumping;
    public bool falling;
    public bool walking;
    public bool idle;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (playerActive)
        {
            turnBoy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        rbDebug = rb.velocity;

        // animation state changes
        if (playerActive)
        {
            //if(rb.velocity.y > 0f)
            //{
            //    isJumping();
            //}
            //else if(rb.velocity.y < 0f && (jumping || idle))
            //{
            //    isFalling();
            //}
            //else if ((rb.velocity.y > -1 && rb.velocity.y < 1) &&
            //    (rb.velocity.x < -1f && rb.velocity.x > 1f))
            //{
            //    isWalking();
            //}
            //else
            //{
            //    isIdle();
            //}

            if(rb.velocity.x >= 0)
            {
                sprite.flipX = false;
                spriteHighlight.flipX = false;
            }
            else if (rb.velocity.x <= 0)
            {
                sprite.flipX = true;
                spriteHighlight.flipX = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!playerActive)
        {
            string myGoal = "p" + (playerTeam + 1) + "Goal";

            if (collision.CompareTag("p1Goal"))
            {
                if (myGoal != "p1Goal") // scored in enemy goal +point
                {
                    gameManager.instance.goalScored(1);
                    bubbleManager.instance.destroyBubble(gameObject, playerTeam);
                }
                else // scored in your own goal
                {
                    bubbleManager.instance.destroyBubble(gameObject, playerTeam);
                    // do something to indicate you lost a boy?
                }
            }
            else if (collision.CompareTag("p2Goal"))
            {
                if (myGoal != "p2Goal") // scored in enemy goal +point
                {
                    gameManager.instance.goalScored(0);
                    bubbleManager.instance.destroyBubble(gameObject, playerTeam);
                }
                else // scored in your own goal
                {
                    bubbleManager.instance.destroyBubble(gameObject, playerTeam);
                    // do something to indicate you lost a boy?
                }
            }
            else if (collision.CompareTag("p1Endzone") && playerTeam == 1)
            {
                inOppEndzone = true;
            }
            else if (collision.CompareTag("p2Endzone") && playerTeam == 0)
            {
                inOppEndzone = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("p1Endzone") && playerTeam == 1)
        {
            inOppEndzone = false;
        }
        else if (collision.CompareTag("p2Endzone") && playerTeam == 0)
        {
            inOppEndzone = false;
        }
    }

    public void activateBubble(player p)
    {
        sprite.sprite = boySprite;
        continueVelocity = false;
        targetPlayer = p;
        playerActive = true;
        rb.gravityScale = activeGravity;
        rb.mass = activeMass;
        rb.sharedMaterial = activeMaterial;

        if(p.getPlayerIndex() == 0)
        {
            gameObject.layer = LayerMask.NameToLayer("P1Active");
        }

        if (p.getPlayerIndex() == 1)
        {
            gameObject.layer = LayerMask.NameToLayer("P2Active");
        }

        turnBoy();
    }

    public void deactivateBubble()
    {
        sprite.sprite = bubbleSprite;
        lingeringVelocity = rb.velocity;
        continueVelocity = true;
        targetPlayer = null;
        playerActive = false;
        rb.gravityScale = inactiveGravity;
        rb.mass = inactiveMass;
        rb.sharedMaterial = inactiveMaterial;
        gameObject.layer = LayerMask.NameToLayer("Default");
        rb.velocity = lingeringVelocity * 1.1f;

        turnBubble();
    }

    public void initializeBubble(int playerNum, Color color)
    {
        targetPlayer = null;
        playerActive = false;
        rb.gravityScale = inactiveGravity;
        rb.mass = inactiveMass;
        rb.sharedMaterial = inactiveMaterial;
        playerTeam = playerNum;
        sprite.color = color;
    }

    public void isWalking()
    {
        walking = true;
        animator.SetBool("walking", true);

        falling = false;
        animator.SetBool("falling", false);
        jumping = false;
        animator.SetBool("jumping", false);
        idle = false;
        animator.SetBool("idle", false);
    }

    public void isFalling()
    {
        falling = true;
        animator.SetBool("falling", true);

        walking = false;
        animator.SetBool("walking", false);
        jumping = false;
        animator.SetBool("jumping", false);
        idle = false;
        animator.SetBool("idle", false);
    }

    public void isJumping()
    {
        jumping = true;
        animator.SetBool("jumping", true);

        walking = false;
        animator.SetBool("walking", false);
        falling = false;
        animator.SetBool("falling", false);
        idle = false;
        animator.SetBool("idle", false);
    }

    public void isIdle()
    {
        idle = true;
        animator.SetBool("idle", true);

        walking = false;
        animator.SetBool("walking", false);
        falling = false;
        animator.SetBool("falling", false);
        jumping = false;
        animator.SetBool("jumping", false);
    }

    public void turnBubble()
    {
        animator.SetTrigger("bubbleMode");

        idle = false;
        animator.SetBool("idle", false);
        walking = false;
        animator.SetBool("walking", false);
        falling = false;
        animator.SetBool("falling", false);
        jumping = false;
        animator.SetBool("jumping", false);
    }

    public void turnBoy()
    {
        animator.SetTrigger("boyMode");

        idle = false;
        animator.SetBool("idle", false);
        walking = false;
        animator.SetBool("walking", false);
        falling = false;
        animator.SetBool("falling", false);
        jumping = false;
        animator.SetBool("jumping", false);
    }

}
