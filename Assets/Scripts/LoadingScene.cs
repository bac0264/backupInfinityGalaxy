using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadingScene : MonoBehaviour {
    public static LoadingScene instance;
    public bool _nextScene;
    public bool FadeIn,FadeOut;
    public string sceneName;
    private void Awake()
    {
        if (instance == null) {
            instance = this;
            //DontDestroyOnLoad(this);
        }
      //  else Destroy(gameObject);
    }
    // Use this for initialization
    void Start() {
        //sHOP & RANK
        transform.position = new Vector3(0, 0, 1);
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

    // Update is called once per frame
    //true = fadein, false = fadeout
    void Update () {
        if (_nextScene) {
            Animator ani = gameObject.GetComponent<Animator>();
            if (FadeIn) {
                StartCoroutine(fadein(ani, sceneName.ToString()));
                FadeIn = false;
            }
            if(FadeOut) {
                StartCoroutine(fadeout(ani));
                FadeOut = false;
            }
        }
	}
    IEnumerator fadein(Animator ani, string _sceneName)
    {
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        transform.SetParent(camera.transform);
        ani.Play("FadeIn");
        yield return new WaitForSeconds(0.3f);
      //  transform.SetParent(null);
      //  DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(_sceneName);
       // transform.SetParent(Camera.main.transform);
    }
    IEnumerator fadeout(Animator ani)
    {
        //GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        ani.Play("FadeOut");
        _nextScene = false;
        yield return null;
    }
}
