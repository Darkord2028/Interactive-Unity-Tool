using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    public void Spawn(int primitiveType)
    {
        PrimitiveType type = (PrimitiveType)primitiveType;
        
        GameObject go = GameObject.CreatePrimitive(type);
        spawnedObjects.Add(go);
        currentSelected = go;
        go.transform.position = Vector3.zero;
        go.transform.rotation = Quaternion.identity;
        SetPosition(go.transform.position);
        SetRotation(go.transform.eulerAngles);
    }

    public void ClearAll()
    {
        foreach (GameObject GO in spawnedObjects)
        {
            Destroy(GO);
        }
        spawnedObjects.Clear();
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
            SetPosition(Vector3.zero);
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

}
