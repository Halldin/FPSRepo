using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float MouseSensitivity = 100.0f;
    public Transform PlayerBody = null;
    float yRotation = 0f;

    void Start()
    {

    }


    void Update()
    {
        
        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        yRotation = -mouseY;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(yRotation, 0, 0);
        PlayerBody.Rotate(Vector3.up * mouseX);


    }
}
