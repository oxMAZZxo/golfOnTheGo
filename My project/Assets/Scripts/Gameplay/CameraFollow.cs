using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance {get; private set;}
    [field: SerializeField] public Transform Target { get; set; }
    [SerializeField,Range(0.001f,1f)]private float smoothTime;
    private Vector3 refVelocity;

    void Awake()
    {
        if(Instance == null && Instance != this)
        {
            Instance = this;
        }else
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (Target == null) { return; }
        Vector3 newposition = Target.position;
        newposition.z = -10;
        transform.position = Vector3.SmoothDamp(transform.position, newposition, ref refVelocity, smoothTime);
    }
}
