using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class MissionPanel : MonoBehaviour
{
    public static MissionPanel instance;
    public GameObject loading;
    public GameObject panel;
    public Text text;

    public void isExit()
    {
        StartCoroutine(loadAnimationBtnExit());
    }
    
    public void Play()
    {
        StartCoroutine(loadAnimationBtnPlay());
    }
    // Use this for initialization
    void Start()
    {
        if (instance == null) instance = this;
        text.text = (PlayerPrefs.GetInt("IsPlaying")+1).ToString();
        //gameObject.GetComponent<Animator>().SetBool("In", false);
        //if (panel != null)
        //{
        //    panel.SetActive(true);
        //    panel.GetComponent<Image>().DOColor(new Color(0, 0, 0, 225f / 255), 0.8f);
        //}
        StartCoroutine(setUp());
    }
    IEnumerator setUp()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<Animator>().SetBool("In", false);
        if (panel != null)
        {
            panel.SetActive(true);
            panel.GetComponent<Image>().DOColor(new Color(0, 0, 0, 225f / 255), 0.8f);
        }
    }
    IEnumerator loadAnimationBtnPlay()
    {
        // gameObject.transform.GetChild(1).GetChild(1).GetComponent<Animator>().Play("ScaleButton");
        if (panel != null)
        {
            panel.GetComponent<Image>().DOColor(new Color(0, 0, 0, 0), 0.5f);
        }
        yield return new WaitForSeconds(0.2f);
        Animator ani = gameObject.GetComponent<Animator>();
        ani.Play("out");
        yield return new WaitForSeconds(0.7f);
        loading.GetComponent<LoadAsync>().LoadingMainGame();
    }
    IEnumerator loadAnimationBtnExit()
    {
        // gameObject.transform.GetChild(1).GetChild(1).GetComponent<Animator>().Play("ScaleButton");
        GameObject map = GameObject.FindGameObjectWithTag("Map");
        if (panel != null)
        {
            panel.GetComponent<Image>().DOColor(new Color(0, 0, 0, 0), 0.5f);
        }
        yield return new WaitForSeconds(0.2f);
        Animator ani = gameObject.GetComponent<Animator>();
        ani.Play("out");
        yield return new WaitForSeconds(0.6f);
        if (map != null)
        {
            map.transform.GetChild(0).gameObject.SetActive(true);
            map.transform.GetChild(1).gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
