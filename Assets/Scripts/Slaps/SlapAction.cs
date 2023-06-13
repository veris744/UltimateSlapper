using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlapAction : MonoBehaviour
{
    [Header("PARAMETERS")]
    public float slapForce = 2000f;
    public float slapRange = 2f;
    

    public void Slap()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward * slapRange, out hit))
        {
            if (hit.rigidbody)
            {
                hit.rigidbody.AddForce(transform.forward * slapForce);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 direction = transform.forward * slapRange;
        Gizmos.DrawRay(transform.position, direction);
    }
}
