using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("CHARACTER")]
    private CharacterController playerController;
    private bool isGrounded;
    private Vector3 playerVelocity;
    public float playerSpeed = 5f;
    public float playerForce = 5f;
    [SerializeField] private float jumpHeight = 3f;

    [Header("OTHERS")]
    [SerializeField] private Camera cam;
    private void Awake()
    {
        playerController = GetComponent<CharacterController>();
    }


    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        isGrounded = playerController.isGrounded;
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = 0f;

        Vector3 playerMovement = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0f) *
            new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        playerMovement.Normalize();

        playerController.Move(playerMovement * playerSpeed * Time.fixedDeltaTime);

        if (playerMovement != Vector3.zero)
        {
            Quaternion desiredRot = Quaternion.LookRotation(playerMovement, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRot, playerSpeed * Time.fixedDeltaTime);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(-jumpHeight * Physics.gravity.y);
        }
        playerVelocity.y += Physics.gravity.y * Time.fixedDeltaTime;
        playerController.Move(playerVelocity * Time.fixedDeltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        Pickable pickable = other.GetComponent<Pickable>();
        if (pickable != null)
        {
            pickable.OnTriggerWithPlayer(this);
            return;
        }

        Event eventObject = other.GetComponent<Event>();
        if (eventObject != null)
        {
            eventObject.OnTriggerWithPlayer(this);
            return;
        }
    }
}
