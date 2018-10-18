using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public static int levelSelected = 0;
    
	// Use this for initialization
	void Start () {
        levelSelected = PlayerPrefs.GetInt("IsPlaying");
        transform.GetChild(levelSelected).gameObject.SetActive(true);
	}
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("escape"))
        {
            LoadAsync.instance.BacktoSelectLV();
        }
    }
}
