using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class BuyingPanel : MonoBehaviour {
    public int curID;
    public GameObject prefabTextEffect;
    public GameObject subGoldButton;
    public GameObject subDiamondButton;
    public GameObject panel;
    private void Start()
    {
        gameObject.GetComponent<Animator>().Play("In");
        gameObject.GetComponent<Animator>().SetBool("In", false);
        if (panel != null)
        {
            panel.SetActive(true);
            panel.GetComponent<Image>().DOColor(new Color(0, 0, 0, 225f / 255), 0.8f);
        }
    }
    public void isYes()
    {
        int i = curID - 1;
        int div = i % 2;
        if (ShopManager.instance.requestMoney(SpaceshipShop.instance.spaceshipList[i].Gold))
        {
            SpaceshipShop.instance.spaceshipList[i].bought = true;
            PlayerPrefs.SetInt("Spaceship", SpaceshipShop.instance.spaceshipList[i].spaceshipID);
            ShopManager.instance.reduceMoney(SpaceshipShop.instance.spaceshipList[i].Gold);
            SpaceshipShop.instance.buybuttonList.Add(SpaceshipShop.instance.spaceshipList[i]);
            ShopManager.instance.curSpaceshipID = curID;
            SpaceshipShop.instance.UpdateBuyButtons();
            prefabTextEffect.transform.GetChild(0).GetComponent<Text>().text = "-" + SpaceshipShop.instance.spaceshipList[i].Gold;
            Instantiate(prefabTextEffect, SpaceshipShop.instance.saveHolder[i/2].transform.GetChild(div));
            SaveLoad.instance.saving();

        }
        else    
        {
            prefabTextEffect.transform.GetChild(0).GetComponent<Text>().text = "NOT ENOUGH";
            Instantiate(prefabTextEffect, SpaceshipShop.instance.saveHolder[i / 2].transform.GetChild(div));
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
        if (panel != null)
        {
            panel.GetComponent<Image>().DOColor(new Color(0, 0, 0, 0), 0.5f);
        }
        Animator ani = gameObject.GetComponent<Animator>();
        ani.Play("out");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
