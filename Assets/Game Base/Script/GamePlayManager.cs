using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    int score = 0;
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
    public void BackClick()
    {
        SceneManager.LoadScene("SelectLevel");
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
        //ScoreBar.GetComponent<Image>().fillAmount = score * 1f / threeStar;
    }
    
    IEnumerator TimeCountDown()
    {
        while (ValueLimit > 0)
        {
            yield return new WaitForSeconds(1f);
            ValueLimit--;
            txtGameLimit.GetComponentsInChildren<Text>()[1].text = ValueLimit.ToString();
        }
        //Thua
    }
    public GameObject getRandromPlanet()
    {
        return appearPlanet[UnityEngine.Random.Range(0, appearPlanet.Count)].Planet;        
    }
    //public void 
    public void Moved()
    {
        if (gameLimit == GameLimit.Move)
        {
            ValueLimit--;
            txtGameLimit.GetComponentsInChildren<Text>()[1].text = ValueLimit.ToString();
            if (ValueLimit == 0)
            {
                //Thua
            }
        }
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
