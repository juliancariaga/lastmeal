using UnityEngine;
using System.Collections;

public class EnemyFeedback : MonoBehaviour
{
    [Header("Scale feedback settings")]
    public float scaleAmount = 0.8f;    // how small to shrink 
    public float scaleDuration = 0.1f;  // how long the squish lasts before returning

    private Vector3 originalScale;
    private Coroutine scaleRoutine;

    void Awake()
    {
        originalScale = transform.localScale;
    }


    public void ShrinkOnHit()
    {
        if (scaleRoutine != null)
            StopCoroutine(scaleRoutine);

        scaleRoutine = StartCoroutine(ScaleFlash());
    }

    private IEnumerator ScaleFlash()
    {
        // scale down
        transform.localScale = originalScale * scaleAmount;
        yield return new WaitForSeconds(scaleDuration);
        // return to normal
        transform.localScale = originalScale;
        scaleRoutine = null;
    }
}
