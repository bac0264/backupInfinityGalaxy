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
    public void _MenuButton()
    {
        //if (Fade.instance != null)
        //{
        //    Fade.instance.check = true;
        //    Fade.instance.FadeIn = true;
        //    Fade.instance.sceneName = "SelectPlanet";
        //}
        Initiate.Fade("SelectPlanet", new Color(0, 0, 0, 1), 4.0f);
        GameObject button = GameObject.FindGameObjectWithTag("ButtonMenu");
        if (button != null)
        {
            button.gameObject.SetActive(false);
        }
        //SceneManager.LoadScene("SelectPlanet");
    }
    public void _BackToMenu()
    {
        //if (Fade.instance != null)
        //{
        //    Fade.instance.check = true;
        //    Fade.instance.FadeIn = true;
        //    Fade.instance.sceneName = "Menu";
        GameObject button = GameObject.FindGameObjectWithTag("Facebook");
        if (button != null) button.SetActive(false);
        Initiate.Fade("Menu", new Color(0,0,0,1), 4.0f);
        if (map != null)
        {
            map.transform.GetChild(0).gameObject.SetActive(false);
            map.transform.GetChild(1).gameObject.SetActive(false);
            StartCoroutine(timetoTransforms());
        }
        // }
    }
    public void _BackToSelectPlanet()
    {
        //if (Fade.instance != null)
        //{
        //    Fade.instance.check = true;
        //    Fade.instance.FadeIn = true;
        //    Fade.instance.sceneName = "SelectPlanet";
        Initiate.Fade("SelectPlanet", new Color(0, 0, 0, 1), 4.0f);
        if (map != null)
        {
            map.transform.GetChild(0).gameObject.SetActive(false);
            map.transform.GetChild(1).gameObject.SetActive(false);
            StartCoroutine(timetoTransforms());
       // }
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
        Initiate.Fade("rank", new Color(0, 0, 0, 1), 4.0f);
        if (map != null) StartCoroutine(timetoTransforms()); ;
    }
    public void CreateBtnClick()
    {
        SceneManager.LoadScene("ListMap");
    }
    public void _shopButton()
    {
        Initiate.Fade("shop", new Color(0, 0, 0, 1), 4.0f);
        if (map != null) StartCoroutine(timetoTransforms()); ;
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
        yield return new WaitForSeconds(0.2f);
        if (map != null)
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
                yield return new WaitForSeconds(0.8f);
            }
            check = false;
        }
        else
        {
            if (setting != null)
            {
                setting.GetComponent<Animator>().Play("BackSetting");
                yield return new WaitForSeconds(0.6f);
            }
            check = true;
        }
    }
}
