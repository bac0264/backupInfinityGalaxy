using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public static int levelSelected = 0;
	// Use this for initialization
	void Start () {
        transform.GetChild(levelSelected).gameObject.SetActive(true);
	}
	// Update is called once per frame
	void Update () {
		
	}
}
