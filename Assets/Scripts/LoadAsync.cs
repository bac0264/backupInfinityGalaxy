using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class LoadAsync : MonoBehaviour
{
    public static LoadAsync instance;
    public GameObject loadingText;
   // public Text loading;
    public GameObject btnGo;
    public GameObject panel;
    GameObject map;
    GameObject setting;
    bool check;
    private void Awake()
    {
        if (instance == null) instance = this;
        map = GameObject.FindGameObjectWithTag("Map");
        setting = GameObject.Find("Setting");
        check = true;
    }
    public void _MenuButton()
    {
        //gameObject.SetActive(true);
        ////if (Fade.instance != null)
        ////{
        ////    Fade.instance.check = true;
        ////    Fade.instance.FadeIn = true;
        ////    Fade.instance.sceneName = "SelectPlanet";
        ////}
        //Initiate.Fade("SelectPlanet", new Color(0, 0, 0, 1), 4.0f);
        //GameObject button = GameObject.FindGameObjectWithTag("ButtonMenu");
        //if (button != null)
        //{
        //    button.gameObject.SetActive(false);
        //}
        ////SceneManager.LoadScene("SelectPlanet");
    }
    public void _BackToMenu()
    {
        GameObject button = GameObject.FindGameObjectWithTag("Facebook");
        if (button != null) button.SetActive(false);

        gameObject.SetActive(true);
        StartCoroutine(AsynchronousLoad("Menu"));
        if (map != null)
        {
            map.transform.GetChild(0).gameObject.SetActive(false);
            map.transform.GetChild(1).gameObject.SetActive(false);
            StartCoroutine(timetoTransforms());
        }
    }
    public void _BackToSelectPlanet()
    {
        gameObject.SetActive(true);
        StartCoroutine(AsynchronousLoad("SelectPlanet"));
        if (map != null)
        {
            map.transform.GetChild(0).gameObject.SetActive(false);
            map.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    public void _BackingButtonShop()
    {
        if (SaveLoad.instance != null)
        {
            SaveLoad.instance.saving();
        }
        gameObject.SetActive(true);
        StartCoroutine(AsynchronousLoad(PlayerPrefs.GetString("Scene")));
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
        gameObject.SetActive(true);
        StartCoroutine(AsynchronousLoad(PlayerPrefs.GetString("Scene")));
        if (map != null)
        {
            map.transform.GetChild(0).gameObject.SetActive(false);
            map.transform.GetChild(1).gameObject.SetActive(false);
            StartCoroutine(timetoTransforms());
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
        StartCoroutine(timetoRunAni("rank"));
    }
    public void CreateBtnClick()
    {
        SceneManager.LoadScene("ListMap");
    }
    public void BacktoSelectLV()
    {
        gameObject.SetActive(true);
        StartCoroutine(AsynchronousLoad("SelectLevel"));
    }
    public void _shopButton()
    {
        StartCoroutine(timetoRunAni("shop"));
    }

    public void _SettingClicked()
    {
        StartCoroutine(TimeToDo());
    }
    IEnumerator timetoRunAni(string scene)
    {
        if (setting != null) setting.GetComponent<Animator>().Play("BackSetting");
        yield return new WaitForSeconds(0.25f);
        GameObject ParentSetting = GameObject.FindGameObjectWithTag("ParentSetting");
        if (ParentSetting != null) ParentSetting.SetActive(false);
        Scene getName = SceneManager.GetActiveScene();
        PlayerPrefs.SetString("Scene", getName.name);
        gameObject.SetActive(true);
        StartCoroutine(AsynchronousLoad(scene));
        if (map != null) StartCoroutine(timetoTransforms()); ;
    }
    IEnumerator timetoTransforms()
    {
        yield return new WaitForSeconds(0.2f);
        if (map != null)
            Destroy(map);

    }
    public void Back()
    {
        gameObject.SetActive(true);
        if (PlayerPrefs.GetString("Scene") != SceneManager.GetActiveScene().name)
            StartCoroutine(AsynchronousLoad(PlayerPrefs.GetString("Scene")));
        else
            StartCoroutine(AsynchronousLoad(PlayerPrefs.GetString(PlayerPrefs.GetString("LastScene"))));
        GameObject ParentSetting = GameObject.FindGameObjectWithTag("ParentSetting");
        if (ParentSetting != null) ParentSetting.SetActive(false);
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
                    panel.transform.GetChild(0).GetComponent<Image>().DOColor(new Color(0, 0, 0, 225f / 255), 0.8f);
                }
                if (dragCam != null)
                {
                    dragCam.GetComponent<DragCamera>().enabled = false;
                }
                if (Camera.main.gameObject.GetComponent<SelectPlanetManager>() != null)
                {
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
    public void LoadingMenu()
    {
        gameObject.SetActive(true);
        StartCoroutine(AsynchronousLoad("SelectPlanet"));
    }
    public void LoadingMainGame()
    {
        gameObject.SetActive(true);
        StartCoroutine(AsynchronousLoad("MainGame"));
    }
    IEnumerator AsynchronousLoad(string scene)
    {
        if(btnGo != null) btnGo.SetActive(false);
        panel.SetActive(true);
        loadingText.SetActive(true);
        yield return null;
        Tween fade = panel.GetComponent<Image>().DOColor(new Color(0, 0, 0, 225f / 255), 0.3f);
        yield return fade.WaitForCompletion();
        AsyncOperation ao = SceneManager.LoadSceneAsync(scene);

        while (!ao.isDone)
        {
            float progress = Mathf.Clamp01(ao.progress / 0.9f);
            Debug.Log("Loading progress: " + (progress * 100) + "%");
            yield return null;
        }
    }
    public void Loading(string scene)
    {
        gameObject.SetActive(true);
        StartCoroutine(AsynchronousLoad(scene));
    }
    public void _nextScene(int id, int complete, ref bool sceneChanging)
    {
        if (id <= complete)
        {
            SelectLevelManager.setPlanetID(id);
            GameObject map = GameObject.FindGameObjectWithTag("Map");
            map.transform.GetChild(0).gameObject.SetActive(false);
            StartCoroutine(AsynchronousLoad("SelectLevel"));
        }
        else
        {
            SelectPlanetManager.instance._setSpaceshipPosition();
            sceneChanging = false;
        }
    }
}
