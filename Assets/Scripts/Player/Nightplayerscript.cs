using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class Nightplayerscript : MonoBehaviour
{

    // === Movement variables ===
    public float moveSpeed = 3f;
    public float moveModifier = 2.5f;

    // === Dash variables ===
    public float dashDistance = 5f;
    public float dashCooldown = 5f;
    private float nextDashReadyTime = 0f;

    // === Components and movement ===
    public Rigidbody2D rb;
    private Vector2 movementInput;
    private Vector2 lastMoveDir = Vector2.right;
    Boolean facingRight;

    // === Healh and stats ===
    public int hp = 0;

    private bool dashRequested = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();

        // remember last non-zero direction for stand-still dashes
        if (movementInput.sqrMagnitude > 0.0001f)
        {
            lastMoveDir = movementInput.normalized;
        }
    }

    void Update()
    {

        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (Time.time >= nextDashReadyTime)
            {
                dashRequested = true;
                nextDashReadyTime = Time.time + dashCooldown;
                Debug.Log("Dash requested");
            }
            else
            {

            }
        }

        // Get horizontal input (A/D or Left/Right)
        movementInput.x = Input.GetAxisRaw("Horizontal");

        // Move
        //rb.velocity = new Vector2(movementInput.x * moveSpeed, rb.velocity.y);

        // Flip the sprite if moving left/right
        if (movementInput.x < 0 && !facingRight)
        {
            Flip();
        }
        else if (movementInput.x > 0 && facingRight)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        // Compute clamped direction for normal/sprint movement
        Vector2 direction = movementInput;
        if (direction.sqrMagnitude > 1f)
            direction = direction.normalized;

        // Handle dash first so it “wins” this physics step
        if (dashRequested)
        {
            Vector2 dashDir = direction.sqrMagnitude > 0.0001f ? direction : lastMoveDir;

            // Instant dash: do NOT multiply by Time.fixedDeltaTime
            rb.MovePosition(rb.position + dashDir * dashDistance);

            dashRequested = false; // consume request
            Debug.Log("Dashed!");
            return; // skip normal movement this tick
        }

        // Sprint or normal move
        float speed = moveSpeed;
        if (Keyboard.current != null && Keyboard.current.leftShiftKey.isPressed)
        {
            speed = moveSpeed * moveModifier;
        }

        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }
    void Flip()
    {
        facingRight = !facingRight;

        // Multiply the X scale by -1 to flip
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
