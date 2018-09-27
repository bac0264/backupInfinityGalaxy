using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipList : MonoBehaviour {
    public List<Sprite> head = new List<Sprite>();
    public List<Sprite> tail = new List<Sprite>();
    private void Start()
    {
        updateSS();
    }
    void updateSS()
    {
        Debug.Log("Update SS");
        int i = PlayerPrefs.GetInt("Spaceship") - 1 ;
        Debug.Log("index:" + i);
        transform.GetChild(0).GetComponent <SpriteRenderer>().sprite = head[i];
        transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = tail[i%3];
    }
}
