using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuyButton : MonoBehaviour {
    public static BuyButton instance;
    public int spaceshipID;
    public GameObject Panel;
    private void Awake()
    {
        if (instance != null) instance = this;
    }
    public void _buyButton()
    {
        if (spaceshipID == 0) {
            Debug.Log("Error");
            return;
        }
        // gameObject.GetComponent<Animator>().Play("PickUp");
        Debug.Log("Spaceshipid: " +spaceshipID);
        for (int i = 0; i < SpaceshipShop.instance.spaceshipList.Count; i++)
        {
            // check id
            if (spaceshipID == SpaceshipShop.instance.spaceshipList[i].spaceshipID)
            {
                // check bought
                if (!SpaceshipShop.instance.spaceshipList[i].bought)
                {
                    ShopManager.instance.select = true;
                    Panel.GetComponent<BuyingPanel>().curID = spaceshipID;
                    Panel.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "Do you want to buy it?";
                    Instantiate(Panel, null);
                   // Panel.GetComponent<Animator>().Play("In");
                }
                else 
                {
                    PlayerPrefs.SetInt("Spaceship", spaceshipID);
                    ShopManager.instance.curSpaceshipID = spaceshipID;
                    UpdateBuyButton();
                }
            }
            else
            {

            }
        }
    }
    public void UpdateBuyButton()
    {
        if (SpaceshipShop.instance != null)
            SpaceshipShop.instance.UpdateBuyButtons();
    }
}
