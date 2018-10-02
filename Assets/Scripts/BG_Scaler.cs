using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Scaler : MonoBehaviour {
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Vector3 tempScale = transform.localScale;
        float height = sr.bounds.size.y;
        float width = sr.bounds.size.x;
        Debug.Log(height + " " + width);
        float WorldHeight = Camera.main.orthographicSize * 2f;
        float WorldWidth = WorldHeight * Screen.width / Screen.height;
        tempScale.x = WorldWidth / width + WorldWidth / width * 0.01f;
        transform.localScale = tempScale;
    }
}
