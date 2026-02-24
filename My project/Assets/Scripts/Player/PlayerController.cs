using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class PlayerController : MonoBehaviour
{
    public static Action PlayerTried;
    private Vector2 touchStart;
    private Vector2 touchEnd;
    public Rigidbody2D Rigidbody {get; private set;}
    public bool Potted {get; set;}
    [field: SerializeField] public float MaxDragDistance = 300f;
    [SerializeField] private float forceMultiplier;
    private bool tryAttempt;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnTouchEnded(object sender, Vector2 e)
    {
        if (tryAttempt) { return; }
        touchEnd = e;
        ShootBall();
    }

    void FixedUpdate()
    {
        if (!tryAttempt || Potted) { return; }

        if (Rigidbody.IsSleeping() || (Rigidbody.linearVelocity.magnitude < 0.12f && Rigidbody.linearVelocity.magnitude != 0f))
        {
            tryAttempt = false;
            PlayerTried?.Invoke();
        }
    }

    private void ShootBall()
    {
        Vector2 dragVector = touchEnd - touchStart;
        float dragDistance = Mathf.Clamp(dragVector.magnitude, 0f, MaxDragDistance);
        Vector2 normalizedDrag = dragVector.normalized;

        Vector2 force = new Vector2(-normalizedDrag.x, -normalizedDrag.y) * dragDistance * forceMultiplier;

        Rigidbody.AddForce(force, ForceMode2D.Force);

        tryAttempt = true;
    }

    private void OnTouchStarted(object sender, Vector2 e)
    {
        if (tryAttempt) { return; }
        touchStart = e;
    }

    public void EnableInput()
    {
        TouchControls.TouchStarted += OnTouchStarted;
        TouchControls.TouchEnded += OnTouchEnded;
    }

    public void DisableInput()
    {
        TouchControls.TouchStarted -= OnTouchStarted;
        TouchControls.TouchEnded -= OnTouchEnded;
    }

    void OnDisable()
    {
        tryAttempt = false;
    }

    void OnDestroy()
    {
        DisableInput();
    }
}
