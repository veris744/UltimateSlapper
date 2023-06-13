using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickable : MonoBehaviour
{
    public float pickableTime;
    public float timeToRegenerate = -1;


    protected MeshRenderer meshRenderer;
    protected SphereCollider objectCollider;

    public abstract void OnTriggerWithPlayer(PlayerController player);
    
    public abstract void ResetPlayer(PlayerController player);

    


    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        objectCollider = GetComponent<SphereCollider>();
    }


    public IEnumerator CountdownToReset(PlayerController player)
    {
        float currCountdownValue = pickableTime;
        while (currCountdownValue > 0)
        {
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }

        ResetPlayer(player);

        if (timeToRegenerate >= 0)
        {
            StartCoroutine(CountdownToRegenerate());
        }
    }

    public IEnumerator CountdownToRegenerate()
    {
        float currCountdownValue = timeToRegenerate;
        while (currCountdownValue > 0)
        {
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }

        //set new location

        meshRenderer.enabled = true;
        objectCollider.enabled = true;

    }
}

