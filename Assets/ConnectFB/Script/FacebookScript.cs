using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;

public class FacebookScript : MonoBehaviour
{

    public GameObject DialogLoggedIn;
    public GameObject DialogLoggedOut;
   // public GameObject DialogUsername;
    public GameObject DialogProfilePic;
    List<string> permissions = new List<string>();
    GameObject fb;
    // public GameObject DialogProfilePicOfFriends;
    void Awake()
    {
        if (!FB.IsInitialized)
        {
            Debug.Log("install");
            FB.Init(SetInit, OnHideUnity);
        }
        else
        {
            Debug.Log("active");
            FB.ActivateApp();
        }
        fb = GameObject.FindGameObjectWithTag("Facebook");
    }

    void SetInit()
    {
        if (FB.IsInitialized) {
            FB.ActivateApp();
            //var token = AccessToken.CurrentAccessToken;
            //Debug.Log(token.TokenString);
            //permissions.Add(token.TokenString);
            //FB.LogInWithPublishPermissions(permissions, AuthCallBack);
        }
        else
        {
        }
        DealWithFBMenus(FB.IsLoggedIn);

    }

    void OnHideUnity(bool isGameShown)
    {

        if (!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

    }

    public void FBlogin()
    {
        permissions.Add("public_profile");
        Debug.Log("Permission:"+permissions.ToArray());
        FB.LogInWithReadPermissions(permissions, AuthCallBack);
    }

    void AuthCallBack(IResult result)
    {
         
        if (result.Error != null)
        {
            Debug.Log(result.Error);
        }
        else
        {
            if (FB.IsLoggedIn)
            {
                Debug.Log("FB is logged in");
                fb.transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                fb.transform.GetChild(0).gameObject.SetActive(false);
                Debug.Log("FB is not logged in");
            }

            DealWithFBMenus(FB.IsLoggedIn);
        }

    }
    public void FBlogout()
    {
        FB.LogOut();
    }
    public void FBshare()
    {
        //FB.ShareLink
    }
    void DealWithFBMenus(bool isLoggedIn)
    {

        if (isLoggedIn)
        {
            DialogLoggedIn.SetActive(true);
            DialogLoggedOut.SetActive(false);
           // FB.API("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
            FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
            //FB.API("/me?friends/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
        }
        else
        {
            DialogLoggedIn.SetActive(false);
            DialogLoggedOut.SetActive(true);
        }

    }

    //void DisplayUsername(IResult result)
    //{

    //    Text UserName = DialogUsername.GetComponent<Text>();

    //    if (result.Error == null)
    //    {

    //        UserName.text = "Hi there, " + result.ResultDictionary["first_name"];

    //    }
    //    else
    //    {
    //        Debug.Log(result.Error);
    //    }

    //}

    void DisplayProfilePic(IGraphResult result)
    {

        if (result.Texture != null)
        {

            Image ProfilePic = DialogProfilePic.GetComponent<Image>();

            ProfilePic.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
            Debug.Log(ProfilePic);
        }
    }
    void DisplayProfilePicOfFriends(IGraphResult result)
    {
        if (result.Texture != null) {

            Image ProfilePicOfFriends = DialogProfilePic.GetComponent<Image>();

            ProfilePicOfFriends.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
        }
    }
    private void ScoresCallBack(IGraphResult result)
    {
        int num = -1;

        var dataList = result.ResultDictionary["data"] as List<object>;

        foreach (object player in dataList)
        {
            num++;
            var dataDict = dataList[num] as Dictionary<string, object>;

            long score = (long)dataDict["score"];
            var user = dataDict["user"] as Dictionary<string, object>;

            string userName = user["name"] as string;
            string userID = user["id"] as string;

            GameObject ScorePanel;
            //ScorePanel = Instantiate() as GameObject;
           // ScorePanel.transform.SetParent(leaderboardPanel.transform, false);
           // ScorePanel.SetActive(true);

           // ScorePanel.transform.GetChild(1).GetComponent<Text>().text = userName;
           // ScorePanel.transform.GetChild(2).GetComponent<Text>().text = score.ToString();
        }
    }

}

