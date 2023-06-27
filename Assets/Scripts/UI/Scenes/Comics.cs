using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Comics : MonoBehaviour
{
    public GameObject Panel1;
    public GameObject Panel2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ButtonNext()
    {
        Panel1.SetActive(false);
        Panel2.SetActive(true);
    }
    public void ButtonStart()
    {
        SceneManager.LoadScene("City");
    }
}
