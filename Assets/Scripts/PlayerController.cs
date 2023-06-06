using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("CHARACTER")]

    [SerializeField]
    private float m_fSpeed = 3f;
    private CharacterController m_CharacContr;
    private void Awake()
    {
        m_CharacContr = GetComponent<CharacterController>();
    }

    private void Start()
    {
        
    }


    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 vCharacPos = (transform.right * Input.GetAxis("Horizontal") + (transform.forward * Input.GetAxis("Vertical")));
        vCharacPos *= m_fSpeed * Time.fixedDeltaTime;
        m_CharacContr.Move(vCharacPos);
    }
}
