using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class SelectLevelManager : MonoBehaviour
{
    public GameObject ItemPrefab;
    public Sprite lockImage;
    public Sprite[] unlockImage;
    public static int PlanetID;
    public List<string> listMapId = new List<string>();
    private const int MAX = 60;
    public int IsWinning = 15;
    public Transform[] listContainer;
    GameObject map;
    public GameObject loading;
    // Use this for initializationS
    private void Start()
    {
        Scene getName = SceneManager.GetActiveScene();
        PlayerPrefs.SetString("Scene", getName.name);
        //if (Fade.instance != null)
        //{
        //    if (Fade.instance.check == true)
        //    {
        //        Fade.instance.FadeOut = true;
        //    }
        //}
        map = GameObject.FindGameObjectWithTag("Map");
        if (map.GetComponent<Map>().back)
        {
            Camera.main.transform.position = map.GetComponent<Map>().pos;
            map.GetComponent<Map>().back = false;
        }
        map.GetComponent<Map>()._changeSprite(PlayerPrefs.GetInt("PlayingPlanet"));
        string listidstr = PlayerPrefs.GetString("ListMapId");
        int playerLevel = PlayerPrefs.GetInt("PlayerLevel");

        listMapId.AddRange(listidstr.Split('|'));
        _SelectLvCase(PlanetID, playerLevel);
        //_enableButton(true);
    }
    public void _SelectLvCase(int mapID, int playerLevel)
    {
        int length = MAX * (mapID + 1) / PlayerPrefs.GetInt("LastPlanetID");
        switch (mapID)
        {
            case 0:
                _makeMap(mapID, IsWinning, playerLevel, length);
                break;
            case 1:
                _makeMap(mapID, IsWinning, playerLevel, length);
                break;
            case 2:
                _makeMap(mapID, IsWinning, playerLevel, length);
                break;
            case 3:
                _makeMap(mapID, IsWinning, playerLevel, length);
                break;
            default: break;
        };
    }
    public void _makeMap(int mapID, int isWinning, int playerLevel, int length)
    {
        for (int i = mapID * isWinning; i < length; i++)
        {
            GameObject item = Instantiate(ItemPrefab, listContainer[i % length]);
            int lv = i;
            item.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { LevelClick(loading,item, lv); });
            if (i > playerLevel)
            {
                item.transform.GetChild(0).GetComponent<Image>().sprite = lockImage;
                item.transform.GetChild(0).GetComponent<Button>().enabled = false;
            }
            else
            {
                item.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = (i + 1).ToString();
                item.transform.GetChild(0).GetComponent<Image>().sprite = unlockImage[PlayerPrefs.GetInt("PlayingPlanet") % 2];
                item.transform.GetChild(0).GetComponent<Button>().enabled = true;
            }
        }
    }
    void LevelClick(GameObject _loading, GameObject _item, int lv)
    {
        // LoadAsync.instance.LoadingMainGame();
        _loading.GetComponent<LoadAsync>().LoadingMainGame();
        PlayerPrefs.SetInt("IsPlaying", lv);
        Debug.Log(PlayerPrefs.GetInt("IsPlaying"));
      //  Initiate.Fade("MainGame", new Color(0, 0, 0, 1), 4.0f);
        /*if (map != null)
        {
            map.GetComponent<Map>().pos = Camera.main.transform.position;
            map.GetComponent<Map>().transform.GetChild(0).gameObject.SetActive(false);
            map.GetComponent<Map>().transform.GetChild(1).gameObject.SetActive(false);
            
        }
        if (Fade.instance != null)
        {
            Fade.instance.check = true;
            Fade.instance.FadeIn = true;
            Fade.instance.sceneName = "MainGame";
        }
       // _enableButton(false);*/
    }
    public static void setPlanetID(int x)
    {
        PlanetID = x;
    }
    void _enableButton(bool check) {
        if (check == true)
        {
            for (int i = 0; i < listContainer.Length; i++) {
                if (listContainer[i].childCount > 0)
                {
                    if (listContainer[i].GetChild(0).GetChild(0).GetComponent<Image>().sprite != lockImage)
                    {
                        listContainer[i].GetChild(0).GetChild(0).GetComponent<Button>().enabled = true;
                    }
                }
            }
        }
        else {
            for (int i = 0; i < listContainer.Length; i++)
            {
                if (listContainer[i].childCount > 0)
                    listContainer[i].GetChild(0).GetChild(0).GetComponent<Button>().enabled = false;
            }
        }
    }
}
