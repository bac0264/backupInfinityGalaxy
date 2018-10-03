using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipList : MonoBehaviour {
    public List<GameObject> ss = new List<GameObject>();
    private void Awake()
    {
        updateSS();
    }
    void updateSS()
    {
        for(int j = 0; j < transform.childCount; j++)
        {
            Destroy(transform.GetChild(j));
        }
        int i = PlayerPrefs.GetInt("Spaceship") - 1 ;
        Debug.Log(i);
        Instantiate(ss[i], gameObject.transform);
    }
}
