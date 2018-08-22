using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class Fade : MonoBehaviour {
    public static Fade instance;
    public bool FadeIn;
    public bool FadeOut;
    public bool check;
    public Image im;
    public string sceneName;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
        im = gameObject.transform.GetChild(0).GetComponent<Image>();
    }
    private void Update()
    {
        if (check == true)
        {
            if (FadeIn == true)
            {
                StartCoroutine(_FadeIn());
                FadeIn = false;
            }
            if (FadeOut == true)
            {
                if (im != null)
                {
                    im.DOColor(new Color(0f, 0f, 0f, 1f), 0.1f);
                    StartCoroutine(_FadeOut());
                }
                FadeOut = false;
            }
        }
    }
    IEnumerator _FadeOut()
    {
        im.DOColor(new Color(0f, 0f, 0f, 1f), 0f);
        Tween FadeOut = im.DOColor(new Color(0f, 0f, 0f, 0f), 0.5f);
        yield return FadeOut.WaitForCompletion();
        //SceneManager.LoadScene(sceneName);

        //gameObject.SetActive(false);
    }
    IEnumerator _FadeIn()
    {
        Debug.Log("run");
        Tween FadeIn = im.DOColor(new Color(0f, 0f, 0f, 1f), 0.5f);
        yield return FadeIn.WaitForCompletion();
        SceneManager.LoadScene(sceneName.ToString());
    }
}
