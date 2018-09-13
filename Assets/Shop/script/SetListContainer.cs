using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetListContainer : MonoBehaviour {

	// Use this for initialization
	void Update () {
        float y = gameObject.transform.position.y;
        if (y < 3.463f)
        {
            y = 3.463f;
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, y);
        }
        else if (y > 43.463f)
        {
            y = 43.463f;
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, y);
        }
    }
}
