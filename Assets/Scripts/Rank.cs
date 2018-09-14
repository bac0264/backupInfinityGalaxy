using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;

public class Rank : MonoBehaviour
{
    public GameObject ListContainer,rankPref;
    List<Sprite> img = new List<Sprite>();
    public GameObject yourRank;
    public List<Sprite> ranking = new List<Sprite>();
    int imgnum = 0;
    // public GameObject DialogProfilePicOfFriends;
    void Awake()
    {
        if (!FB.IsInitialized)
        {

            FB.Init(SetInit, OnHideUnity);
        }
        else
        {
            FB.ActivateApp();
            DealWithFBMenus(FB.IsLoggedIn);
        }
    }

    void SetInit()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
            DealWithFBMenus(FB.IsLoggedIn);
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
    void AuthCallBack(IResult result)
    {

        if (result.Error != null)
        {
        }
        else
        {
            if (FB.IsLoggedIn)
            {
                //  Debug.Log("FB is logged in");
            }
            else
            {
                //  fb.transform.GetChild(0).gameObject.SetActive(false);
                // Debug.Log("FB is not logged in");
            }

            DealWithFBMenus(FB.IsLoggedIn);
        }

    }
    void DealWithFBMenus(bool isLoggedIn)
    {
        if (isLoggedIn)
        {
            // FB.API("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
            FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
            //FB.API("/me?friends/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
            FB.API("/me/friends", HttpMethod.GET, FBLoadFriendsCallBack);
        }
        else
        {
        }

    }


    //Json to Dictionary
    void FBLoadFriendsCallBack(IGraphResult result)
    {
        if (result.Error == null)
        {

            var dict = (Dictionary<string, object>)Facebook.MiniJSON.Json.Deserialize(result.RawResult);
            var listFriends = (List<object>)dict["data"];
            Debug.Log(listFriends.ToArray());
            foreach (object dataObject in listFriends)
            {
                Dictionary<string, object> dataDic = dataObject as Dictionary<string, object>;
                string friendID = dataDic["id"] as string;
                string name = dataDic["name"] as string;

                string url = "https://graph.facebook.com/" + friendID + "/picture?type=large";
                Debug.Log(url);
                StartCoroutine(LoadProfile(url,name));
                // break;
            }
        }
    }


    IEnumerator LoadProfile(string urlString, string name)
    {
        WWW url = new WWW(urlString);
        Texture2D textFb = new Texture2D(128, 128, TextureFormat.DXT1, false);

        // Load Image
        yield return url;
        url.LoadImageIntoTexture(textFb);
        Rect rec = new Rect(0, 0, textFb.width, textFb.height);
        img.Add(Sprite.Create(textFb, rec, new Vector2(0, 0), .01f));

        GameObject rankButton = Instantiate(rankPref, ListContainer.transform);
        rankButton.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = img[imgnum];
        rankButton.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = name;
        if (imgnum <= 2)
        {
            rankButton.transform.GetChild(0).GetChild(4).GetComponent<Image>().sprite = ranking[imgnum];
        }
        else {
            rankButton.transform.GetChild(0).GetChild(4).gameObject.SetActive(false);
            rankButton.transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
            rankButton.transform.GetChild(0).GetChild(5).gameObject.GetComponent<Text>().text = (imgnum + 1).ToString();
        }
        //GameObject item = Instantiate(imgPref, friendsPhotoContainer);
        //item.GetComponent<Image>().sprite = img[imgnum];
        imgnum++;
    }
    void DisplayProfilePic(IGraphResult result)
    {

        if (result.Texture != null)
        {
            GameObject rankButton = Instantiate(rankPref, ListContainer.transform);
            Debug.Log(rankButton);
            if (rankButton != null)
            {
                img.Add(Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2()));
                rankButton.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = img[imgnum];
                rankButton.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = name;
                yourRank.GetComponent<Text>().text = (imgnum + 1).ToString();
                rankButton.transform.GetChild(0).GetChild(4).GetComponent<Image>().sprite = ranking[imgnum];
                rankButton.transform.GetChild(0).GetChild(4).GetComponent<RectTransform>().sizeDelta = new Vector2(1.6f, 2);
                imgnum++;
            }
        }
    }
}

