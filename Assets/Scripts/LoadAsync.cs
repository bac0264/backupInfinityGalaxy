using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class LoadAsync : MonoBehaviour
{
    public Image fill;
    public GameObject btnGo;
    public GameObject panel;
    private void Start()
    {
        fill.fillAmount = 0;
    }
    public void LoadingMenu()
    {
        gameObject.SetActive(true);
        StartCoroutine(AsynchronousLoad("SelectPlanet"));
    }
    IEnumerator AsynchronousLoad(string scene)
    {
        btnGo.SetActive(false);
        panel.SetActive(true);
        yield return null;
        Tween fade = panel.GetComponent<Image>().DOColor(new Color(0, 0, 0, 225f / 255), 0.3f);
        yield return fade.WaitForCompletion();
        AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
       // ao.allowSceneActivation = false;

        while (!ao.isDone)
        {
            // [0, 0.9] > [0, 1]
            float progress = Mathf.Clamp01(ao.progress / 0.9f);
            Debug.Log("Loading progress: " + (progress * 100) + "%");
            fill.fillAmount = progress;
            // Loading completed
            yield return null;
        }
    }
}
