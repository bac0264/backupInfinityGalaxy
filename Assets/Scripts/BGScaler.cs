using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScaler : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Vector3 tempScale = transform.localScale;
        float height = sr.bounds.size.y;
        float width = sr.bounds.size.x;
        float WorldHeight = Camera.main.orthographicSize * 2f;
        float WorldWidth = WorldHeight * Screen.width / Screen.height;
        tempScale.y = WorldHeight / height;
        tempScale.x = WorldWidth / width;
        transform.localScale = tempScale;
    }
}
