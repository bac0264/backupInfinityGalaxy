﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ItemHolder : MonoBehaviour {
    public Text id;
    public Text spaceshipName;
    public Image sprite;
    public Text Gold;
    public Text Diamond;
    public Sprite[] Types;
    public Image Marked;
    bool Selected = false;
    Tween select;
    private void Start()
    {
        Selected = false;
    }
    public void Select()
    {
       // Selected = false;
        StartCoroutine(timetoSelect());
    }
    IEnumerator timetoSelect()
    {
        gameObject.GetComponent<Animator>().Play("pickup");
        // yield return new WaitForSeconds(0.05f);
        // SelectComplete();
        yield return null;
    }
    void SelectComplete()
    {
        Selected = false;
    }
    void FixedUpdate()
    {
        if (!Selected)
        {
            float distaneToCamera = ((Vector2)transform.position - (Vector2)Camera.main.transform.position).magnitude*2;
            if (distaneToCamera < Camera.main.orthographicSize)
            {
                float size = 0.8f + 0.5f * (Camera.main.orthographicSize - distaneToCamera) / Camera.main.orthographicSize;
                if (size > 1f) size = 1f;
                transform.localScale = new Vector3(size, size);
            }
            else
            {
                transform.localScale = new Vector3(0.8f, 0.8f);
            }
        }
    }
    public Sprite _types(int index) {
        switch (index)
        {
            case 0: return Types[0];

            case 1: return Types[1];

            case 2: return Types[2];
    
            case 3: return Types[3];
     
            default: break;
        }
        return null; 
    }
}
