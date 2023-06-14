using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [Header("PARAMETERS")]
    public Transform baseCollRagdoll;

    private Collider[] allColliders;
    private List<Rigidbody> allRigs = new List<Rigidbody>();
    private List<Collider> collRagdolls = new List<Collider>();

    private void Awake()
    {
        allColliders = baseCollRagdoll.GetComponentsInChildren<Collider>();
        foreach (var coll in allColliders)
        {
            if (coll.transform != transform)
            {
                if (coll.GetComponent<Rigidbody>())
                {
                    allRigs.Add(coll.GetComponent<Rigidbody>());
                    collRagdolls.Add(coll.GetComponent<Collider>());
                }
            }
        }

        EnableRagdoll(false);
    }

    public void EnableRagdoll(bool _enableRagdoll)      //Ejemplo, true
    {
        //TODO: Desactivar animator controller
        //GetComponent<Collider>().enabled = !_enableRagdoll;     //Collider del personaje

        foreach (Rigidbody rig in allRigs)
        {
            rig.useGravity = _enableRagdoll;
            rig.isKinematic = !_enableRagdoll;
        }

        foreach (Collider coll in collRagdolls)
        {
            coll.enabled = _enableRagdoll;
        }

        GetComponent<Rigidbody>().useGravity = !_enableRagdoll;
        //GetComponent<Rigidbody>().isKinematic = _enableRagdoll;
    }
}
