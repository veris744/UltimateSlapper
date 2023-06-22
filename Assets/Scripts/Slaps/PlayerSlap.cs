using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlap : SlapAction
{
    private bool isSlapped = false;
    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isSlapped)
        {
            
            Slap(GetComponentInParent<PlayerController>().playerForce);
            isSlapped = true;
        }
        if (Input.GetButtonUp("Fire1"))
            isSlapped = false;
    }
}
