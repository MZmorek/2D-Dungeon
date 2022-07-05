using System.Collections;
using UnityEngine;

public class FadeAway : MonoBehaviour
{
    public SpriteRenderer squareRenderer;
    public float fadeRate;

    private Coroutine fadeCoroutine;
    private float transparency = 0;

    private void Start()
    {
        fadeCoroutine = StartCoroutine(Blackout());
    }

    private IEnumerator Blackout()
    {
        yield return new WaitForSeconds(3f);

        while (transparency < 1)
        {
            yield return null;
            transparency += fadeRate;
            squareRenderer.color = new Color(0f, 0f, 0f, transparency);
        }
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}