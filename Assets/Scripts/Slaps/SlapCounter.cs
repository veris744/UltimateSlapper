using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]

public class SlapCounter : MonoBehaviour
{
    public int pointsToAdd = 100;
    [HideInInspector] public SlapCounter lastSlapped = null;
    [HideInInspector] public SlapCounter toSlap = null;
    [HideInInspector] public bool canCount = true;
    [HideInInspector] public bool goSlap = false;
    private void OnCollisionEnter(Collision collision)
    {
        SlapCounter slapCount = collision.gameObject.GetComponent<SlapCounter>();
        //if (slapCount && (slapCount.lastSlapped == null || slapCount.toSlap != this))
        if (slapCount && slapCount.canCount && canCount && goSlap)
        {
            slapCount.OnCollisionWithSlapped();

            //Ragdoll
            //if (collision.gameObject.GetComponentInParent<Ragdoll>())
            //    collision.gameObject.GetComponentInParent<Ragdoll>().EnableRagdoll(true);
            
            if (collision.gameObject.GetComponent<Ragdoll>())
            {
                collision.gameObject.GetComponent<Ragdoll>().EnableRagdoll(true);
                if (collision.gameObject.GetComponent<Animator>())
                {
                    collision.gameObject.GetComponent<Animator>().enabled = false;
                }
            }
                

            if (gameObject.GetComponent<Ragdoll>())
                gameObject.GetComponent<Ragdoll>().EnableRagdoll(true);

            //slapCount.lastSlapped = this;
            //toSlap = slapCount;

            canCount = false;
            goSlap = false;
            slapCount.goSlap = true;
            
        }
    }

    public void OnCollisionWithSlapped()
    {
        ((GameManager)GameManager.Instance).slapCount++;
        ((GameManager)GameManager.Instance).slapPointsCount += pointsToAdd;
        ((GameManager)GameManager.Instance).comboTimer = ConstParamenters.COMBO_TIMER_DEFAULT;

        //Debug

        Debug.Log(((GameManager)GameManager.Instance).slapCount);
    }
}
