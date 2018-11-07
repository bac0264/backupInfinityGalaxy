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
    public Image im_2;
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
        im_2 = gameObject.transform.GetChild(1).GetComponent<Image>();
    }
    private void Update()
    {
        if (check == true)
        {
            if (FadeIn == true)
            {
                StartCoroutine(_FadeIn());
            }
            if (FadeOut == true)
            {
                if (im != null)
                {
                    StartCoroutine(_FadeOut());
                }
            }
        }
    }
    IEnumerator _FadeOut()
    {
        FadeOut = false;
        im.DOColor(new Color(0f, 0f, 0f, 1f), 0.5f);
        Tween fadeIn = im_2.DOColor(new Color(0, 0, 0, 1), 0.7f);
        yield return fadeIn.WaitForCompletion();
        transform.GetChild(1).gameObject.SetActive(false);
        Tween fadeOut = im.DOColor(new Color(0f, 0f, 0f, 0f), 0.7f);
        yield return fadeOut.WaitForCompletion();
        transform.GetChild(0).gameObject.SetActive(false);
    }
    IEnumerator _FadeIn()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        FadeIn = false;
        Tween fadeIn = im.DOColor(new Color(0f, 0f, 0f, 1f), 0.7f);
        yield return fadeIn.WaitForCompletion();
        transform.GetChild(1).gameObject.SetActive(true);
        Tween fadeOut = im_2.DOColor(new Color(1f, 1f, 1f, 1f), 0.7f);
        yield return fadeOut.WaitForCompletion();
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
