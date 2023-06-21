using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public int damageToPlayer = 5;
    public PlayerController player;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.AddPoints(-damageToPlayer);
        }
        player = null;
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 v = (player.transform.position - transform.position) * 50;
            rb.AddForce(v.x, 0, v.z, ForceMode.Impulse);
        }

    }
}
