using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {
    public static Map instance;
    public List<GameObject> mapList ;
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
    public void _changeMap(int _id)
    {
        //transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = spriteList[_id];
        mapList[_id].SetActive(true);
        transform.GetChild(3).GetChild(0).SetParent(mapList[_id].transform);
    }

}
