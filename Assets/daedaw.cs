using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class daedaw : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("INIT", 4f);
    }

    void INIT()
    {
        SceneManager.LoadScene("MainMenu Demo (Elite)");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
