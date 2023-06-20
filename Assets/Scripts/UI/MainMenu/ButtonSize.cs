using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSize : MonoBehaviour
{
public void PointEnter() 
    {
        transform.localScale = new Vector2(4.0f, 4.0f);
    }

public void PointExit() 
    {
        transform.localScale = new Vector2(3.3f, 3.3f);
    }

}
