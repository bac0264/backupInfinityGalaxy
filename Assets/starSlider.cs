using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class starSlider : MonoBehaviour {
    public Sprite vang, xam;
    int sao = 0;
    public List<GameObject> listSao;
	// Use this for initialization
	void Start () {
        for (int i = 0; i < 3; i++)
        {
            listSao[i].GetComponent<Image>().sprite = xam;
        }
    }
    public void tangsao()
    {
        listSao[sao].GetComponent<Image>().sprite = vang;
        sao++;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
