using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlapAction : MonoBehaviour
{
    [Header("PARAMETERS")]
    //public float slapForce = 1000f;
    public float slapRange = 2f;
    

    public void Slap(float force)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward * slapRange, out hit, slapRange))
        {
            if (hit.rigidbody)
            {
                hit.rigidbody.AddForce(transform.forward * force);
                if (hit.collider.GetComponent<Ragdoll>())
                    hit.collider.GetComponent<Ragdoll>().EnableRagdoll(true);

                if (hit.collider.GetComponent<SlapCounter>())
                {
                    hit.collider.GetComponent<SlapCounter>().goSlap = true;
                    ((GameManager)GameManager.Instance).isCombo = true;
                    ((GameManager)GameManager.Instance).comboTimer = ConstParamenters.COMBO_TIMER_DEFAULT;
                    ((GameManager)GameManager.Instance).slapPointsCount = hit.collider.GetComponent<SlapCounter>().pointsToAdd;
                }
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
