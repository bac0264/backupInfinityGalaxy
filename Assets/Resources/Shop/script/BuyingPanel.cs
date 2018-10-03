using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyingPanel : MonoBehaviour {
    public int curID;
    public GameObject warningPanel;
    public GameObject successingPanel;
    GameObject Container;
    private void Start()
    {
        gameObject.GetComponent<Animator>().Play("In");
        gameObject.GetComponent<Animator>().SetBool("In", false);
    }
    public void isYes()
    {
        int i = curID - 1;
        if (ShopManager.instance.requestMoney(SpaceshipShop.instance.spaceshipList[i].Gold))
        {
            SpaceshipShop.instance.spaceshipList[i].bought = true;
            PlayerPrefs.SetInt("Spaceship", SpaceshipShop.instance.spaceshipList[i].spaceshipID);
            ShopManager.instance.reduceMoney(SpaceshipShop.instance.spaceshipList[i].Gold);
            SpaceshipShop.instance.buybuttonList.Add(SpaceshipShop.instance.spaceshipList[i]);
            ShopManager.instance.curSpaceshipID = curID;
            SpaceshipShop.instance.UpdateBuyButtons();
            Instantiate(successingPanel, Container.transform);

        }
        else    
        {
            Instantiate(warningPanel, Container.transform);
        }
        Destroy(gameObject);
    }
    public void isNo() {
        StartCoroutine(loadAnimation());
    }
    public void isExit()
    {
        StartCoroutine(loadAnimation());
    }
    IEnumerator loadAnimation()
    {
        Animator ani = gameObject.GetComponent<Animator>();
        ani.Play("out");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
