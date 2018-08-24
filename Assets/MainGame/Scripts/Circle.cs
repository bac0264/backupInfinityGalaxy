using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour {
    public Vector2 Pos;
    public List<Sprite> listSprite;
    private void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = listSprite[Random.Range(0, listSprite.Count)];
    }


}
