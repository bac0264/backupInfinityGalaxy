using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopManager : MonoBehaviour {
    public static ShopManager instance;
    [SerializeField]
    private float Gold;
    private float Diamond;

    public Text goldText;
    public Text diamondText;
    private void Start()
    {
        if (instance == null) instance = this;
        UpdateUI();
    }

    public void addMoney(float amount) {
        Gold += amount;
        UpdateUI();
    }
    public void reduceMoney(float amount) {
        Gold -= amount;
        UpdateUI();
    }
    public bool requestMoney(float amount) {
        if (amount <= Gold) return true;
        return false;
    }
    void UpdateUI()
    {
        if(goldText != null)
        goldText.text =  Gold.ToString();
    }

}
