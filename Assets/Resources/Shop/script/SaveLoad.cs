﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
public class SaveLoad : MonoBehaviour
{
    public static SaveLoad instance;
    [Serializable]
    public class SaveData
    {
        public List<SpaceShip> shopList = new List<SpaceShip>();
        public List<SpaceShip> buybuttonList = new List<SpaceShip>();
        public int curspaceshipID = 0;
        public float Gold = 500;
        public float Diamond = 500;
    }
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    //private void Start()
    //{
    //    loading();
    //}
    public void saving()
    {
        try
        {
            SaveData saveData = new SaveData();
            saveData.shopList.Clear();
            saveData.buybuttonList.Clear();
            saveData.curspaceshipID = ShopManager.instance.curSpaceshipID;
            saveData.Diamond = ShopManager.instance.Diamond;
            saveData.Gold = ShopManager.instance.Gold;
            for (int i = 0; i < SpaceshipShop.instance.spaceshipList.Count; i++)
            {
                saveData.shopList.Add(SpaceshipShop.instance.spaceshipList[i]);
            }
            for (int i = 0; i < SpaceshipShop.instance.buybuttonList.Count; i++)
            {
                saveData.buybuttonList.Add(SpaceshipShop.instance.buybuttonList[i]);
            }

            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(Application.persistentDataPath + "/shop.txt", FileMode.OpenOrCreate);
            bf.Serialize(fs, saveData);
            fs.Close();
        }
        catch(Exception e)
        {
            print(e);
        }
        print("saved data to " + Application.persistentDataPath + "/shop.txt");
    }
    public void loading()
    {
        Debug.Log(Application.persistentDataPath + "/shop.txt");
        if (File.Exists(Application.persistentDataPath + "/shop.txt"))
        {
            try
            {
                SaveData saveData = new SaveData();
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = new FileStream(Application.persistentDataPath + "/shop.txt", FileMode.Open);
                saveData = (SaveData)bf.Deserialize(fs);
                fs.Close();
                if (ShopManager.instance != null)
                {
                    ShopManager.instance.Gold = saveData.Gold;
                    ShopManager.instance.Diamond = saveData.Diamond;
                    ShopManager.instance.curSpaceshipID = saveData.curspaceshipID;
                    ShopManager.instance.UpdateUI();
                }
                if (SpaceshipShop.instance != null)
                {
                    for (int i = 0; i < saveData.buybuttonList.Count; i++)
                    {
                        SpaceshipShop.instance.buybuttonList.Add(saveData.buybuttonList[i]);
                    }
                    for (int i = 0; i < saveData.shopList.Count; i++)
                    {
                        SpaceshipShop.instance.spaceshipList[i] = saveData.shopList[i];
                    }
                }
            }
            catch(Exception e)
            {
                print(e);
            }
        }
    }
}
