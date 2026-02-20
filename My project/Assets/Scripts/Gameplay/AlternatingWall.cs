using UnityEngine;

[RequireComponent(typeof(SpriteRenderer),typeof(BoxCollider2D))]
public class AlternatingWall : MonoBehaviour
{
    [SerializeField,Range(1f,10f)]private float alternationInterval = 5f;
    private Collider2D myCollider;
    private SpriteRenderer myRenderer;
    private float timer;

    void Start()
    {
        myCollider = GetComponent<Collider2D>();
        myRenderer = GetComponent<SpriteRenderer>();
        myCollider.isTrigger = false;
        myRenderer.enabled = true;
    }

    void Update()
    {
        
        if(timer >= alternationInterval)
        {
            myCollider.isTrigger = !myCollider.isTrigger;
            myRenderer.enabled = !myRenderer.enabled;
            timer = 0f;
        }

        timer += Time.deltaTime;
    }
}
