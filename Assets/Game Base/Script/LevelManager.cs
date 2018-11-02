using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public static int levelSelected = 0;
    public Text txtLv,txtScore;
	// Use this for initialization
	void Start () {
        levelSelected = PlayerPrefs.GetInt("IsPlaying");
        txtLv.text = (levelSelected + 1).ToString();
        transform.GetChild(levelSelected).gameObject.SetActive(true);
	}
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Escape))
        {
            LoadAsync.instance.BacktoSelectLV();
        }
        txtScore.text = GetComponentInChildren<GamePlayManager>().score.ToString();
    }
}
