using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System.Text.RegularExpressions;
using System;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class UpdateSound : MonoBehaviour
{
    //private string url = "https://ludo-project-backend.vercel.app/api/v1/user/updateSound";
  
    private void Start()
    {
        
    }

    public void SoundAPI(string url)
    {
       // url = url + DataSaver.Instance.contestIdJoined;
        StartCoroutine(Registrations(url));
    }
    public void MusicAPI(string url)
    {
        // url = url + DataSaver.Instance.contestIdJoined;
        StartCoroutine(Registrations(url));
    }
    public void LanguageAPI(string url)
    {
        // url = url + DataSaver.Instance.contestIdJoined;
        StartCoroutine(Registrations(url));
    }

    IEnumerator Registrations(string url)
    {

        using (UnityWebRequest request = UnityWebRequest.Put(url,""))
        {
            /* byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

             request.uploadHandler = new UploadHandlerRaw(bodyRaw);*/
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + DataSaver.Instance.token);
            yield return request.SendWebRequest();
            var response = request.result;
            try
            {
                if (request.result != UnityWebRequest.Result.Success) Debug.Log(request.error);
                else if (request.result == UnityWebRequest.Result.Success)
                {
                    print("Successfully registered ");
                    var json = request.downloadHandler.text;
                    Debug.Log(json.ToString());

                   // MyData val = JsonConvert.DeserializeObject<MyData>(json.ToString());
                    
                    //Debug.Log("rules" + val.message);
                }
            }
            catch (Exception e)
            {
                print("exception " + e);
            }
            finally
            {

            }
        }

    }
}
