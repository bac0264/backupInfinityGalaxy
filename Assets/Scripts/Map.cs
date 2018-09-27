using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {
    public static Map instance;
    public List<Sprite> spriteList;
    public Vector3 pos;
    public bool back = false;
    void Start () {
        if (instance == null)
        {
            instance = this;    
            //DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
	}
    public void _changeSprite(int _id)
    {
        transform.GetChild(4).GetComponent<SpriteRenderer>().sprite = spriteList[_id];
    }

}
