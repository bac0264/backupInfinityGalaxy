﻿using System.Collections;
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
    public GameObject warningPanel;
    public enum _Type
    {
        Exit,
        Yes,
        No,
    };
    _Type type = new _Type();

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
        type = _Type.Yes;
        StartCoroutine(loadAnimation());
    }
    public void isNo() {
        type = _Type.No;
        StartCoroutine(loadAnimation());
    }
    public void isExit()
    {
        type = _Type.Exit;
        StartCoroutine(loadAnimation());
    }
    IEnumerator loadAnimation()
    {
        if (type == _Type.Yes)
        {
            gameObject.transform.GetChild(1).GetChild(2).GetComponent<Animator>().Play("ScaleButton");
            yield return new WaitForSeconds(0.2f);
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
                Instantiate(prefabTextEffect, SpaceshipShop.instance.saveHolder[i / 2].transform.GetChild(div));
                SaveLoad.instance.saving();

            }
            else
            {
               // prefabTextEffect.transform.GetChild(0).GetComponent<Text>().text = "Not enough money";
                warningPanel.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "Not enough money";
                Instantiate(warningPanel);
            }
            ShopManager.instance.select = false;
            Destroy(gameObject);
        }
        else
        {
            if (panel != null)
            {
                panel.GetComponent<Image>().DOColor(new Color(0, 0, 0, 0), 0.5f);
            }
            if (type == _Type.No)
            {
                gameObject.transform.GetChild(1).GetChild(3).GetComponent<Animator>().Play("ScaleButton");
            }
            else if (type == _Type.Exit)
            {
                gameObject.transform.GetChild(1).GetChild(1).GetComponent<Animator>().Play("ScaleButton");
            }
            yield return new WaitForSeconds(0.2f);
            Animator ani = gameObject.GetComponent<Animator>();
            ani.Play("out");
            yield return new WaitForSeconds(0.5f);
            ShopManager.instance.select = false;
            Destroy(gameObject);
        }
    }
}
