using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public int damageToPlayer = 5;
    public float restingTime = 5;

    [HideInInspector]
    public  PlayerController player;

    private Rigidbody rb;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && player != null)
        {
            player = null;
            audioSource.Play();
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
