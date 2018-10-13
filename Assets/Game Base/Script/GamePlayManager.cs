using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayManager : SerializedMonoBehaviour
{
    [BoxGroup("Game Limit")]
    public GameLimit gameLimit;
    [BoxGroup("Game Limit")]
    public int ValueLimit;
    [BoxGroup("Game Limit")]
    public GameObject txtGameLimit;
    [BoxGroup("Score Star")]
    public int oneStar, twoStar, threeStar;
    [BoxGroup("Score Star")]
    public GameObject OneStarPos, TwoStarPos, ThreeStarPos, SpaceShip, ScoreBar, StarExplo;
    [HideInInspector]
    public int score = 0;
    public List<AppearPlanet> appearPlanet;
    [BoxGroup("Target")]
    public GameObject itemTargetPrefab, containerTarget;
    [BoxGroup("Target")]
    public List<TargetPlanet> targetPlanet;
    // Use this for initialization
    void Start () {
        //Debug.Log(ScoreBar.GetComponent<RectTransform>().rect.width);
        if(gameLimit==GameLimit.Time)
        {
            txtGameLimit.GetComponentsInChildren<Text>()[0].text = "Time";
            txtGameLimit.GetComponentsInChildren<Text>()[1].text = ValueLimit.ToString();
            StartCoroutine(TimeCountDown());
        }
        else
        {
            txtGameLimit.GetComponentsInChildren<Text>()[0].text = "Move";
            txtGameLimit.GetComponentsInChildren<Text>()[1].text = ValueLimit.ToString();
        }
        foreach (var obj in targetPlanet)
        {
            itemTargetPrefab.GetComponent<Image>().sprite = obj.Planet.GetComponent<SpriteRenderer>().sprite;
            itemTargetPrefab.GetComponentInChildren<Text>().text = obj.NumOfTarget.ToString();
            obj.ItemTarget=Instantiate(itemTargetPrefab, containerTarget.transform);
        }
        ScoreBar.GetComponent<Image>().fillAmount = 0;
        OneStarPos.GetComponent<RectTransform>().anchoredPosition = new Vector2(ScoreBar.GetComponent<RectTransform>().rect.width*(oneStar*1f/threeStar), 0);
        TwoStarPos.GetComponent<RectTransform>().anchoredPosition = new Vector2(ScoreBar.GetComponent<RectTransform>().rect.width * (twoStar * 1f / threeStar), 0);
    }
    private void Update()
    {
        float before = ScoreBar.GetComponent<Image>().fillAmount;
        ScoreBar.GetComponent<Image>().fillAmount += (score * 1f / threeStar - ScoreBar.GetComponent<Image>().fillAmount)*0.2f;
        SpaceShip.GetComponent<RectTransform>().anchoredPosition = new Vector2(ScoreBar.GetComponent<RectTransform>().rect.width* ScoreBar.GetComponent<Image>().fillAmount, 0);
        if(oneStar*1f/threeStar< ScoreBar.GetComponent<Image>().fillAmount&& oneStar * 1f / threeStar > before)
        {
            Debug.Log("1 sao");
            Destroy(Instantiate(StarExplo,new Vector3(OneStarPos.transform.position.x, OneStarPos.transform.position.y+1f,0f) ,Quaternion.identity),1.5f);
        }
        if (twoStar * 1f / threeStar < ScoreBar.GetComponent<Image>().fillAmount && twoStar * 1f / threeStar > before)
        {
            Debug.Log("2 sao");
            Destroy(Instantiate(StarExplo, new Vector3(TwoStarPos.transform.position.x, OneStarPos.transform.position.y + 1f, 0f), Quaternion.identity), 1.5f);
        }
        if(before<1&& ScoreBar.GetComponent<Image>().fillAmount == 1)
        {
            Debug.Log("3 sao");
            Destroy(Instantiate(StarExplo, new Vector3(ThreeStarPos.transform.position.x, OneStarPos.transform.position.y + 1f, 0f), Quaternion.identity), 1.5f);
        }
    }
    public void addScore(int num)
    {
        score += num;
        //checkQuaman
        /*bool check = true;
        for (int i = 0; i < targetPlanet.Count; i++)
        {
            if(a)
        }
        if(check)//qua man*/
        //ScoreBar.GetComponent<Image>().fillAmount = score * 1f / threeStar;
    }
    IEnumerator TimeCountDown()
    {
        while (ValueLimit > 0)
        {
            yield return new WaitForSeconds(1f);
            if(ValueLimit>0) ValueLimit--;
            txtGameLimit.GetComponentsInChildren<Text>()[1].text = ValueLimit.ToString();
        }
        checkGame();
    }
    public GameObject getRandromPlanet()
    {
        return appearPlanet[UnityEngine.Random.Range(0, appearPlanet.Count)].Planet;        
    }    
    public void Moved()
    {
        if (gameLimit == GameLimit.Move)
        {
            ValueLimit--;
            txtGameLimit.GetComponentsInChildren<Text>()[1].text = ValueLimit.ToString();
        }
    }
    IEnumerator waitCheckGame()
    {
        yield return new WaitForSeconds(2f);
        bool check = true;
        for (int i = 0; i < targetPlanet.Count; i++)
        {
            if (targetPlanet[i].Count != targetPlanet[i].NumOfTarget)
            {
                check = false;
                break;
            }
        }
        if (check && ValueLimit >= 0)
        {
            //qua man
            Debug.Log("victory");
            ValueLimit = 0;
            GameObject.FindGameObjectWithTag("PopupContainer").GetComponent<PopupManager>().showDialog("Victory");
        }
        else if (!check && ValueLimit == 0)
        {
            //gameover
            Debug.Log("gameover");
            GameObject.FindGameObjectWithTag("PopupContainer").GetComponent<PopupManager>().showDialog("GameOver");
        }
    }
    public void checkGame()
    {
        StartCoroutine(waitCheckGame());

    }
}
public class AppearPlanet
{
    public GameObject Planet;
    public float PercentAppear;
}
public class TargetPlanet
{
    public GameObject Planet;
    [HideInInspector]
    public GameObject ItemTarget;
    public int NumOfTarget;
    [HideInInspector]
    public int Count;
}
public enum GameLimit
{
    Move,
    Time
}
