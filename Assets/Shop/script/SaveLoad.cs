using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public class SaveLoad : MonoBehaviour {
    public class SaveData{
        public List<SpaceShip> shopList = new List<SpaceShip>();  
        public int curspaceshipID;
        public float Gold;
        public float Diamond;
    }
    public void saving()
    {
        SaveData saveData = new SaveData();
        saveData.shopList.Clear();
        saveData.curspaceshipID = ShopManager.instance.curSpaceshipID;
        saveData.Gold = ShopManager.instance.Gold;
        saveData.Diamond = ShopManager.instance.Diamond;
        for (int i = 0; i < SpaceshipShop.instance.spaceshipList.Count; i++) {
            saveData.shopList.Add(SpaceshipShop.instance.spaceshipList[i]);
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(Application.persistentDataPath + "shop.ig", FileMode.Create);
        bf.Serialize(fs, saveData);
        fs.Close();
        Debug.Log("save");
    }
    public void loading()
    {
        if (File.Exists(Application.persistentDataPath + "shop.ig"))
        {
            SaveData saveData = new SaveData();
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(Application.persistentDataPath + "shop.ig", FileMode.Open);
            saveData = (SaveData)bf.Deserialize(fs);
            ShopManager.instance.Gold = saveData.Gold;
            ShopManager.instance.Diamond = saveData.Diamond;
            for (int i = 0; i < saveData.shopList.Count; i++) {
                SpaceshipShop.instance.spaceshipList.Add(saveData.shopList[i]);
                SpaceshipShop.instance.UpdateBuyButtons();
            }
        }
    }
}
