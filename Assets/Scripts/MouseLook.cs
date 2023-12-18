using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;
    private Quaternion  StartingRot = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        StartingRot = transform.rotation;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        //xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        var rotationTotal = /*StartingRot.eulerAngles +*/ new Vector3 (xRotation, 0f, 0f);
        transform.localRotation =   Quaternion.Euler(rotationTotal);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
