﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuyButton : MonoBehaviour {
    public static BuyButton instance;
    public int spaceshipID;
    public bool select;
    public GameObject Panel;
    private void Start()
    {
        if (instance != null) instance = this;
    }
    public void _buyButton()
    {
        if (spaceshipID == 0) {
            Debug.Log("Error");
            return;
        }
        Debug.Log(SpaceshipShop.instance);
        for (int i = 0; i < SpaceshipShop.instance.spaceshipList.Count; i++)
        {
            // check id
            if (spaceshipID == SpaceshipShop.instance.spaceshipList[i].spaceshipID)
            {
                Panel.GetComponent<BuyingPanel>().curID = spaceshipID;
                Panel.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Do u want to buy it";
                // check bought
                if (!SpaceshipShop.instance.spaceshipList[i].bought)
                {
                    Instantiate(Panel, null);
                }
                else 
                {
                    ShopManager.instance.curSpaceshipID = spaceshipID;
                }
            }
            else
            {

            }
        }
    }
    public void UpdateBuyButton()
    {
        SpaceshipShop.instance.UpdateBuyButtons();
    }
}