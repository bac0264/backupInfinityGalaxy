﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public class SelectLevelManager : MonoBehaviour
{
    [Serializable]
    public class Position
    {
        public Vector3 pos;
        public int index;

        public Position(Vector3 pos, int index)
        {
            this.pos = pos;
            this.index = index;
        }
        public Position()
        {
        }
    }
    [Serializable]
    public class SavePosition
    {
        private List<Position> posList;

        public void setPos(List<Position> _posList)
        {
            posList = _posList;
        }
        public List<Position> getPos()
        {
            return posList;
        }
    }
    public GameObject MissionPopup;
    public GameObject ItemPrefab;
    public Sprite lockImage;
    public Sprite[] unlockImage;
    public static int PlanetID;
    public List<string> listMapId = new List<string>();
    public List<GameObject> itemList = new List<GameObject>();
    private const int MAX = 60;
    public int IsWinning = 15;
    public Transform[] listContainer;
    GameObject map;
    public GameObject Glow;
    public Transform listCloud;
    public GameObject loading;
    public List<Position> posCopy;
    const float temp = 7.7f;
    // Use this for initializationS
    private void Awake()
    {
        OpenCloud();
        Scene getName = SceneManager.GetActiveScene();
        PlayerPrefs.SetString("Scene", getName.name);
        PlayerPrefs.SetString("LastScene", getName.name);
        map = GameObject.FindGameObjectWithTag("Map");
        if (map.GetComponent<Map>().back)
        {
            Camera.main.transform.position = map.GetComponent<Map>().pos;
            map.GetComponent<Map>().back = false;
        }
        if (map != null)
            map.GetComponent<Map>()._changeMap(PlayerPrefs.GetInt("PlayingPlanet"));
        string listidstr = PlayerPrefs.GetString("ListMapId");
        int playerLevel = PlayerPrefs.GetInt("PlayerLevel");

        listMapId.AddRange(listidstr.Split('|'));
        _SelectLvCase(PlanetID, playerLevel);
    }
    public void _saving()
    {
        try
        {
            SavePosition saveData = new SavePosition();
            // do something  
            saveData.getPos().Clear();
            saveData.setPos(posCopy);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(Application.persistentDataPath + "/Position.txt", FileMode.OpenOrCreate);
            bf.Serialize(fs, saveData);
            fs.Close();
        }
        catch (Exception e)
        {
            print(e);
        }
        print("saved data to " + Application.persistentDataPath + "/Position.txt");
    }
    public void _Loading()
    {
        if (File.Exists(Application.persistentDataPath + "/Position.txt"))
        {
            try
            {
                SavePosition saveData = new SavePosition();
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = new FileStream(Application.persistentDataPath + "/Position.txt", FileMode.Open);
                saveData = (SavePosition)bf.Deserialize(fs);
                fs.Close();
                posCopy = saveData.getPos();
                // do somthing
            }
            catch (Exception e)
            {
                print(e);
            }
        }
    }
    // tìm vị trí đang chơi khi từ selectplanet vào
    public void findPos()
    {

        bool check = false;
        for (int i = 0; i < posCopy.Count; i++)
        {
            if (posCopy[i].index == PlayerPrefs.GetInt("IsPlaying"))
            {
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x,
                    posCopy[i].pos.y + temp, Camera.main.transform.position.z);
                Glow.transform.SetParent(itemList[i].transform);
                // Glow.transform.position = Vector3.zero;
                check = true;
                break;
            }
        }
        if (!check)
        {
            Debug.Log("Dont find");
            Debug.Log("IsPlaying: " + PlayerPrefs.GetInt("IsPlaying"));
        }
    }
    // Tìm vị trí khi continue game
    public void _findPos()
    {
        StartCoroutine(timetoFindPos());
    }
    IEnumerator timetoFindPos()
    {
        bool check = false;
        for (int i = 0; i < posCopy.Count; i++)
        {
            if (posCopy[i].index == PlayerPrefs.GetInt("IsPlaying") && i != 0)
            {
                if (posCopy[i].pos.y + temp >= DragCamera.instance.limitUp)
                {
                    Camera.main.transform.position = new Vector3(Camera.main.transform.position.x,
                    posCopy[i].pos.y + temp, Camera.main.transform.position.z);
                    yield return new WaitForSeconds(0.8f);
                    Instantiate(MissionPopup);
                    // Glow.transform.SetParent(itemList[i].transform);
                }
                else
                {
                    Camera.main.transform.position = new Vector3(Camera.main.transform.position.x,
    posCopy[i - 1].pos.y + temp, Camera.main.transform.position.z);
                    Tween move = Camera.main.transform.DOMoveY(posCopy[i].pos.y + temp, 0.8f);
                    yield return move.WaitForCompletion();
                    Instantiate(MissionPopup);
                    // PlayerPrefs.SetInt("IsPlaying", PlayerPrefs.GetInt("IsPlaying") + 1);

                    // Glow.transform.SetParent(itemList[i].transform);
                }
                check = true;
                break;
            }
            else if (posCopy[i].index == PlayerPrefs.GetInt("IsPlaying") && i == 0)
            {
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x,
                    posCopy[i].pos.y + temp, Camera.main.transform.position.z);
            }
        }
        if (!check)
        {
            Debug.Log("Dont find");
            Debug.Log("IsPlaying: " + PlayerPrefs.GetInt("IsPlaying"));
        }
    }
    public void OpenCloud()
    {
        StartCoroutine(cloudAni());
    }
    IEnumerator cloudAni()
    {
        int playerLevel = PlayerPrefs.GetInt("PlayerLevel");
        int div = (playerLevel % IsWinning) / 2;
        int div_2 = playerLevel % IsWinning;
        int temp = 3;
        int temp_2 = 6;
        GameObject _listCloud = GameObject.FindGameObjectWithTag("cloud");
        if (PlayerPrefs.GetInt("PlayingPlanet") == PlayerPrefs.GetInt("CompleteLastPlanet"))
        {
            int cloudOpened = PlayerPrefs.GetInt("CloudOpened");
            if (cloudOpened != 0)
            {
                for (int i = 0; i < cloudOpened; i++)
                {
                    // if (playerLevel % IsWinning != 0)
                    //{
                    _listCloud.transform.GetChild(i).gameObject.SetActive(false);
                    //}
                }
            }
            if (div_2 < temp_2)
            {
                for (int i = 0; i < temp; i++)
                {
                    // if (playerLevel % IsWinning != 0)
                    //{
                    _listCloud.transform.GetChild(i).GetComponent<Animator>().Play("out");
                    yield return new WaitForSeconds(0.5f);
                    // }
                }
                PlayerPrefs.SetInt("CloudOpened", temp);
            }
            else
            {
                for (int i = 0; i < IsWinning / 2; i++)
                {
                    // if (playerLevel % IsWinning != 0)
                    //{
                    _listCloud.transform.GetChild(i).GetComponent<Animator>().Play("out");
                    yield return new WaitForSeconds(0.5f);
                    // }
                }
                PlayerPrefs.SetInt("CloudOpened", IsWinning / 2);
            }
        }
        else if (PlayerPrefs.GetInt("PlayingPlanet") < PlayerPrefs.GetInt("CompleteLastPlanet"))
        {
            for (int i = 0; i < _listCloud.transform.childCount; i++)
            {
                _listCloud.transform.GetChild(i).gameObject.SetActive(false);
            }
            yield return null;
        }
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
            posCopy.Add(new Position(item.transform.position, i));
            itemList.Add(item);
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
        if (PlayerPrefs.GetInt("ContinueGame") == 0)
        {
            //  _Loading();
            findPos();
            Debug.Log("continue = 0");
        }
        else
        {
            _findPos();
            PlayerPrefs.SetInt("ContinueGame", 0);
            Debug.Log("continue = 1");
        }
    }
    void LevelClick(GameObject _item, int lv)
    {
        Instantiate(MissionPopup);
        PlayerPrefs.SetInt("IsPlaying", lv);
        GameObject[] ParentSetting = GameObject.FindGameObjectsWithTag("ParentSetting");
        if (ParentSetting != null)
        {
            foreach (GameObject ps in ParentSetting)
            {
                ps.SetActive(false);
            }
        }
    }

    public static void setPlanetID(int x)
    {
        PlanetID = x;
    }
    void _enableButton(bool check)
    {
        if (check == true)
        {
            for (int i = 0; i < listContainer.Length; i++)
            {
                if (listContainer[i].childCount > 0)
                {
                    if (listContainer[i].GetChild(0).GetChild(0).GetComponent<Image>().sprite != lockImage)
                    {
                        listContainer[i].GetChild(0).GetChild(0).GetComponent<Button>().enabled = true;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < listContainer.Length; i++)
            {
                if (listContainer[i].childCount > 0)
                    listContainer[i].GetChild(0).GetChild(0).GetComponent<Button>().enabled = false;
            }
        }
    }
}