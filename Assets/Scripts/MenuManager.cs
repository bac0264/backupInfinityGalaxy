using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {


    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
   void Start()
    {
        if (Fade.instance != null)
        {
            if (Fade.instance.check == true)
            {
                Fade.instance.FadeOut = true;
                // Fade.instance.check = false;
            }
        }
    }
}
