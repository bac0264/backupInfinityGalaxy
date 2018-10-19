using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScaler : MonoBehaviour {

    float distance = 19.15f;
    // Use this for initialization
    void Start()
    {
        if (Fade.instance != null)
        {
            Fade.instance.FadeOutfc();
        }
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Vector3 tempScale = transform.localScale;
        float height = sr.bounds.size.y;
        float width = sr.bounds.size.x;
        float WorldHeight = Camera.main.orthographicSize * 2f;
        float WorldWidth = WorldHeight * Screen.width / Screen.height;
        tempScale.x = WorldWidth / width + WorldWidth / width*0.01f;
        tempScale.y = tempScale.x;
        transform.localScale = tempScale;
        float _height = sr.bounds.size.y;
        float limit = _height - distance;
        DragCamera.instance.setLimitUp(limit);
    }
    void FixedUpdate()
    {
        if (Input.GetKey("escape"))
        {
            LoadAsync.instance._BackToSelectPlanet();
        }
    }
}
