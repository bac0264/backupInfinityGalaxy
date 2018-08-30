using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetCamera : MonoBehaviour
{
    // Use this for initialization
    GameObject map;
    GameObject panel;
    void Start()
    {
        if (Fade.instance != null)
        {
            if (Fade.instance.check == true)
            {
                Fade.instance.FadeOut = true;
            }
        }
        panel = GameObject.FindGameObjectWithTag("Fade");
        if (panel != null) {
            panel.transform.GetChild(0).gameObject.SetActive(true);
        }
        map = GameObject.FindGameObjectWithTag("Map");
        if (map != null)
        {
            //Camera.main.transform.position = new Vector3(map.GetComponent<Map>().pos.x, map.GetComponent<Map>().pos.y, Camera.main.transform.position.z);
            map.GetComponent<Map>().back = true;
            //map.SetActive(false);
        }
    }
    public void _SettingBack()
    {
        //GameObject setting = GameObject.FindGameObjectWithTag("Setting");
        //GameObject buttonBlur = GameObject.FindGameObjectWithTag("Blur");
        //if(buttonBlur != null)
        //buttonBlur.SetActive(false);
        //if (setting != null)
        //    setting.GetComponent<Animator>().Play("BackSetting");
        //gameObject.GetComponent<Animator>().Play("noblur");
        if (map != null)
        {
            map.transform.GetChild(0).gameObject.SetActive(true);
            map.transform.GetChild(1).gameObject.SetActive(true);
        }
        StartCoroutine(timeToBack());
    }
    IEnumerator timeToBack()
    {
        if (panel != null)
        {
            panel.transform.GetChild(0).gameObject.SetActive(false);
        }
        if (panel != null)
        {
            panel.transform.GetChild(0).gameObject.SetActive(false);
        }
        if (Fade.instance != null)
        {
            GameObject Button = GameObject.FindGameObjectWithTag("ButtonMenu");
            if (Button != null) {
                Button.SetActive(false);
            }
            Fade.instance.check = true;
            Fade.instance.FadeIn = true;
            Fade.instance.sceneName = "SelectLevel";
            yield return new WaitForSeconds(0.3f);
            //if (map != null)
            //{
            //    map.SetActive(true);
            //}
        }
    }
}
