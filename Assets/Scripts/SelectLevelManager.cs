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
    // Use this for initializationS
    private void Start()
    {
        if (Fade.instance != null)
        {
            Debug.Log(Fade.instance.check);
            if (Fade.instance.check == true)
            {
                Fade.instance.FadeOut = true;
               // Fade.instance.check = false;
            }
        }
        map = GameObject.FindGameObjectWithTag("Map");
        if (map.GetComponent<Map>().back)
        {
            Camera.main.transform.position = map.GetComponent<Map>().pos;
            map.GetComponent<Map>().back = false;
        }
        map.GetComponent<Map>()._changeSprite(PlayerPrefs.GetInt("PlayingPlanet"));
        //  Debug.Log(PlayerPrefs.GetInt("PlayingPlanet"));
        string listidstr = PlayerPrefs.GetString("ListMapId");
        int playerLevel = PlayerPrefs.GetInt("PlayerLevel");

        // if (listidstr != "")
        // {
        listMapId.AddRange(listidstr.Split('|'));
        _SelectLvCase(PlanetID, playerLevel);
        // }
    }
    public void _SelectLvCase(int mapID, int playerLevel)
    {
        Debug.Log(PlayerPrefs.GetInt("LastPlanetID"));
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
            // Debug.Log("LoadData"+ i );
            GameObject item = Instantiate(ItemPrefab, listContainer[i % length]);
            int lv = i;
            item.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { LevelClick(item, lv); });
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
    void LevelClick(GameObject obj, int lv)
    {
        if (map != null)
        {
            map.GetComponent<Map>().pos = Camera.main.transform.position;
            map.GetComponent<Map>().transform.GetChild(0).gameObject.SetActive(false);
            map.GetComponent<Map>().transform.GetChild(1).gameObject.SetActive(false);
        }
        SceneManager.LoadScene("_MainGame");
    }
    public static void setPlanetID(int x)
    {
        PlanetID = x;
    }
}
