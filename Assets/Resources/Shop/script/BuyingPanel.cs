using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyingPanel : MonoBehaviour {
    public int curID;
    public GameObject warningPanel;
    GameObject Container;
    private void Start()
    {
        Container = GameObject.FindGameObjectWithTag("container");
    }
    public void isYes()
    {
        int i = curID - 1;
        if (ShopManager.instance.requestMoney(SpaceshipShop.instance.spaceshipList[i].Gold))
        {
            SpaceshipShop.instance.spaceshipList[i].bought = true;
            PlayerPrefs.SetInt("Spaceship", SpaceshipShop.instance.spaceshipList[i].spaceshipID);
            ShopManager.instance.reduceMoney(SpaceshipShop.instance.spaceshipList[i].Gold);
            Debug.Log(ShopManager.instance.Gold);
            SpaceshipShop.instance.buybuttonList.Add(SpaceshipShop.instance.spaceshipList[i]);
            ShopManager.instance.curSpaceshipID = curID;
            SpaceshipShop.instance.UpdateBuyButtons();

        }
        else    
        {
            Instantiate(warningPanel, Container.transform);
        }
        Destroy(gameObject);
    }
    public void isNo() {
        Destroy(gameObject);
    }
    public void isExit()
    {
        Destroy(gameObject);
    }
}
