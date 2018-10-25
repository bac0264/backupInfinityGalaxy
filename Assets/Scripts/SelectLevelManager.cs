using System.Collections;
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
        public float pos;
        public int index;

        public Position(float pos, int index)
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
    public GameObject ItemPrefab;
    public Sprite lockImage;
    public Sprite[] unlockImage;
    public static int PlanetID;
    public List<string> listMapId = new List<string>();
    private const int MAX = 60;
    public int IsWinning = 15;
    public Transform[] listContainer;
    GameObject map;
    public Transform listCloud;
    public GameObject loading;
    public List<Position> posCopy;
    const float temp = 7.7f;
    // Use this for initializationS
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
        Debug.Log("Loading:"+ File.Exists(Application.persistentDataPath + "/Position.txt"));
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
                Debug.Log("count:"+ posCopy.Count);
                // do somthing
            }
            catch (Exception e)
            {
                print(e);
            }
        }
    }
    public void findPos()
    {

        bool check = false;
        for (int i = 0; i < posCopy.Count; i++) {
            if (posCopy[i].index == PlayerPrefs.GetInt("IsPlaying")) {
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, 
                    posCopy[i].pos + temp, Camera.main.transform.position.z);
                check = true;
                break;
            }
        }
        if (!check) {
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
        GameObject listCloud = GameObject.FindGameObjectWithTag("cloud");
        if ( PlayerPrefs.GetInt("PlayingPlanet") == PlayerPrefs.GetInt("CompleteLastPlanet") ) {
            int cloudOpened = PlayerPrefs.GetInt("CloudOpened");
            if (cloudOpened != 0)
            {
                for (int i = 0; i <= cloudOpened; i++)
                {
                    if (playerLevel % IsWinning != 0)
                    {
                        listCloud.transform.GetChild(i).gameObject.SetActive(false);
                    }
                }
            }
            for (int i = 0; i <= div; i++)
            {
                if (playerLevel % IsWinning != 0)
                {
                    listCloud.transform.GetChild(i).GetComponent<Animator>().Play("out");
                    Debug.Log("run");
                    yield return new WaitForSeconds(0.3f);
                }
            }
            PlayerPrefs.SetInt("CloudOpened", div);
        }
        else if(PlayerPrefs.GetInt("PlayingPlanet") < PlayerPrefs.GetInt("CompleteLastPlanet"))
        {
            for (int i = 0; i < listCloud.transform.childCount; i++) {
                listCloud.transform.GetChild(i).gameObject.SetActive(false);
            }
            yield return null;
        }
    }
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
        map.GetComponent<Map>()._changeMap(PlayerPrefs.GetInt("PlayingPlanet"));
        string listidstr = PlayerPrefs.GetString("ListMapId");
        int playerLevel = PlayerPrefs.GetInt("PlayerLevel");

        listMapId.AddRange(listidstr.Split('|'));
        _SelectLvCase(PlanetID, playerLevel);
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
            posCopy.Add(new Position(item.transform.position.y , i));
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
        _Loading();
        findPos();
    }
    void LevelClick(GameObject _loading, GameObject _item, int lv)
    {
        Debug.Log(_item.transform.position);
        _loading.GetComponent<LoadAsync>().LoadingMainGame();
        PlayerPrefs.SetInt("IsPlaying", lv);
        GameObject[] ParentSetting = GameObject.FindGameObjectsWithTag("ParentSetting");
        if (ParentSetting != null) {
            foreach(GameObject ps in ParentSetting)
            {
                ps.SetActive(false);
            }
        }
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