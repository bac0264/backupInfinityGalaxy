using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ItemHolder : MonoBehaviour {
    public Text id;
    public Text spaceshipName;
    public Image spriteName;
    public Text Gold;
    bool Selected = false;
    public void Select()
    {
        Selected = true;
        transform.DOScale(1.2f, 0.8f).From().OnComplete(SelectComplete);
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
                Debug.Log(distaneToCamera);
                Debug.Log(Camera.main.orthographicSize);
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
}
