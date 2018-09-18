using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetListContainer : MonoBehaviour {

	// Use this for initialization
	void Update () {
        float y = gameObject.transform.position.y;
        if (y <= 9.5f)
        {
            y = 9.5f;
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, y);
        }
        else if (y > 48)
        {
            y = 48f;
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, y);
        }
    }
}
