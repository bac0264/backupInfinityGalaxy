using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectPlanetManager : MonoBehaviour
{
    bool touching = false, sceneChanging = false;
    public Transform PlanetContainer;
    public Transform SpaceShip;
    public List<Sprite> imageList;
    Vector2 ScrollVelocity;
    Vector2 beginPos;
    int complete;
    public float limitDown;
    public float limitUp;
    public GameObject subPlanet_1;
    public GameObject subPlanet_2;
    public float speed;
    public GameObject[] planets;

    //Vector2 temp = new Vector2();
    private void Awake()
    {
        Scene getName = SceneManager.GetActiveScene();
        PlayerPrefs.SetString("Scene", getName.name);
        if (Map.instance != null) Destroy(Map.instance);
        int i = 0;
        foreach (GameObject _planet in planets)
        {
            i++;
        }
        PlayerPrefs.SetInt("LastPlanetID", i);
        _IsGameStartedForTheFirstTime();
        _setOpenPlanet();
        _setSpaceshipPosition();
        _setNameScene();
      // PlayerPrefs.SetInt("IsGameStartedForTheFirstTime");
    }
    private void Start()
    {
        //if (Fade.instance != null) {
        //    if(Fade.instance.check == true)
        //    {
        //        Fade.instance.FadeOut = true;
        //       // Fade.instance.check = false;
        //    }
        //}
    }
    void _IsGameStartedForTheFirstTime()
    {

       if (!PlayerPrefs.HasKey("IsGameStartedForTheFirstTime"))
        {
            PlayerPrefs.SetInt("PlayingPlanet", -1);
            PlayerPrefs.SetInt("CompleteLastPlanet", 0);
            PlayerPrefs.SetInt("IsGameStartedForTheFirstTime", 0);
            PlayerPrefs.SetInt("PlayerLevel", 0);
            PlayerPrefs.SetInt("Spaceship",1);
        }

    }
    void _setSpaceshipPosition()
    {
        if (PlayerPrefs.GetInt("PlayingPlanet", -1) >= 0)
        {
            Camera.main.transform.position = new Vector3(PlanetContainer.GetChild(PlayerPrefs.GetInt("PlayingPlanet")).position.x, PlanetContainer.GetChild(PlayerPrefs.GetInt("PlayingPlanet")).position.y, -10f);
            SpaceShip.SetParent(PlanetContainer.GetChild(PlayerPrefs.GetInt("PlayingPlanet")).GetComponent<_Planet>().SpaceShipPosition);
            SpaceShip.localPosition = new Vector3();
            SpaceShip.localEulerAngles = new Vector3();
            SpaceShip.localScale = new Vector3(1, 1, 1);
            SpaceShip.DOScale(0.5f, 0.5f).SetEase(Ease.OutElastic).From();
        }
    }
    void _setOpenPlanet()
    {
        //PlayerPrefs.SetInt("CompleteLastPlanet", 3);
        complete = PlayerPrefs.GetInt("CompleteLastPlanet");
        PlanetContainer.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = imageList[0];
        for (int i = 1; i < PlanetContainer.childCount; i++)
        {
            if (i <= complete)
            {
                PlanetContainer.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
                PlanetContainer.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = imageList[i];
                //PlanetContainer.transform.GetChild(i).GetComponent<Collider2D>().enabled = true;
            }
            else
            {
                PlanetContainer.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
                PlanetContainer.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = imageList[i + 3];
                //PlanetContainer.transform.GetChild(i).GetComponent<Collider2D>().enabled = false;
            }
        }
    }
    void _setNameScene(){
        PlayerPrefs.SetString("SelectLevel", "SelectLevel");
        PlayerPrefs.SetString("SelectPlanet", "SelectPlanet");
        PlayerPrefs.SetString("Menu", "Menu");
    }
    void touchBegin(Vector2 screenPosition)
    {
        beginPos = Camera.main.ScreenToWorldPoint(screenPosition);
    }
    void touchHold(Vector2 screenPosition)
    {
        ScrollVelocity = Camera.main.ScreenToWorldPoint(screenPosition);
        ScrollVelocity = ScrollVelocity - beginPos;
    }
    void touchEnd(Vector2 screenPosition)
    {
        //CheckClick
        if ((screenPosition - (Vector2)Camera.main.WorldToScreenPoint(beginPos)).magnitude < 50)
        {
            GameObject selectPlanet = ObjectClicked(screenPosition);
            if (selectPlanet != null)
            {
                selectPlanet.GetComponent<_Planet>().Select();
                sceneChanging = true;
                StartCoroutine(ChangeToScene(selectPlanet.GetComponent<_Planet>().id, selectPlanet));
            }
        }
        else
        {

        }
    }
    IEnumerator ChangeToScene(int id, GameObject selectPlanet)
    {
        //Khacs manf ddang chon
        yield return new WaitForSeconds(0.2f);
        if (id != PlayerPrefs.GetInt("PlayingPlanet", -1))
        {
            //Lần đầu vào game
            if (PlayerPrefs.GetInt("PlayingPlanet", -1) == -1)
            {
                Tween resetCamera = Camera.main.transform.DOMove(new Vector3(SpaceShip.position.x,
                                                                             SpaceShip.position.y + Camera.main.orthographicSize / 2,
                                                                            -10f), 1.5f).SetEase(Ease.InOutExpo);
                yield return resetCamera.WaitForCompletion();
                Tween apear = SpaceShip.DOScale(1f, 1f).SetEase(Ease.OutElastic);
                yield return apear.WaitForCompletion();
            }
            else
            {
                Tween resetCamera = Camera.main.transform.DOMove(new Vector3(PlanetContainer.GetChild(PlayerPrefs.GetInt("PlayingPlanet")).position.x,
                                                                         PlanetContainer.GetChild(PlayerPrefs.GetInt("PlayingPlanet")).position.y,
                                                                         -10f), 1.5f).SetEase(Ease.InOutExpo);
                yield return resetCamera.WaitForCompletion();
                Tween prepare = SpaceShip.DORotate(new Vector3(0, 0, Vector2.SignedAngle(Vector2.up, PlanetContainer.GetChild(id).position - SpaceShip.position)), 0.6f);
                SpaceShip.DOLocalMove(new Vector3(0, 2f, 0), 0.6f);
                yield return prepare.WaitForCompletion();
            }
            SpaceShip.SetParent(null);
            SpaceShip.GetComponentInChildren<Animator>().SetBool("Flying", true);
            Tween moveCamera = Camera.main.transform.DOMove(new Vector3(PlanetContainer.GetChild(id).position.x,
                                                                         PlanetContainer.GetChild(id).position.y,
                                                                         -10f), 1.8f).SetEase(Ease.InOutBack);
            SpaceShip.DOMove(new Vector3(PlanetContainer.GetChild(id).position.x,
                                         PlanetContainer.GetChild(id).position.y,
                                         0), 1.5f).SetEase(Ease.InOutCubic);
            yield return new WaitForSeconds(1.7f);
            SpaceShip.GetComponentInChildren<Animator>().SetBool("Flying", false);
            yield return moveCamera.WaitForCompletion();
        }
        else
        {
            Tween resetCamera = Camera.main.transform.DOMove(new Vector3(PlanetContainer.GetChild(id).position.x,
                                                                        PlanetContainer.GetChild(id).position.y,
                                                                        -10f), 2f).SetEase(Ease.InOutExpo);
            yield return resetCamera.WaitForCompletion();
        }
        PlayerPrefs.SetInt("PlayingPlanet", id);
        if (id <= complete)
        {
            SelectLevelManager.setPlanetID(id);
            //Initiate.Fade("SelectLevel", new Color(1, 1, 1), 15.0f);
            //if (Fade.instance != null)
          //  {
                GameObject map = GameObject.FindGameObjectWithTag("Map");
                map.transform.GetChild(0).gameObject.SetActive(false);
            // Fade.instance.sceneName = PlayerPrefs.GetString("SelectLevel");
            // Fade.instance.check = true;
            //Fade.instance.FadeIn = true;
            // }
            Initiate.Fade("SelectLevel", new Color(0, 0, 0, 1), 4.0f);
        }
        else
        {
            _setSpaceshipPosition();
            sceneChanging = false;
        }
    }

    GameObject ObjectClicked(Vector2 screenPosition)
    {
        //Converting Mouse Pos to 2D (vector2) World Pos
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPosition);
        Vector2 rayPos = new Vector2(worldPos.x, worldPos.y);
        RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);
        if (hit)
        {
            return hit.transform.gameObject;
        }
        else return null;
    }

    private void FixedUpdate()
    {
        if (!sceneChanging)
        {
            if (touching)
            {
                touchHold(Input.mousePosition);
            }
            if (Input.GetMouseButtonDown(0) && !touching)
            {
                touching = true;
                touchBegin(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                touching = false;
                touchEnd(Input.mousePosition);
            }
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    touching = true;
                    touchBegin(Input.GetTouch(0).position);
                }
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    touchHold(Input.GetTouch(0).position);
                }
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    touching = false;
                    touchEnd(Input.GetTouch(0).position);
                }
            }

            //Camera di chuyển theo phương X
            if (PlanetContainer != null)
            {
                for (int i = 0; i < PlanetContainer.childCount - 1; i++)
                {
                    if (Camera.main.transform.position.y > PlanetContainer.GetChild(i).position.y && Camera.main.transform.position.y < PlanetContainer.GetChild(i + 1).position.y)
                    {
                        float ratio = (Camera.main.transform.position.y - PlanetContainer.GetChild(i).position.y) / (PlanetContainer.GetChild(i + 1).position.y - PlanetContainer.GetChild(i).position.y);
                        if (DOTween.TweensById("MoveX", true) != null)
                        {
                            foreach (Tween tween in DOTween.TweensById("MoveX", true))
                            {
                                tween.Kill();
                            }
                        }
                        Camera.main.transform.DOMoveX(PlanetContainer.GetChild(i).position.x + ratio * (PlanetContainer.GetChild(i + 1).position.x - PlanetContainer.GetChild(i).position.x), 0.2f).SetId("MoveX");
                        break;
                    }
                }
                // Camera di chuyển theo phuong Y
                if (Camera.main.transform.position.y - ScrollVelocity.y >= limitDown && Camera.main.transform.position.y - ScrollVelocity.y <= limitUp)
                {
                    if (DOTween.TweensById("MoveY", true) != null)
                    {
                        foreach (Tween tween in DOTween.TweensById("MoveY", true))
                        {
                            tween.Kill();
                        }
                    }
                    Camera.main.transform.DOMoveY((Camera.main.transform.position.y - ScrollVelocity.y), 0.1f).SetId("MoveY");
                    if (!touching)
                        ScrollVelocity = Vector2.Lerp(ScrollVelocity, Vector2.zero, 0.125f);
                }
                else if (Camera.main.transform.position.y - ScrollVelocity.y < limitDown)
                {
                    Camera.main.transform.DOMoveY(limitDown, 0.1f);
                }
                else
                {
                    Camera.main.transform.DOMoveY(limitUp, 0.1f);
                }
            }
        }
    }
}
