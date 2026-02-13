using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Vector2 touchStart;
    private Vector2 touchEnd;
    private Rigidbody2D rb;
    [SerializeField] private float maxDragDistance = 300f;
    [SerializeField] private float forceMultiplier;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        TouchControls.touchStarted += OnTouchStarted;
        TouchControls.touchEnded += OnTouchEnded;
    }

    private void OnTouchEnded(object sender, Vector2 e)
    {
        touchEnd = e;
        ShootBall();
    }

    private void ShootBall()
    {
        Vector2 dragVector = touchEnd - touchStart;
        float dragDistance = Mathf.Clamp(dragVector.magnitude, 0f, maxDragDistance);
        Vector2 normalizedDrag = dragVector.normalized;

        Vector3 force = new Vector3(normalizedDrag.x, 0 , normalizedDrag.y) * dragDistance * forceMultiplier;

        rb.AddForce(force, ForceMode2D.Impulse);
    }

    private void OnTouchStarted(object sender, Vector2 e)
    {
        touchStart = e;
    }

    void OnDestroy()
    {
        TouchControls.touchStarted -= OnTouchStarted;
        TouchControls.touchEnded -= OnTouchEnded;
    }
}
