using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public class SaveLoad : MonoBehaviour {
    public static SaveLoad instance;
    [Serializable]
    public class SaveData{
        public List<SpaceShip> shopList = new List<SpaceShip>();
        public List<SpaceShip> buybuttonList = new List<SpaceShip>();
        public int curspaceshipID;
        public float Gold;
        public float Diamond;
    }
    private void Awake()
    {
        Debug.Log("saveload");
        if (instance == null) instance = this;
    }
    public void saving()
    {
        SaveData saveData = new SaveData();
        saveData.shopList.Clear();
        saveData.buybuttonList.Clear();
        saveData.curspaceshipID = ShopManager.instance.curSpaceshipID;
        saveData.Gold = ShopManager.instance.Gold;
        saveData.Diamond = ShopManager.instance.Diamond;
        for (int i = 0; i < SpaceshipShop.instance.spaceshipList.Count; i++) {
            saveData.shopList.Add(SpaceshipShop.instance.spaceshipList[i]);
        }
        for (int i = 0; i < SpaceshipShop.instance.buybuttonList.Count; i++)
        {
            saveData.buybuttonList.Add(SpaceshipShop.instance.buybuttonList[i]);
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(Application.dataPath + "shop.txt", FileMode.OpenOrCreate);
        bf.Serialize(fs, saveData);
        fs.Close();
        Initiate.Fade("SelectPlanet", new Color(0, 0, 0, 1), 4.0f);
        print("saved data to " + Application.dataPath + "shop.txt");
    }
    public void loading()
    {
        if (!PlayerPrefs.HasKey("IsGameStartedForTheFirstTime"))
        {
            SaveData sd = new SaveData();
            sd.curspaceshipID = 0;
            sd.Gold = 500f;
            for (int i = 0; i < SpaceshipShop.instance.spaceshipList.Count; i++)
            {
                sd.shopList.Add(SpaceshipShop.instance.spaceshipList[i]);
            }
            for (int i = 0; i < SpaceshipShop.instance.buybuttonList.Count; i++)
            {
                sd.buybuttonList.Add(SpaceshipShop.instance.buybuttonList[i]);
            }
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(Application.dataPath + "shop.txt", FileMode.OpenOrCreate);
            bf.Serialize(fs, sd);
            fs.Close();
            SaveData saveData = new SaveData();
            fs = new FileStream(Application.dataPath + "shop.txt", FileMode.Open);
            saveData = (SaveData)bf.Deserialize(fs);
            if (ShopManager.instance != null)
            {
                ShopManager.instance.Gold = saveData.Gold;
                ShopManager.instance.Diamond = saveData.Diamond;
            }
            fs.Close();
            if (SpaceshipShop.instance != null)
            {
                for (int i = 0; i < saveData.buybuttonList.Count; i++)
                {
                    SpaceshipShop.instance.buybuttonList.Add(saveData.buybuttonList[i]);
                }
                for (int i = 0; i < saveData.shopList.Count; i++)
                {
                    SpaceshipShop.instance.spaceshipList.Add(saveData.shopList[i]);
                    SpaceshipShop.instance.UpdateBuyButtons();
                }
            }
        }
        else
        {
            Debug.Log(File.Exists(Application.dataPath + "shop.txt"));
            if (File.Exists(Application.dataPath + "shop.txt"))
            {
                SaveData saveData = new SaveData();
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = new FileStream(Application.dataPath + "shop.txt", FileMode.Open);
                saveData = (SaveData)bf.Deserialize(fs);
                if (ShopManager.instance != null)
                {
                    ShopManager.instance.Gold = saveData.Gold;
                    ShopManager.instance.Diamond = saveData.Diamond;
                }
                fs.Close();
                if (SpaceshipShop.instance != null)
                {
                    for (int i = 0; i < saveData.buybuttonList.Count; i++)
                    {
                        SpaceshipShop.instance.buybuttonList.Add(saveData.buybuttonList[i]);
                    }
                    for (int i = 0; i < saveData.shopList.Count; i++)
                    {
                        SpaceshipShop.instance.spaceshipList.Add(saveData.shopList[i]);
                        SpaceshipShop.instance.UpdateBuyButtons();
                    }
                }
            }
        }
    }
}
