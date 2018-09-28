using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScaler : MonoBehaviour {

    // Use this for initialization
    void Awake()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Vector3 tempScale = transform.localScale;
        float height = sr.bounds.size.y;
        float width = sr.bounds.size.x;
        Debug.Log(height + " " + width);
        float WorldHeight = Camera.main.orthographicSize * 2f;
        float WorldWidth = WorldHeight * Screen.width / Screen.height;
        tempScale.x = WorldWidth / width + WorldWidth / width*0.01f;
        tempScale.y = tempScale.x;
        transform.localScale = tempScale;
    }
}
