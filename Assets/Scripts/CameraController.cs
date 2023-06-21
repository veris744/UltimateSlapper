using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private Vector3 camOffset = Vector3.zero;

    private float rotationY, rotationX;
    private Vector3 currentRotation;
    private Vector3 smoothVel = Vector3.zero;

    public float moveSpeed = 0.2f;
    public float mouseSensitivity = 1f;
    public float distanceFromPlayer = 3f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = (-Input.GetAxis("Mouse Y")) * mouseSensitivity;

        rotationY += mouseX;
        rotationX += mouseY;

        rotationX = Mathf.Clamp(rotationX, -40, 40);

        Vector3 nextRotation = new Vector3(rotationX, rotationY, 0f);

        currentRotation = Vector3.SmoothDamp(currentRotation, nextRotation, ref smoothVel, moveSpeed);
        transform.localEulerAngles = currentRotation;

        transform.position = ((playerTransform.position + camOffset) - transform.forward * distanceFromPlayer);
    }
}
