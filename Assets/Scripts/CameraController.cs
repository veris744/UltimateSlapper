using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float damping = 1f;
    public float rotateSpeed = 5f;
    private Vector3 offset;

    void Start()
    {
        offset = player.transform.position - transform.position;
    }

    void LateUpdate()
    {
        float currentAngle = transform.eulerAngles.y;
        float desiredAngle = player.transform.eulerAngles.y;
        float angle = Mathf.LerpAngle(currentAngle, desiredAngle, Time.deltaTime * damping);

        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        transform.position = player.transform.position - (rotation * offset);

        transform.LookAt(player.transform);
        //Control mouse

        //float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        //player.transform.Rotate(0, horizontal, 0);
        //Quaternion rotationNew = Quaternion.Euler(0, desiredAngle, 0);
        //transform.position = player.transform.position - (rotation * offset);

    }
}
