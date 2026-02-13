using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class Pothole : MonoBehaviour
{
    [field: SerializeField, Range(1, 10)] public int Points { get; private set; }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerController player))
        {
            //award player.
            player.gameObject.SetActive(false);
            //something something......
        }
    }
}
