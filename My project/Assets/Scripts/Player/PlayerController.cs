using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class PlayerController : MonoBehaviour
{
    public static EventHandler PlayerTried;
    private Vector2 touchStart;
    private Vector2 touchEnd;
    private Rigidbody2D rb;
    [SerializeField] private float maxDragDistance = 300f;
    [SerializeField] private float forceMultiplier;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTouchEnded(object sender, Vector2 e)
    {
        touchEnd = e;
        ShootBall();
        PlayerTried?.Invoke(this, EventArgs.Empty);
    }

    private void ShootBall()
    {
        Vector2 dragVector = touchEnd - touchStart;
        float dragDistance = Mathf.Clamp(dragVector.magnitude, 0f, maxDragDistance);
        Vector2 normalizedDrag = dragVector.normalized;

        Vector2 force = new Vector2(-normalizedDrag.x, -normalizedDrag.y) * dragDistance * forceMultiplier;

        rb.AddForce(force, ForceMode2D.Force);
    }

    private void OnTouchStarted(object sender, Vector2 e)
    {
        touchStart = e;
    }

    public void EnableInput()
    {
        TouchControls.touchStarted += OnTouchStarted;
        TouchControls.touchEnded += OnTouchEnded;
    }

    public void DisableInput()
    {
        TouchControls.touchStarted -= OnTouchStarted;
        TouchControls.touchEnded -= OnTouchEnded;
    }

    void OnDestroy()
    {
        DisableInput();
    }
}
