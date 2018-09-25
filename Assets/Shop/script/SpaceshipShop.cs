using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipShop : MonoBehaviour
{
    public static SpaceshipShop instance;
    public List<SpaceShip> spaceshipList = new List<SpaceShip>();
    public List<SpaceShip> buybuttonList = new List<SpaceShip>();
    public GameObject Holder;
    public Transform list;


    private void Start()
    {
        if (instance == null) instance = this;
        FillList();
    }

    // Load spaceshipList
    void FillList()
    {
        Debug.Log("saveload: "+SaveLoad.instance);
        if (SaveLoad.instance != null) SaveLoad.instance.loading();
        for (int i = 0; i < spaceshipList.Count; i++)
        {
            GameObject spaceship = Instantiate(Holder, list, false);
            Holder holder = spaceship.GetComponent<Holder>();

            ItemHolder spaceshipScript_1 = holder.ItemHolder_1.GetComponent<ItemHolder>();
            spaceshipScript_1.spaceshipName.text = spaceshipList[i].spaceshipName;
            spaceshipScript_1.id.text = spaceshipList[i].spaceshipID.ToString();
            spaceshipScript_1.Gold.text = spaceshipList[i].Gold.ToString();
           // spaceshipScript_1.spriteName.sprite = spaceshipList[i].getSpriteName();
            holder.ItemHolder_1.transform.GetChild(4).GetComponent<BuyButton>().spaceshipID = spaceshipList[i].spaceshipID;
            // Buy first item
            if (spaceshipList[i].bought)
            {

            }
            else
            {

            }
            ItemHolder spaceshipScript_2 = holder.ItemHolder_2.GetComponent<ItemHolder>();
            spaceshipScript_2.spaceshipName.text = spaceshipList[++i].spaceshipName;
            spaceshipScript_2.id.text = spaceshipList[i].spaceshipID.ToString();
            spaceshipScript_2.Gold.text = spaceshipList[i].Gold.ToString();
           // spaceshipScript_2.spriteName.sprite = spaceshipList[i].getSpriteName();
            holder.ItemHolder_2.transform.GetChild(4).GetComponent<BuyButton>().spaceshipID = spaceshipList[i].spaceshipID;
            // Buy second item
            if (spaceshipList[i].bought)
            {

            }
            else
            {

            }
        }
    }
    public void UpdateBuyButtons()
    {
        int curID = ShopManager.instance.curSpaceshipID;
        for (int j = 0; j < buybuttonList.Count; j++)
        {
            for (int i = 0; i < spaceshipList.Count; i++)
            {
                if (spaceshipList[i].bought && spaceshipList[i].spaceshipID != curID && buybuttonList[j].spaceshipID == spaceshipList[i].spaceshipID)
                {
                    Debug.Log("index:" + spaceshipList[i] + "Use");
                }
                else if (spaceshipList[i].bought && spaceshipList[i].spaceshipID == curID && buybuttonList[j].spaceshipID == spaceshipList[i].spaceshipID)
                {
                    Debug.Log("Using");
                }
            }
        }
    }
}
