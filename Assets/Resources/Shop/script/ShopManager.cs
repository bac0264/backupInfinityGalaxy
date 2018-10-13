using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    [SerializeField]
    public float Gold;
    public float Diamond;
    private bool touching = false;
    public int curSpaceshipID;
    public Text goldText;
    public Text diamondText;
    public float curGold;
    public float curDiamond;
    public bool select;

    Vector2 ScrollVelocity;
    Vector2 beginPos;
    float limitUp;
    float limitDown;
    private void Awake()
    {
        if (instance == null) instance = this;
        UpdateUI();
    }
    public void addMoney(float amount)
    {
        Gold += amount;
        UpdateUI();
    }
    public void reduceMoney(float amount)
    {
        Gold -= amount;
        UpdateUI();
    }
    public bool requestMoney(float amount)
    {
        if (amount <= Gold) return true;
        return false;
    }
    public void UpdateUI()
    {
        if (goldText != null)
            goldText.text = Gold.ToString();
    }
    private void FixedUpdate()
    {
        if (touching)
        {
            touchHold(Input.mousePosition);
        }
        if (Input.GetMouseButtonDown(0) && !touching)
        {
            touching = true;
            touchBegin(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            touching = false;
            touchEnd(Input.mousePosition);
        }
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touching = true;
                touchBegin(Input.GetTouch(0).position);
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                touchHold(Input.GetTouch(0).position);
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                touching = false;
                touchEnd(Input.GetTouch(0).position);
            }
        }
    }
    void touchBegin(Vector2 screenPos)
    {
        beginPos = Camera.main.ScreenToWorldPoint(screenPos);
    }
    void touchHold(Vector2 screenPos)
    {
        ScrollVelocity = Camera.main.ScreenToWorldPoint(screenPos);
        ScrollVelocity = ScrollVelocity - screenPos;
    }
    void touchEnd(Vector2 screenPos)
    {
        GameObject selectItemHolder = ObjectClicked(screenPos);
        if (selectItemHolder != null && !select )
        {
            selectItemHolder.GetComponent<ItemHolder>().Select();
        }
        else
        {

        }
    }
    public void Add()
    {
        Gold = Gold + 500f;
        UpdateUI();
    }
    GameObject ObjectClicked(Vector2 screenPosition)
    {
        //Converting Mouse Pos to 2D (vector2) World Pos
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPosition);
        Vector2 rayPos = new Vector2(worldPos.x, worldPos.y);
        RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);
        if (hit)
        {
            return hit.transform.gameObject;
        }
        else return null;
    }
}
