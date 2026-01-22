using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public enum Axis
{
    X = 0,
    Y = 1,
    Z = 2
}

public class ObjectManager : MonoBehaviour
{
    [Header("Input Fields")]
    [SerializeField] TMP_InputField posXInput;
    [SerializeField] TMP_InputField posYInput;
    [SerializeField] TMP_InputField posZInput;
    [SerializeField] TMP_InputField rotXInput;
    [SerializeField] TMP_InputField rotYInput;
    [SerializeField] TMP_InputField rotZInput;

    private GameObject currentSelected;
    private List<GameObject> spawnedObjects = new List<GameObject>();
    
    void Start()
    {
        SetPosition(Vector3.zero);
        SetRotation(Vector3.zero);

        posXInput.onEndEdit.AddListener(delegate { OnPositionChanged(Axis.X); });
        posYInput.onEndEdit.AddListener(delegate { OnPositionChanged(Axis.Y); });
        posZInput.onEndEdit.AddListener(delegate { OnPositionChanged(Axis.Z); });

        rotXInput.onEndEdit.AddListener(delegate { OnRotationChanged(Axis.X); });
        rotYInput.onEndEdit.AddListener(delegate { OnRotationChanged(Axis.Y); });
        rotZInput.onEndEdit.AddListener(delegate { OnRotationChanged(Axis.Z); });
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            SelectShape();
        }
    }

    public void Spawn(int primitiveType)
    {
        PrimitiveType type = (PrimitiveType)primitiveType;
        
        GameObject go = GameObject.CreatePrimitive(type);
        spawnedObjects.Add(go);
        go.transform.position = Vector3.zero;
        go.transform.rotation = Quaternion.identity;
        SetCurrentShape(go);
    }

    public void ClearAll()
    {
        foreach (GameObject GO in spawnedObjects)
        {
            Destroy(GO);
        }
        spawnedObjects.Clear();
        SetCurrentShape(null);
    }

    private void SetPosition(Vector3 position)
    {
        posXInput.SetTextWithoutNotify(position.x.ToString("F2"));
        posYInput.SetTextWithoutNotify(position.y.ToString("F2"));
        posZInput.SetTextWithoutNotify(position.z.ToString("F2"));
    }

    private void SetRotation(Vector3 rotation)
    {
        rotXInput.SetTextWithoutNotify(rotation.x.ToString("F2"));
        rotYInput.SetTextWithoutNotify(rotation.y.ToString("F2"));
        rotZInput.SetTextWithoutNotify(rotation.z.ToString("F2"));
    }

    private void OnPositionChanged(Axis axis)
    {
        if (!currentSelected)
        {
            SetPosition(Vector3.zero);
            return;
        }

        TMP_InputField inputField = axis switch
        {
            Axis.X => posXInput,
            Axis.Y => posYInput,
            Axis.Z => posZInput,
            _ => null
        };

        if (!inputField) return;

        if (!float.TryParse(inputField.text, out float value))
        {
            SetPosition(currentSelected.transform.position);
            return;
        }

        Vector3 pos = currentSelected.transform.position;
        pos[(int)axis] = value;
        currentSelected.transform.position = pos;
        SetPosition(currentSelected.transform.position);
    }

    private void OnRotationChanged(Axis axis)
    {
        if (!currentSelected)
        {
            SetRotation(Vector3.zero);
            return;
        }

        TMP_InputField inputField = axis switch
        {
            Axis.X => rotXInput,
            Axis.Y => rotYInput,
            Axis.Z => rotZInput,
            _ => null
        };

        if (!inputField) return;

        if (!float.TryParse(inputField.text, out float value))
        {
            SetRotation(currentSelected.transform.eulerAngles);
            return;
        }

        Vector3 rot = currentSelected.transform.eulerAngles;
        rot[(int)axis] = value;
        currentSelected.transform.eulerAngles = rot;
        SetRotation(currentSelected.transform.eulerAngles);

    }

    private void SelectShape()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (spawnedObjects.Contains(hitInfo.collider.gameObject))
            {
                SetCurrentShape(hitInfo.collider.gameObject);
            }
        }
    }

    private void SetCurrentShape(GameObject gameObject)
    {
        if (!gameObject)
        {
            currentSelected = null;
            return;
        }
        else if (currentSelected)
        {
            currentSelected.layer = LayerMask.NameToLayer("Default");
        }
        currentSelected = gameObject;
        currentSelected.layer = LayerMask.NameToLayer("Outline");
        SetPosition(currentSelected.transform.position);
        SetRotation(currentSelected.transform.eulerAngles);
    }

    public void OnFocusInput(InputAction.CallbackContext context)
    {
        if (!context.performed || currentSelected == null)
            return;

        Transform pivot = Camera.main.transform.parent;
        if (!pivot) return;

        pivot.position = currentSelected.transform.position;
    }

}
