using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private Rigidbody rig;
    private Animator animator;

    public float Speed = 5;
    public float JumpForce = 0.4f;
    public float thurst = 10;

    public bool bIsGrounded = true;

    private Vector3 moveInput;
    private Vector3 moveVelocity;

    private Camera mainCamera;
    public GameObject Player { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Player = gameObject;
        rig = GetComponent<Rigidbody>();
        bIsGrounded = true;
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        moveInput = new Vector3(hAxis, 0f, vAxis);
        moveVelocity = transform.forward * Speed * moveInput.sqrMagnitude;

        //rig.velocity = new Vector3(hAxis * Speed, rig.velocity.y, vAxis * Speed);

        Vector3 cameraForward = mainCamera.transform.forward;
        cameraForward.y = 0;

        Quaternion cameraRelativeRotation = Quaternion.FromToRotation(Vector3.forward, cameraForward);   //nos creamos una variable de tipo Quaternion llamada cameraRelativeRotation, a la cual le asignamos el resultado de la llamada a la función Quaternion.FromToRotation() la cual usa como primer parámetro la posición actual, y como segundo la posición hacia la que queremos rotar, por ello se usa en los paréntesis (Vector3.forward, cameraForward);
        Vector3 lookToward = cameraRelativeRotation * moveInput;

        if (moveInput.sqrMagnitude > 0)
        {
            Ray lookRay = new Ray(transform.position, lookToward);
            transform.LookAt(lookRay.GetPoint(1));
        }

        if (Input.GetKeyDown(KeyCode.Space) && bIsGrounded == true)
        {
            Jump();
        }
    }

    public void Jump()
    {
        bIsGrounded = false;
        rig.AddForce(0, thurst, 0, ForceMode.Impulse);
        //animator.SetBool("OnJump", true);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            bIsGrounded = true;
            //animator.SetBool("OnJump", false);
        }
    }
}
