using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadAsync : MonoBehaviour
{
    public Slider slider;
    public void LoadingMenu()
    {
        StartCoroutine(AsynchronousLoad("SelectPlanet"));
    }
    IEnumerator AsynchronousLoad(string scene)
    {
        yield return null;

        AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
       // ao.allowSceneActivation = false;

        while (!ao.isDone)
        {
            // [0, 0.9] > [0, 1]
            float progress = Mathf.Clamp01(ao.progress / 0.9f);
            Debug.Log("Loading progress: " + (progress * 100) + "%");
            slider.value = progress;
            // Loading completed
            //if (ao.progress == 1f)
            //{
            //    ao.allowSceneActivation = true;
            //}
            yield return null;
        }
    }
}
