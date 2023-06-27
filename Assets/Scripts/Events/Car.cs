using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public Vector3 carImpulse;
    private bool run = false;
    public float timeToExist;
    public int damageToPlayer = 1;

    private Rigidbody carRb;

    private void Start()
    {
        carRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (run && carRb.velocity.magnitude < carImpulse.magnitude)
        {
            carRb.AddForce(carImpulse, ForceMode.Impulse);
        }
    }

    public void Run()
    {
        run = true;
    }


    public IEnumerator CountdownToLeave()
    {
        float currCountdownValue = timeToExist;
        while (currCountdownValue > 0)
        {
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }

        gameObject.SetActive(false);

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (run && collision.gameObject.CompareTag("Player"))
        {
            carRb.AddForce(collision.impulse.x*1.75f, collision.impulse.y*1.75f + 10, collision.impulse.z * 1.75f, ForceMode.Impulse);
            run = false;
            GameManager.Instance.AddLifes(-damageToPlayer);
            StopCoroutine(CountdownToLeave());
            StartCoroutine(CountdownToLeave());
        }
    }

}
