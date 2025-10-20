using UnityEngine;

public class DogScript : MonoBehaviour
{
    [Header("Chase Settings")]
    public float maxSpeed = 3f;
    public float stoppingDistance = 0.05f;
    [Header("Target")]
    public Transform player;
    private Rigidbody2D rb;
    private float _findCooldown = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        TryFindPlayer();
    }

    void FixedUpdate()
    {
        if (player == null)
        {
            _findCooldown -= Time.fixedDeltaTime;
            if (_findCooldown <= 0f) TryFindPlayer();
            return;
        }

        Vector2 pos = rb.position;
        Vector2 targetPos = player.position;
        Vector2 toPlayer = targetPos - pos;

        Vector2 direction = toPlayer.sqrMagnitude > stoppingDistance * stoppingDistance
            ? toPlayer.normalized
            : Vector2.zero;

        Vector2 velocity = direction * maxSpeed;

        rb.MovePosition(pos + velocity * Time.fixedDeltaTime);

        // --- sprit flip ---
        // if (direction.x != 0f)
        // {
        //     var sr = GetComponentInChildren<SpriteRenderer>();
        //     if (sr) sr.flipX = direction.x < 0f ? false : true;
        // }
    }

    private void TryFindPlayer()
    {
        if (player != null) return;

        GameObject found = GameObject.FindGameObjectWithTag("Player");
        if (found != null) player = found.transform;

        _findCooldown = 0.5f;
    }
}
