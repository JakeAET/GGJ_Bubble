using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class playerInputHandler : MonoBehaviour
{
    private player player;
    private PlayerInput playerInput;
    [SerializeField] bool controllerMode = false;

    private void Awake()
    {
        if (!controllerMode)
        {
            player = GetComponentInParent<player>();
            playerInput = GetComponent<PlayerInput>();
        }
        else
        {
            playerInput = GetComponent<PlayerInput>();
            var players = FindObjectsOfType<player>();
            var index = playerInput.playerIndex;

            player = players.FirstOrDefault(p => p.getPlayerIndex() == index);
            Debug.Log(player);
        }
        //Debug.Log(player);
        //var players = FindObjectsOfType<player>();
        //var index = playerInput.playerIndex;

        //player = players.FirstOrDefault(p => p.getPlayerIndex() == index);

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(CallbackContext context)
    {
        if(player != null && gameManager.instance.gameActive)
        {
            player.SetMovementVector(context.ReadValue<Vector2>());
        }
    }

    public void OnJump(CallbackContext context)
    {
        //if (player.isGrounded())
        //{
        //    player.jumpsRemaining = 2;
        //}

        if (gameManager.instance.gameActive)
        {
            if (context.performed && player != null && player.jumpsRemaining > 0)
            {
                player.jump();
            }

            if (context.canceled && player != null && player.rb.velocity.y > 0f)
            {
                player.endJump();
            }
        }

    }

    public void OnSplit(CallbackContext context)
    {
        if (context.performed && player != null &&player.splitTimer <= 0 && gameManager.instance.gameActive)
        {
            player.splitBubble();
        }
    }

    public void OnSwap(CallbackContext context)
    {
        if (context.performed && player != null && gameManager.instance.gameActive)
        {
            //Debug.Log("swap");
            player.swapBubble();
        }
    }
}
