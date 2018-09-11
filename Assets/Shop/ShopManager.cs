using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopManager : MonoBehaviour {
    [SerializeField]
    private float money;
    public Text moneyText;


    public void addMoney(float amount) {
        money += amount;
        UpdateUI();
    }
    public void reduceMoney(float amount) {
        money -= amount;
        UpdateUI();
    }
    public bool requestMoney(float amount) {
        if (amount <= money) return true;
        return false;
    }
    void UpdateUI()
    {
        moneyText.text = "$" +money.ToString();
    }

}
