using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverDialog : MonoBehaviour {

    public GameObject gameOver;
    // Use this for initialization
    void Start () {
		
	}
	
    public void PlayAgain()
    {
        // mấy cái loadscene này chỉnh thêm cái loading nhé
        gameOver.SetActive(false);
        PlayerPrefs.SetInt("IsPlaying", LevelManager.levelSelected);
        LevelManager.levelSelected--;
        if (Fade.instance != null)
        {
            PlayerPrefs.SetInt("ContinueGame", 1);
            Fade.instance.FadeInfc("SelectLevel");

        }
    }
    public void exitclick()
    {
        //load scene cũ
        gameOver.SetActive(false);
        if (Fade.instance != null)
        {
            PlayerPrefs.SetInt("ContinueGame", 1);
            Fade.instance.FadeInfc("SelectLevel");

        }
    }
}
