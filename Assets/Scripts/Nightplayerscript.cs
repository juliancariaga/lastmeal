using UnityEngine;
using UnityEngine.InputSystem;


public class Nightplayerscript : MonoBehaviour
{

    public float moveSpeed = 3f;
    public float moveModifier = 2.5f;
    public Rigidbody2D rb;
    Vector2 movementInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
    private void FixedUpdate()
    {
        Vector2 direction = Vector2.ClampMagnitude(movementInput, 1f);

        if (Keyboard.current.leftShiftKey.isPressed)
        {
            rb.MovePosition(rb.position + direction * (moveSpeed * moveModifier) * Time.fixedDeltaTime);
        }
        else
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
