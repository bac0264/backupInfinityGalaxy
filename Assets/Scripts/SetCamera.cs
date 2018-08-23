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
        panel = GameObject.FindGameObjectWithTag("Fade");
        if (panel != null) {
            panel.transform.GetChild(0).gameObject.SetActive(true);
        }
        map = GameObject.FindGameObjectWithTag("Map");
        if (map != null)
        {
            Camera.main.transform.position = new Vector3(map.GetComponent<Map>().pos.x, map.GetComponent<Map>().pos.y, Camera.main.transform.position.z);
            map.GetComponent<Map>().back = true;
        }
        gameObject.GetComponent<Animator>().Play("blur");
    }
    public void _SettingBack()
    {
        GameObject setting = GameObject.FindGameObjectWithTag("Setting");
        GameObject buttonBlur = GameObject.FindGameObjectWithTag("Blur");
        if(buttonBlur != null)
        buttonBlur.SetActive(false);
        if (setting != null)
            setting.GetComponent<Animator>().Play("BackSetting");
        gameObject.GetComponent<Animator>().Play("noblur");
        if (map != null)
        {
            map.transform.GetChild(0).gameObject.SetActive(true);
            map.transform.GetChild(1).gameObject.SetActive(true);
        }
        StartCoroutine(timeToBack());
    }
    IEnumerator timeToBack()
    {
        yield return new WaitForSeconds(0.4f);
        if (panel != null)
        {
            panel.transform.GetChild(0).gameObject.SetActive(false);
        }
        if (map != null)
        {
            if (map.tag == "Map")
            {
                SceneManager.LoadScene("SelectLevel");
            }
            else SceneManager.LoadScene("SelectPlanet");
        }
        else SceneManager.LoadScene("SelectPlanet");

    }
}
