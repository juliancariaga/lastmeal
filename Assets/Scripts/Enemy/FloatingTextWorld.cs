using UnityEngine;
using TMPro;

public class FloatingTextWorld : MonoBehaviour
{
    public float lifetime = 1f;          // how long before destroying
    public float floatSpeed = 1f;        // move upward speed
    public float fadeSpeed = 2f;         // fade strength

    private TMP_Text text;
    private Color originalColor;
    private float timer = 0f;

    void Awake()
    {
        text = GetComponent<TMP_Text>();
        originalColor = text.color;
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Move upward
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;

        // Fade out
        float alpha = Mathf.Lerp(1f, 0f, timer / lifetime);
        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

        // Destroy after lifetime
        if (timer >= lifetime)
            Destroy(gameObject);
    }
}
