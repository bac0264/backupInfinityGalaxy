using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpaceshipShop : MonoBehaviour
{
    public static SpaceshipShop instance;
    public List<SpaceShip> spaceshipList = new List<SpaceShip>();
    public List<SpaceShip> buybuttonList = new List<SpaceShip>();
    public List<GameObject> saveHolder = new List<GameObject>();
    public GameObject Holder;
    public Transform list;


    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void Start()
    {
        ShopManager.instance.curSpaceshipID = PlayerPrefs.GetInt("Spaceship");
        SaveLoad.instance.loading();
        FillList();
    }
    void FillList()
    {
        // Load image
        Sprite[] sprites ;
        sprites = Resources.LoadAll<Sprite>("Shop/sprite/PT");
        if (spaceshipList != null) {
            // Load 
            spaceshipList[0].bought = true;
            buybuttonList.Add(spaceshipList[0]);
        }
        for (int i = 0; i < spaceshipList.Count; i++)
        {
            // Make list shop
            GameObject spaceship = Instantiate(Holder, list, false);
            Holder holder = spaceship.GetComponent<Holder>();

            ItemHolder spaceshipScript_1 = holder.ItemHolder_1.GetComponent<ItemHolder>();
            spaceshipScript_1.spaceshipName.text = spaceshipList[i].spaceshipName;
            spaceshipScript_1.id.text = spaceshipList[i].spaceshipID.ToString();
            spaceshipScript_1.Gold.text = spaceshipList[i].Gold.ToString();
            spaceshipScript_1.sprite.sprite = sprites[i];
            holder.ItemHolder_1.transform.GetChild(4).GetComponent<BuyButton>().spaceshipID = spaceshipList[i].spaceshipID;
            // Buy first item
            if (spaceshipList[i].bought)
            {
                spaceshipScript_1.transform.GetChild(5).gameObject.SetActive(true);
                if (spaceshipList[i].spaceshipID == ShopManager.instance.curSpaceshipID)
                {
                    spaceshipScript_1.transform.GetChild(3).gameObject.GetComponent<Image>().sprite = spaceshipScript_1._types(3);
                    spaceshipScript_1.transform.GetChild(3).GetChild(0).gameObject.SetActive(false);
                }
                else
                {
                    spaceshipScript_1.transform.GetChild(3).gameObject.GetComponent<Image>().sprite = spaceshipScript_1._types(2);
                    spaceshipScript_1.transform.GetChild(3).GetChild(0).gameObject.SetActive(false);
                }
            }
            else
            {

            }
            ItemHolder spaceshipScript_2 = holder.ItemHolder_2.GetComponent<ItemHolder>();
            spaceshipScript_2.spaceshipName.text = spaceshipList[++i].spaceshipName;
            spaceshipScript_2.id.text = spaceshipList[i].spaceshipID.ToString();
            spaceshipScript_2.Gold.text = spaceshipList[i].Gold.ToString();
            spaceshipScript_2.sprite.sprite = sprites[i];
            holder.ItemHolder_2.transform.GetChild(4).GetComponent<BuyButton>().spaceshipID = spaceshipList[i].spaceshipID;
            // Buy second item
            if (spaceshipList[i].bought)
            {
                spaceshipScript_2.transform.GetChild(5).gameObject.SetActive(true);
                if (spaceshipList[i].spaceshipID == ShopManager.instance.curSpaceshipID)
                {
                    spaceshipScript_2.transform.GetChild(3).gameObject.GetComponent<Image>().sprite = spaceshipScript_2._types(3);
                    spaceshipScript_2.transform.GetChild(3).GetChild(0).gameObject.SetActive(false);
                }
                else
                {
                    spaceshipScript_2.transform.GetChild(3).gameObject.GetComponent<Image>().sprite = spaceshipScript_2._types(2);
                    spaceshipScript_2.transform.GetChild(3).GetChild(0).gameObject.SetActive(false);
                }
            }
            else
            {

            }
            saveHolder.Add(spaceship);
        }
        UpdateBuyButtons();
    }
    public void UpdateBuyButtons()
    {
        int curID = ShopManager.instance.curSpaceshipID;
        PlayerPrefs.SetInt("Spaceship", curID);
        Debug.Log(PlayerPrefs.GetInt("Spaceship"));
        for (int j = 0; j < buybuttonList.Count; j++)
        {
            for (int i = 0; i < spaceshipList.Count; i++)
            {
                int div = i % 2;
                // ss were bought but not used
                if (spaceshipList[i].bought && spaceshipList[i].spaceshipID != curID && buybuttonList[j].spaceshipID == spaceshipList[i].spaceshipID)
                {                   
                    if (div == 0)
                    {
                        saveHolder[i / 2].transform.GetChild(div).GetChild(3).gameObject.GetComponent<Image>().sprite = saveHolder[i / 2].transform.GetChild(div).GetComponent<ItemHolder>()._types(2);
                        saveHolder[i / 2].transform.GetChild(div).GetChild(3).GetChild(0).gameObject.SetActive(false);
                        saveHolder[i / 2].transform.GetChild(div).GetChild(5).gameObject.SetActive(true);
                    }
                    else {
                        saveHolder[i / 2].transform.GetChild(div).GetChild(3).gameObject.GetComponent<Image>().sprite = saveHolder[i / 2].transform.GetChild(1).GetComponent<ItemHolder>()._types(2);
                        saveHolder[i / 2].transform.GetChild(div).GetChild(3).GetChild(0).gameObject.SetActive(false);
                        saveHolder[i / 2].transform.GetChild(div).GetChild(5).gameObject.SetActive(true);
                    }
                }
                // ss were bought but used
                else if (spaceshipList[i].bought && spaceshipList[i].spaceshipID == curID && buybuttonList[j].spaceshipID == spaceshipList[i].spaceshipID)
                {
                   
                    if (i % 2 == 0) 
                    {
                        saveHolder[i / 2].transform.GetChild(div).GetChild(3).gameObject.GetComponent<Image>().sprite = saveHolder[i / 2].transform.GetChild(0).GetComponent<ItemHolder>()._types(3);
                        saveHolder[i / 2].transform.GetChild(div).GetChild(3).GetChild(0).gameObject.SetActive(false);
                        saveHolder[i / 2].transform.GetChild(div).GetChild(5).gameObject.SetActive(true);
                    }
                    else
                    {
                        saveHolder[i / 2].transform.GetChild(div).GetChild(3).gameObject.GetComponent<Image>().sprite = saveHolder[i / 2].transform.GetChild(1).GetComponent<ItemHolder>()._types(3);
                        saveHolder[i / 2].transform.GetChild(div).GetChild(3).GetChild(0).gameObject.SetActive(false);
                        saveHolder[i / 2].transform.GetChild(div).GetChild(5).gameObject.SetActive(true);
                    }
                }
            }
        }
        SaveLoad.instance.saving();
    }
}
