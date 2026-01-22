using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] float zoomSpeed = 10f;
    [SerializeField] float rotationSpeed = 0.2f;
    [SerializeField] float panSpeed = 0.01f;

    private Transform pivot;

    private void Awake()
    {
        pivot = transform.parent;
        if (!pivot)
        {
            Debug.LogError("No Camera Pivot!");
        }
    }

    private void Update()
    {
        HandleZoom();
        HandleRotation();
        HandlePan();
    }

    void HandleZoom()
    {
        float scroll = Mouse.current.scroll.ReadValue().y;
        if (Mathf.Abs(scroll) > 0.01f)
        {
            transform.localPosition += Vector3.forward * scroll * zoomSpeed * Time.deltaTime;
        }
    }


    void HandleRotation()
    {
        if (Mouse.current.rightButton.isPressed && pivot)
        {
            Vector2 delta = Mouse.current.delta.ReadValue();

            float yaw = delta.x * rotationSpeed;
            float pitch = -delta.y * rotationSpeed;

            pivot.Rotate(Vector3.up, yaw, Space.World);
            pivot.Rotate(Vector3.right, pitch, Space.Self);
        }
    }

    void HandlePan()
    {
        if (Mouse.current.middleButton.isPressed && pivot)
        {
            Vector2 delta = Mouse.current.delta.ReadValue();

            Vector3 move =
                (-pivot.right * delta.x + -pivot.up * delta.y) * panSpeed;

            pivot.position += move;
        }
    }
}
