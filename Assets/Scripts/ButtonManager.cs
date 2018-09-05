using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ButtonManager : MonoBehaviour
{
    GameObject map;
    GameObject setting;
    bool check;
    private void Start()
    {
        map = GameObject.FindGameObjectWithTag("Map");
        setting = GameObject.Find("Setting");
        check = true;
    }
    public void _Start()
    {
        if (Fade.instance != null)
        {
            Fade.instance.check = true;
            Fade.instance.FadeIn = true;
            Fade.instance.sceneName = "SelectPlanet";
        }
        GameObject button = GameObject.FindGameObjectWithTag("ButtonMenu");
        if (button != null) {
            button.gameObject.SetActive(false);
        }
        //SceneManager.LoadScene("SelectPlanet");
    }
    public void _BackToStart()
    {
        if (Fade.instance != null)
        {
            Fade.instance.check = true;
            Fade.instance.FadeIn = true;
            Fade.instance.sceneName = "Menu";
            if (map != null)
            {
                map.transform.GetChild(0).gameObject.SetActive(false);
                map.transform.GetChild(1).gameObject.SetActive(false);
                StartCoroutine(timetoTransforms());
            }
            GameObject button = GameObject.FindGameObjectWithTag("Facebook");
            if (button != null) button.SetActive(false);
        }
    }
    public void _BackToMenu()
    {
        if (Fade.instance != null)
        {
            Fade.instance.check = true;
            Fade.instance.FadeIn = true;
            Fade.instance.sceneName = "SelectPlanet";
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
    public void _rankingButton()
    {
        Initiate.Fade("rank", new Color(1, 1, 1, 1), 5.0f);
    }
    public void CreateBtnClick()
    {
        SceneManager.LoadScene("ListMap");
    }
    // Selectlevel
    //public void _SettingBtnClick()
    //{
    //    if (Fade.instance != null) Fade.instance.check = false;
    //    map.GetComponent<Map>().pos = Camera.main.transform.position;
    //    map.transform.GetChild(0).gameObject.SetActive(false);
    //    map.transform.GetChild(1).gameObject.SetActive(false);
    //    SceneManager.LoadScene("Setting");
    //}
    // Planet
    public void _SettingClicked()
    {
        StartCoroutine(TimeToDo());
    }
    //public void _SettingBackClicked()
    //{
    //    if (map != null)
    //    {
    //        map.transform.GetChild(0).gameObject.SetActive(true);
    //    }
    //    if (setting != null)
    //    {
    //        setting.GetComponent<Animator>().Play("BackSetting");
    //    }
    //}
    IEnumerator timetoTransforms()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(map);
    }
    IEnumerator TimeToDo()
    {
        if (check)
        {
            Debug.Log(map);

            if (setting != null)
            {
                setting.GetComponent<Animator>().Play("Setting");
                yield return new WaitForSeconds(0.4f);
            }
            check = false;
        }
        else
        {
            if (setting != null)
            {
                setting.GetComponent<Animator>().Play("BackSetting");
                yield return new WaitForSeconds(0.4f);
            }
            check = true;
        }
    }
}
