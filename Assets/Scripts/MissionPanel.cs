using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class MissionPanel : MonoBehaviour
{
    public GameObject loading;
    public GameObject panel;
    public void isExit()
    {
        StartCoroutine(_loadAnimation());
    }
    
    public void Play()
    {
        StartCoroutine(loadAnimation());
    }
    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<Animator>().Play("In");
        gameObject.GetComponent<Animator>().SetBool("In", false);
        if (panel != null)
        {
            panel.SetActive(true);
            panel.GetComponent<Image>().DOColor(new Color(0, 0, 0, 225f / 255), 0.8f);
        }
    }

    IEnumerator loadAnimation()
    {
        // gameObject.transform.GetChild(1).GetChild(1).GetComponent<Animator>().Play("ScaleButton");
        if (panel != null)
        {
            panel.GetComponent<Image>().DOColor(new Color(0, 0, 0, 0), 0.5f);
        }
        yield return new WaitForSeconds(0.2f);
        Animator ani = gameObject.GetComponent<Animator>();
        ani.Play("out");
        //yield return new WaitForSeconds(0.5f);
        loading.GetComponent<LoadAsync>().LoadingMainGame();
    }
    IEnumerator _loadAnimation()
    {
        // gameObject.transform.GetChild(1).GetChild(1).GetComponent<Animator>().Play("ScaleButton");
        if (panel != null)
        {
            panel.GetComponent<Image>().DOColor(new Color(0, 0, 0, 0), 0.5f);
        }
        yield return new WaitForSeconds(0.2f);
        Animator ani = gameObject.GetComponent<Animator>();
        ani.Play("out");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
