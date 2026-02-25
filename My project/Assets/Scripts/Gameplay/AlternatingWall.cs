using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
public class AlternatingWall : MonoBehaviour
{
    [SerializeField, Range(1f, 10f)] private float alternationInterval = 5f;
    private float fadeSpeed = 2f;

    private Collider2D myCollider;
    private SpriteRenderer myRenderer;

    private float timer;
    private bool isVisible = true;

    void Start()
    {
        myCollider = GetComponent<Collider2D>();
        myRenderer = GetComponent<SpriteRenderer>();
        fadeSpeed = alternationInterval * 2;
        myCollider.isTrigger = false;

        SetAlpha(1f);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= alternationInterval)
        {
            timer = 0f;

            myCollider.isTrigger = !myCollider.isTrigger;

            isVisible = !isVisible;
        }

        float targetAlpha = isVisible ? 1f : 0f;
        Color currentColor = myRenderer.color;
        float currentAlpha = Mathf.Lerp(currentColor.a, targetAlpha, fadeSpeed * Time.deltaTime);

        SetAlpha(currentAlpha);
    }

    void SetAlpha(float alpha)
    {
        Color color = myRenderer.color;
        color.a = alpha;
        myRenderer.color = color;
    }
}