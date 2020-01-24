using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 Offset;
    public float pitch = 2f;
    public float zoomSpeed = 4f;
    public float minZoom = 5f;
    public float maxZoom = 15f;
    public float yawSpeed = 100f;

    private float currentZoom = 10f;
    private float currentYaw = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        if(Input.GetKey(KeyCode.Q))
            currentYaw += yawSpeed * Time.deltaTime;
        if(Input.GetKey(KeyCode.E))
            currentYaw -= yawSpeed * Time.deltaTime;

    }
    void LateUpdate()
    {
        transform.position = target.position - Offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);
        transform.RotateAround(target.position, Vector3.up, currentYaw);
    }
}
