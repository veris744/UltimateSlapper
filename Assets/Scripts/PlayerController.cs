using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController playerController;
    private bool isGrounded;
    private Vector3 playerVelocity;

    [Header("CHARACTER")]
    public float playerSpeed = 5f;
    public float playerForce = 1000f;
    public float HP = 0;
    public float scoreMultiplier = 1;
    [HideInInspector] public Animator animator;
    [SerializeField] private float jumpHeight = 3f;

    [Header("OTHERS")]
    [SerializeField] private Camera cam;

    [HideInInspector] public bool speedBoosted;
    [HideInInspector] public bool forceBoosted;
    [HideInInspector] public bool scoreBoosted;

    private void Awake()
    {
        playerController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        speedBoosted = false;
        forceBoosted = false;
        scoreBoosted = false;
    }

    private void Update()
    {
        animator.SetFloat("Speed", Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 ? 1f : 0f);
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
