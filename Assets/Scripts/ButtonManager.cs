using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ButtonManager : MonoBehaviour
{
    GameObject map;
    GameObject setting;
    private void Start()
    {
        map = GameObject.FindGameObjectWithTag("Map");
        setting = GameObject.Find("Setting");
    }
    public void _BackToMenu()
    {
        if (Fade.instance != null)
        {
            Fade.instance.sceneName = PlayerPrefs.GetString("SelectPlanet");
            Fade.instance.check = true;
            Fade.instance.FadeIn = true;
            if (map != null)
            {
                map.transform.GetChild(0).gameObject.SetActive(false);
                map.transform.GetChild(1).gameObject.SetActive(false);
                StartCoroutine(timetoTransforms());
            }
        }
    }
    public void _nextresetGameScene()
    {
        SceneManager.LoadScene("ResetScene");
    }
    public void _resetGame()
    {
        PlayerPrefs.SetInt("PlayingPlanet", -1);
        PlayerPrefs.SetInt("PlayerLevel", 0);
        PlayerPrefs.SetInt("CompleteLastPlanet", 0);
    }
    public void CreateBtnClick()
    {
        SceneManager.LoadScene("ListMap");
    }
    // Selectlevel
    public void _SettingBtnClick()
    {
        if (Fade.instance != null) Fade.instance.check = false;
        map.GetComponent<Map>().pos = Camera.main.transform.position;
        map.transform.GetChild(0).gameObject.SetActive(false);
        map.transform.GetChild(1).gameObject.SetActive(false);
        SceneManager.LoadScene("Setting");
    }
    // Planet
    public void _SettingClicked()
    {
        Debug.Log(map);
        if (map != null)
        {
            Debug.Log("run");
            map.transform.GetChild(0).gameObject.SetActive(false);
        }
        if (setting != null)
        {
            setting.GetComponent<Animator>().Play("Setting");
        }
        Camera.main.GetComponent<Animator>().Play("blur");
        Camera.main.GetComponent<SelectPlanetManager>().enabled = false;
    }
    public void _SettingBackClicked()
    {
        if (map != null)
        {
            map.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (setting != null)
        {
            setting.GetComponent<Animator>().Play("BackSetting");
        }
        Camera.main.GetComponent<Animator>().Play("noblur");
        Camera.main.GetComponent<SelectPlanetManager>().enabled = true;
    }
    IEnumerator timetoTransforms()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(map);
    }
}
