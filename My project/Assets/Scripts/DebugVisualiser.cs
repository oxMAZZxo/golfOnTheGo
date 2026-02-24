using UnityEngine;

public class DebugVisualiser : MonoBehaviour
{
    public float radius = 1f;
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position,radius);
    }
}
