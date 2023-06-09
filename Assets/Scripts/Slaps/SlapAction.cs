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
                {
                    hit.collider.GetComponent<Ragdoll>().EnableRagdoll(true);
                    if (hit.collider.GetComponent<Animator>())
                    {
                        hit.collider.GetComponent<Animator>().enabled = false;
                    }
                }

                

                if (hit.collider.GetComponent<SlapCounter>())
                {
                    hit.collider.GetComponent<SlapCounter>().goSlap = true;
                    ((GameManager)GameManager.Instance).isCombo = true;
                    ((GameManager)GameManager.Instance).comboTimer = ConstParamenters.COMBO_TIMER_DEFAULT;
                    if (((GameManager)GameManager.Instance).slapCount != 0)
                    {
                        ((GameManager)GameManager.Instance).slapPointsCount = hit.collider.GetComponent<SlapCounter>().pointsToAdd;
                    }
                    else
                    {
                        ((GameManager)GameManager.Instance).AddPoints(hit.collider.GetComponent<SlapCounter>().pointsToAdd);
                        ((GameManager)GameManager.Instance).slapPointsCount = 0;
                        ((GameManager)GameManager.Instance).slapCount = 0;
                    }
                    if (hit.transform.CompareTag("NPC"))
                        hit.transform.GetComponentInParent<AudioSource>().Play();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 direction = transform.forward * slapRange;
        //Gizmos.DrawRay(transform.position, direction);
    }
}
