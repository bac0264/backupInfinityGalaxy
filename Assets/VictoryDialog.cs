using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryDialog : MonoBehaviour {

    GamePlayManager manager;
    public Text ScoreText;
    public Text LevelTxt;
    public List<Transform> listStar;
	// Use this for initialization
	IEnumerator Start () {
        LevelTxt.text ="Level "+(LevelManager.levelSelected+1).ToString();
        manager = GameObject.FindWithTag("levelManager").transform.GetChild(LevelManager.levelSelected).GetComponent<GamePlayManager>();
        yield return new WaitForSeconds(0.5f);
        for (int i = 30; i >0; i--)
        {
            ScoreText.text = "Score: " + (manager.score - i);
            if (i == 30 && manager.score > manager.oneStar) listStar[0].DOScale(1f, 0.3f).SetEase(Ease.OutBack);
            if (i == 20 && manager.score > manager.twoStar) listStar[1].DOScale(1f, 0.3f).SetEase(Ease.OutBack);
            if (i == 10 && manager.score > manager.threeStar) listStar[2].DOScale(1f, 0.3f).SetEase(Ease.OutBack);
            yield return new WaitForSeconds(0.03f);
        }
	}
    public void playAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void continueBtn()
    {
        LevelManager.levelSelected = (LevelManager.levelSelected + 1) % 15;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
