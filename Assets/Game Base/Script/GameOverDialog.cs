using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverDialog : MonoBehaviour {

    
	// Use this for initialization
	void Start () {
		
	}
	
    public void PlayAgain()
    {
        // mấy cái loadscene này chỉnh thêm cái loading nhé
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void exitclick()
    {
        //load scene cũ
        SceneManager.LoadScene("SelectLevel");
        
    }
}
