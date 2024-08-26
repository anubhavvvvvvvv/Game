using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SharedCode : MonoBehaviour
{
    public string punRoomLink; // Replace with your pun room link
    public TextMeshProUGUI reedem;

    private void Start()
    {
        string code= UnityEngine.Random.Range(11111, 99999).ToString();
        reedem.text = code;
        DataSaver.Instance.SetRoomId(code);
    }

    public void ShareViaWhatsApp(int val)
    {
        string message = "Join my room: " + DataSaver.Instance.GetRoomId();
        Debug.Log("whatsappppp");
        // Create an Intent to share via WhatsApp
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
        intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), message);
        intentObject.Call<AndroidJavaObject>("setType", "text/plain");
        if (val == 1) { Debug.Log("whatsapp"); intentObject.Call<AndroidJavaObject>("setPackage", "com.whatsapp"); }
        else if (val == 2) { Debug.Log("facebook"); intentObject.Call<AndroidJavaObject>("setPackage", "com.facebook.katana"); }
        else if (val == 3) { Debug.Log("telegram"); intentObject.Call<AndroidJavaObject>("setPackage", "org.telegram.messenger"); }
        else if (val == 4) { Debug.Log("instagram"); intentObject.Call<AndroidJavaObject>("setPackage", "com.instagram.android"); }

        // Start the activity
        AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
        currentActivity.Call("startActivity", intentObject);
    }


    

}
