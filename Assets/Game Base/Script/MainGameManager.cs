﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameManager : MonoBehaviour {

	public void BackClick()
    {
        SceneManager.LoadScene("SelectLevel");
    }
}
