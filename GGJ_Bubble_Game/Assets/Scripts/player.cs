using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.InputSystem.InputAction;

public class player : MonoBehaviour
{
    [SerializeField] GameObject targetBubble;
    [SerializeField] bubbleBehavior targetBubbleRef;

    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float jumpForce = 3f;

    public Rigidbody2D rb;
    public bool canMove;

    private Vector2 moveDirection;
    //private Vector2 inputVector = Vector2.zero;
    
    [SerializeField] Vector2 boxSize;
    [SerializeField] float castDistance;
    [SerializeField] LayerMask groundLayer;

    [SerializeField] int playerIndex = 0;

    public int jumpsRemaining = 2;

    public float splitCooldown;
    public float splitTimer;

    private void Awake()
    {
        targetBubble = transform.parent.gameObject;
        targetBubbleRef = targetBubble.GetComponent<bubbleBehavior>();
        rb = GetComponentInParent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //targetBubble = transform.parent.gameObject;
        //targetBubbleRef = targetBubble.GetComponent<bubbleBehavior>();
        //rb = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //moveDirection = inputVector;
        //moveDirection = transform.TransformDirection(moveDirection);
        //moveDirection *= moveSpeed;

        //animation control
        if(isGrounded() && moveDirection.x != 0 && !targetBubbleRef.walking)
        {
            targetBubbleRef.isWalking();
        }
        else if(isGrounded() && moveDirection.x == 0 && moveDirection.y == 0 && !targetBubbleRef.idle)
        {
            targetBubbleRef.isIdle();
        }
        else if(!isGrounded() && rb.velocity.y > 1 && !targetBubbleRef.jumping)
        {
            targetBubbleRef.isJumping();
        }
        else if (!isGrounded() && rb.velocity.y < -3 && !targetBubbleRef.falling)
        {
            targetBubbleRef.isFalling();
        }


        if (isGrounded() && jumpsRemaining == 0)
        {
            //Debug.Log("jump reset");
            jumpsRemaining = 2;
        }

        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);

        if(splitTimer > 0)
        {
            splitTimer -= Time.deltaTime;
        }
    }

    public int getPlayerIndex()
    {
        return playerIndex;
    }

    public void SetMovementVector(Vector2 direction)
    {
        moveDirection = direction;
    }

    public void jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        //targetBubbleRef.isJumping();

        //rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        if (isGrounded())
        {
            jumpsRemaining = 1;
        }
        else
        {
            jumpsRemaining = 0;
        }
    }

    public void endJump()
    {
        targetBubbleRef.isFalling();
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.7f);
    }

    public bool isGrounded()
    {
        if(Physics2D.BoxCast(targetBubble.transform.position, boxSize, 0, -targetBubble.transform.up, castDistance, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        //if(targetBubble != null)
        //{
        //    Gizmos.DrawWireCube(targetBubble.transform.position - targetBubble.transform.up * castDistance, boxSize);
        //}
    }

    public void swapBubble()
    {
        bubbleBehavior oldBubbleRef = targetBubble.GetComponent<bubbleBehavior>();
        GameObject newBubble = bubbleManager.instance.pickNewBubble(playerIndex, targetBubble.GetComponent<bubbleBehavior>());

        if (newBubble != null)
        {
            targetBubbleRef.audioSource.PlayOneShot(targetBubbleRef.sfx3);

            targetBubble = newBubble;
            targetBubbleRef = targetBubble.GetComponent<bubbleBehavior>();
            targetBubbleRef.activateBubble(this);
            oldBubbleRef.deactivateBubble();
            transform.SetParent(targetBubble.transform);
            transform.position = targetBubble.transform.position;
            rb = GetComponentInParent<Rigidbody2D>();
        }
    }

    public void splitBubble()
    {
        targetBubbleRef.audioSource.PlayOneShot(targetBubbleRef.sfx4);

        splitTimer = splitCooldown;
        bubbleManager.instance.spawnBubble(playerIndex, rb.position);
    }

    public void disableMovement()
    {
        //targetBubbleRef.turnBubble();
        moveDirection = Vector2.zero;
        rb.velocity = Vector2.zero;
        canMove = false;
        //rb.Sleep();
    }

    public void enableMovement()
    {
        canMove = true;
        //rb.WakeUp();
    }

    public void turnBubble()
    {
        targetBubbleRef.turnBubble();
    }
}
