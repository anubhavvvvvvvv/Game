using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Facebook.Unity;
using UnityEngine.UI;


public class FacebookScript : MonoBehaviour
{
   // public GameObject Panel_Add;
  //  public Text FB_userName;
   // public Image FB_useerDp;
  /*  private void Awake()
    {
        FB.Init(SetInit, onHidenUnity);
       // Panel_Add.SetActive(false);
    }
    void SetInit()
    {
        if (FB.IsLoggedIn)
        {
            Debug.Log("Facebook is Login!");
        }
        else
        {
            Debug.Log("Facebook is not Logged in!");
        }
        DealWithFbMenus(FB.IsLoggedIn);
    }

    void onHidenUnity(bool isGameShown)
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
    public void FBLogin()
    {
        List<string> permissions = new List<string>();
        permissions.Add("public_profile");
        FB.LogInWithReadPermissions(permissions, AuthCallBack);
    }
    // Start is called before the first frame update
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
                    Debug.Log("Facebook is Login!");
                   // Panel_Add.SetActive(true);
                }
            else
            {
                Debug.Log("Facebook is not Logged in!");
            }
            DealWithFbMenus(FB.IsLoggedIn);
        }
    }

    void DealWithFbMenus(bool isLoggedIn)
    {
        if (isLoggedIn)
        {
            FB.API("/me?fields=first_name",HttpMethod.GET,DisplayUsername);
            FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
        }
        else
        {

        }
    }
    void DisplayUsername(IResult result)
    {
        if (result.Error == null)
        {
            string name = ""+result.ResultDictionary["first_name"];
          //  FB_userName.text = name;
            
            Debug.Log(""+name);
        }
        else
        {
            Debug.Log(result.Error);
        }
    }

    void DisplayProfilePic(IGraphResult result)
    {
        if (result.Texture != null)
        {
            Debug.Log("Profile Pic");
           // FB_useerDp.sprite = Sprite.Create(result.Texture,new Rect(0,0,128,128),new Vector2());
        }
        else
        {
            Debug.Log(result.Error);
        }
    }
  */

}
