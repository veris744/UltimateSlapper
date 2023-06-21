using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePickable : Pickable
{
    public float forceMultiplier = 2;
    private float earlierForce;

    
    public override void OnTriggerWithPlayer(PlayerController player)
    {
        earlierForce = player.playerForce;
        player.playerForce *= forceMultiplier;

        meshRenderer.enabled = false;
        objectCollider.enabled = false;

        StartCoroutine(CountdownToReset(player));

    }

    public override void ResetPlayer(PlayerController player)
    {
        player.playerForce = earlierForce;
    }

    
}
