using UnityEngine;
using UnityEngine.UI;

public class TouchVisualiser : MonoBehaviour
{
    public static TouchVisualiser Instance {get; private set;}
    [field: SerializeField] public PlayerController CurrentPlayer { get; set; }

    [SerializeField] private Image visual;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Gradient dragGradient;
    private RectTransform parentRect;
    private RectTransform visualRect;

    private bool touchStarted;

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

    void OnEnable()
    {
        TouchControls.TouchStarted += OnTouchStarted;
        TouchControls.TouchEnded += OnTouchEnded;
        parentRect = GetComponent<RectTransform>();
        visualRect = visual.rectTransform;
        visual.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!touchStarted || CurrentPlayer == null)
            return;

        Vector2 playerScreenPos = mainCamera.WorldToScreenPoint(CurrentPlayer.transform.position);
        Vector2 touchPos = TouchControls.Instance.CurrentTouchPosition;

        Vector2 direction = touchPos - playerScreenPos;
        float distance = direction.magnitude;

        parentRect.position = playerScreenPos;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        parentRect.rotation = Quaternion.Euler(0, 0, angle - 90f);

        float clampedDistance = Mathf.Min(direction.magnitude, CurrentPlayer.MaxDragDistance);
        visualRect.sizeDelta = new Vector2(visualRect.sizeDelta.x, clampedDistance);

        float t = Mathf.Clamp01(distance / CurrentPlayer.MaxDragDistance);
        Color sampledColor = dragGradient.Evaluate(t);
        visual.color = sampledColor;
    }

    private void OnTouchStarted(object sender, Vector2 e)
    {
        touchStarted = true;
        visual.gameObject.SetActive(true);
    }

    private void OnTouchEnded(object sender, Vector2 e)
    {
        touchStarted = false;
        visual.gameObject.SetActive(false);
    }

    void OnDisable()
    {
        TouchControls.TouchStarted -= OnTouchStarted;
        TouchControls.TouchEnded -= OnTouchEnded;
    }
}