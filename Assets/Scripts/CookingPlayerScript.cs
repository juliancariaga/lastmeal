using UnityEngine;
using UnityEngine.InputSystem;


public class CookingPlayerScript : MonoBehaviour
{

    public float moveSpeed = 6f;
    public float moveModifier = 4f;
    public float edgePadding = 0.1f;
    public Rigidbody2D rb;
    Vector2 movementInput;
    float halfWidth;
    float leftLimit, rightLimit;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        var sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            halfWidth = sr.bounds.extents.x;
        }
        else
        {
            var col = GetComponent<Collider2D>();
            if (col != null) halfWidth = col.bounds.extents.x;
            else halfWidth = 0.5f;

        }
        ComputeBounds();
    }

    void ComputeBounds()
    {
        var cam = Camera.main;
        // Visible world corners from camera
        Vector3 leftWorld = cam.ViewportToWorldPoint(new Vector3(0f, 0.5f, 0f));
        Vector3 rightWorld = cam.ViewportToWorldPoint(new Vector3(1f, 0.5f, 0f));
        leftLimit = leftWorld.x + halfWidth + edgePadding;
        rightLimit = rightWorld.x - halfWidth - edgePadding;
    }

    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }
    void Start()
    {


    }
    void Update()
    {

    }
    void FixedUpdate()
    {
        // Horizontal-only input
        Vector2 direction = new Vector2(movementInput.x, 0f);
        direction = Vector2.ClampMagnitude(direction, 1f);

        float currentSpeed;

        // If Shift is held, move faster
        if (Keyboard.current.leftShiftKey.isPressed)
        {
            currentSpeed = moveSpeed * moveModifier;
        }
        else
        {
            currentSpeed = moveSpeed;
        }

        Vector2 newPos = rb.position + direction * currentSpeed * Time.fixedDeltaTime;
        newPos.x = Mathf.Clamp(newPos.x, leftLimit, rightLimit);
        rb.MovePosition(newPos);
    }
}
