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
                transform.GetChild(0).gameObject.SetActive(true);
                StartCoroutine(_FadeIn());
            }
            if (FadeOut == true)
            {
                if (im != null)
                {
                    im.DOColor(new Color(0f, 0f, 0f, 1f), 0.1f);
                    StartCoroutine(_FadeOut());
                }
            }
        }
    }
    IEnumerator _FadeOut()
    {
        FadeOut = false;
        im.DOColor(new Color(0f, 0f, 0f, 1f), 0f);
        Tween fadeOut = im.DOColor(new Color(0f, 0f, 0f, 0f), 0.8f);
        yield return fadeOut.WaitForCompletion();
        transform.GetChild(0).gameObject.SetActive(false);
    }
    IEnumerator _FadeIn()
    {
        FadeIn = false;
        Tween fadeIn = im.DOColor(new Color(1f, 1f, 1f, 1f), 0.5f);
        yield return fadeIn.WaitForCompletion();
        SceneManager.LoadScene(sceneName.ToString());
    }
    public void FadeInfc(string _sceneName)
    {
        if (instance != null)
        {
            check = true;
            FadeIn = true;
            sceneName = _sceneName;
        }
    }
    public void FadeOutfc()
    {
        if (instance != null)
        {
            if (check == true)
            {
                FadeOut = true;
                // Fade.instance.check = false;
            }
        }
    }
}
