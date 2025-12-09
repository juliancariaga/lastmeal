using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour
{
    [Header("Timing")]
    [Tooltip("How long the weapon is active (hitbox on) per click.")]
    public float activeDuration = 0.15f;

    [Tooltip("Delay before you can swing again.")]
    public float cooldown = 0.25f;

    [Header("References (optional)")]
    public SpriteRenderer spriteRenderer;      // assign if you want visuals to hide/show
    public Behaviour[] extraEnableWhileActive; // e.g., DamageOnContact

    private Collider2D col;
    private bool canSwing = true;

    void Awake()
    {
        col = GetComponent<Collider2D>();
        col.isTrigger = true; // important for DamageOnContact

        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();

        SetActiveState(false);
    }

    void Update()
    {
        // Basic input (left mouse)
        if (canSwing && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(SwingOnce());
        }

        // get mouse world position 
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0f;

        // calculate direction from player to mouse
        Vector2 dir = (mouse - transform.position).normalized;

        // rotate weapon to face that direction
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float rotationOffset = -90f; // 90f instead if weird
        transform.rotation = Quaternion.Euler(0f, 0f, angle + rotationOffset);
    }

    private IEnumerator SwingOnce()
    {
        canSwing = false;

        // turn on visuals + hitbox + damage script(s)
        SetActiveState(true);
        yield return new WaitForSeconds(activeDuration);

        // turn everything off
        SetActiveState(false);
        yield return new WaitForSeconds(cooldown);

        canSwing = true;
    }

    private void SetActiveState(bool isActive)
    {
        // hitbox
        if (col) col.enabled = isActive;

        // sprite 
        if (spriteRenderer) spriteRenderer.enabled = isActive;

        // any extra behaviours to enable only while swinging (e.g., DamageOnContact)
        if (extraEnableWhileActive != null)
        {
            foreach (var b in extraEnableWhileActive)
                if (b) b.enabled = isActive;
        }
    }

    private void OnDisable()
    {
        // 🔧 Fix: switching away mid-swing shouldn’t perma-lock the weapon
        StopAllCoroutines();
        SetActiveState(false);
        canSwing = true;
    }
}
