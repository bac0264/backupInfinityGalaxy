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
    public GameObject Container;
    public GameObject DialogProfilePic;
    public GameObject buttonLogOut;
    public GameObject itemPref;
    public Transform friendsPhotoContainer;
    List<string> permissions = new List<string>();
    List<Sprite> img = new List<Sprite>();
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
        if (FB.IsInitialized) {
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

    public void FBlogin()
    {
        permissions.Add("public_profile");
        FB.LogInWithReadPermissions(permissions, AuthCallBack);
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
    public void FBlogout()
    {
        FB.LogOut();
        logOut();
    }
    public void FBshare()
    {
        //FB.ShareLink
    }
    void logOut()
    {
        buttonLogOut.SetActive(false);
        DialogLoggedIn.SetActive(false);
        DialogLoggedOut.SetActive(true);
        Container.SetActive(false);
    }
    void DealWithFBMenus(bool isLoggedIn)
    {
        Debug.Log(isLoggedIn);
        if (isLoggedIn)
        {
            buttonLogOut.SetActive(true);
            DialogLoggedIn.SetActive(true);
            DialogLoggedOut.SetActive(false);
           // FB.API("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
            FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
            Container.SetActive(true);
            //FB.API("/me?friends/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
        }
        else
        {
            buttonLogOut.SetActive(false);
            DialogLoggedIn.SetActive(false);
            DialogLoggedOut.SetActive(true);
            Container.SetActive(false);
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
    void InITFaceBookFriends()
    {
        FB.API("me/taggable_friends", Facebook.Unity.HttpMethod.GET, FBLoadFriendsCallBack);
    }

    //Json to Dictionary
    void FBLoadFriendsCallBack(IGraphResult result)
    {
        if (result.Error == null)
        {
            var dict = (Dictionary<string, object>)Facebook.MiniJSON.Json.Deserialize(result.RawResult);
            var listFriends = (List<object>)dict["data"];
            foreach (object dataObject in listFriends)
            {
                Dictionary<string, object> dataDic = dataObject as Dictionary<string, object>;
                string friendID = dataDic["id"] as string;
                string name = dataDic["name"] as string;

                string url = "https://graph.facebook.com/" + friendID + "/picture?type=large";
                Debug.Log(url);
                StartCoroutine(LoadImage(url));
                // break;
            }
        }
    }


    IEnumerator LoadImage(string urlString)
    {
        WWW url = new WWW(urlString);
        Texture2D textFb = new Texture2D(128, 128, TextureFormat.DXT1, false);

        yield return url;
        url.LoadImageIntoTexture(textFb);
        Rect rec = new Rect(0, 0, textFb.width, textFb.height);
        img.Add(Sprite.Create(textFb, rec, new Vector2(0, 0), .01f));
        GameObject item = Instantiate(itemPref, friendsPhotoContainer);
        item.GetComponent<Image>().sprite = img[imgnum];
        imgnum++;
    }


}

