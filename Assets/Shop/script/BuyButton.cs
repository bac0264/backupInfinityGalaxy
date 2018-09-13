using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyButton : MonoBehaviour {
    public int spaceshipID;
    public void _buyButton()
    {
        if (spaceshipID == 0) {
            Debug.Log("Error");
            return;
        }
        Debug.Log(SpaceshipShop.instance);
        for (int i = 0; i < SpaceshipShop.instance.spaceshipList.Count; i++)
        {
            if (spaceshipID == SpaceshipShop.instance.spaceshipList[i].spaceshipID)
            {
                if (!SpaceshipShop.instance.spaceshipList[i].bought)
                {
                    if (ShopManager.instance.requestMoney(SpaceshipShop.instance.spaceshipList[i].Gold))
                    {
                        SpaceshipShop.instance.spaceshipList[i].bought = true;
                        ShopManager.instance.reduceMoney(SpaceshipShop.instance.spaceshipList[i].Gold);
                        Debug.Log(SpaceshipShop.instance.spaceshipList[i].Gold);
                        break;
                    }
                }
            }
        }
    }
}
