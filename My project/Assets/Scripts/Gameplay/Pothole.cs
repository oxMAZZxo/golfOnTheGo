using System;
using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D),typeof(Animator))]
public class Pothole : MonoBehaviour
{
    [field: SerializeField, Range(1, 10)] public int Points { get; private set; }
    [SerializeField] private TMP_Text pointsLabel;
    [SerializeField] private Transform animatedProp;
    private Animator animator;
    private PlayerController current;
    public static event Action<Pothole,PlayerController> PlayerPotted;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        if (pointsLabel == null) { return; }
        pointsLabel.text = Points.ToString();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController player))
        {
            player.Potted = true;
            player.Rigidbody.Sleep();
            player.transform.SetParent(animatedProp);
            player.transform.localPosition = new Vector2(0,0);
            current = player;
            animator.SetTrigger("Potted");
        }
    }

    public void AnimationFinished()
    {
        PlayerPotted?.Invoke(this, current);
    }
}
