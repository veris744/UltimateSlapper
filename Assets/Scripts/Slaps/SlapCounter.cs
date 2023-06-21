using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public class SlapCounter : MonoBehaviour
{
    private int pointsToAdd = 100;
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
            if (collision.gameObject.GetComponent<Ragdoll>())
                collision.gameObject.GetComponent<Ragdoll>().EnableRagdoll(true);

            if (gameObject.GetComponent<Ragdoll>())
                gameObject.GetComponent<Ragdoll>().EnableRagdoll(true);

            //slapCount.lastSlapped = this;
            //toSlap = slapCount;

            canCount = false;
            goSlap = false;
            slapCount.goSlap = true;

            if (gameObject.GetComponent<MeshRenderer>())
            {
                gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
            }
            else
            {
                Debug.Log("Dolor");
            }
            
        }
    }

    public void OnCollisionWithSlapped()
    {
        ((GameManager)GameManager.Instance).slapCount++;
        ((GameManager)GameManager.Instance).comboTimer = ConstParamenters.COMBO_TIMER_DEFAULT;

        Debug.Log(((GameManager)GameManager.Instance).slapCount);
    }
}
