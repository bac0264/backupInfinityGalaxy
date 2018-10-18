using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class ButtonManager : MonoBehaviour
{
    GameObject map;
    GameObject setting;
    bool check;
    private void Awake()
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
    public void _BackingButtonShop()
    {
        if (SaveLoad.instance != null)
        {
            SaveLoad.instance.saving();
        }
        Initiate.Fade(PlayerPrefs.GetString("Scene"),new Color(0, 0, 0, 1), 4.0f);
        if (map != null)
        {
            map.transform.GetChild(0).gameObject.SetActive(false);
            map.transform.GetChild(1).gameObject.SetActive(false);
            StartCoroutine(timetoTransforms());
            // }
        }
    }
    public void _BackingButtonRank()
    {
        Initiate.Fade(PlayerPrefs.GetString("Scene"), new Color(0, 0, 0, 1), 4.0f);
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
        Scene getName = SceneManager.GetActiveScene();
        PlayerPrefs.SetString("Scene", getName.name);
        Initiate.Fade("rank", new Color(0, 0, 0, 1), 4.0f);
        if (map != null) StartCoroutine(timetoTransforms()); ;
    }
    public void CreateBtnClick()
    {
        SceneManager.LoadScene("ListMap");
    }
    public void BacktoSelectLV()
    {
        Initiate.Fade("SelectLevel", new Color(0, 0, 0, 1), 4.0f);
    }
    public void _shopButton()
    {
        Scene getName = SceneManager.GetActiveScene();
        PlayerPrefs.SetString("Scene", getName.name);
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
    public void Back()
    {
        if(PlayerPrefs.GetString("Scene") != SceneManager.GetActiveScene().name)
            Initiate.Fade(PlayerPrefs.GetString("Scene"), new Color(0, 0, 0, 1), 4.0f);
        else Initiate.Fade(PlayerPrefs.GetString("LastScene"), new Color(0, 0, 0, 1), 4.0f);
    }
    IEnumerator TimeToDo()
    {
        GameObject panel = GameObject.FindGameObjectWithTag("Panel");
        GameObject dragCam = GameObject.FindGameObjectWithTag("Drag Camera");
        if (check)
        {

            if (setting != null)
            {
                if (panel != null)
                {
                    panel.transform.GetChild(0).gameObject.SetActive(true);
                    panel.transform.GetChild(0).GetComponent<Image>().DOColor(new Color(0, 0, 0, 225f/255), 0.8f);
                }
                if (dragCam != null)
                {
                    dragCam.GetComponent<DragCamera>().enabled = false;
                }
                if (Camera.main.gameObject.GetComponent<SelectPlanetManager>() != null) {
                    Camera.main.gameObject.GetComponent<SelectPlanetManager>().enabled = false;
                }
                setting.GetComponent<Animator>().Play("Setting");
                yield return new WaitForSeconds(0.8f);
            }
            check = false;
        }
        else
        {
            if (setting != null)
            {
                if (panel != null)
                {
                    Tween fade = panel.transform.GetChild(0).GetComponent<Image>().DOColor(new Color(0, 0, 0, 0), 0.6f);
                    // panel.transform.GetChild(0).localScale = new Vector3(0, 0, 1);
                }
                if (dragCam != null)
                {
                    dragCam.GetComponent<DragCamera>().enabled = true;
                }
                if (Camera.main.gameObject.GetComponent<SelectPlanetManager>() != null)
                {
                    Camera.main.gameObject.GetComponent<SelectPlanetManager>().enabled = true;
                }
                setting.GetComponent<Animator>().Play("BackSetting");
                yield return new WaitForSeconds(0.6f);
                panel.transform.GetChild(0).gameObject.SetActive(false);
            }
            check = true;
        }
    }
}
