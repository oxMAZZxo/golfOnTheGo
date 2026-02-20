using System;
using UnityEngine;

public class Forcefield : MonoBehaviour
{
    [SerializeField,Range(20f,100f)]private float accumilatedForce = 50f;
    [SerializeField]private LayerMask physicsOverlapLayer;
    [SerializeField]private float overlapRadius;
    [SerializeField]private Vector2 overlapPositionOffset;

    void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + (Vector3)overlapPositionOffset, overlapRadius, physicsOverlapLayer);

        foreach(Collider2D collider in colliders)
        {
            if(collider.TryGetComponent(out Rigidbody2D rb))
            {
                rb.AddForce(transform.right * accumilatedForce, ForceMode2D.Force);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + (Vector3)overlapPositionOffset,overlapRadius);
    }
}
