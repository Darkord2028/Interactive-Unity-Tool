using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [Header("Transfrom Panel")]
    [SerializeField] TextMeshProUGUI positionX;
    [SerializeField] TextMeshProUGUI positionY;
    [SerializeField] TextMeshProUGUI positionZ;
    [SerializeField] TextMeshProUGUI rotationX;
    [SerializeField] TextMeshProUGUI rotationY;
    [SerializeField] TextMeshProUGUI rotationZ;

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
    }

    void Update()
    {
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

    private void SetPosition(Vector3 position)
    {
        positionX.text = position.x.ToString("F2");
        positionY.text = position.y.ToString("F2");
        positionZ.text = position.z.ToString("F2");
    }

    private void SetRotation(Vector3 rotation)
    {
        rotationX.text = rotation.x.ToString("F2");
        rotationY.text = rotation.y.ToString("F2");
        rotationZ.text = rotation.z.ToString("F2");
    }

}
