using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    private void Start()
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
