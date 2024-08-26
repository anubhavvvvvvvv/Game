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

public class AddWallet : MonoBehaviour
{
   
    private string url = "https://ludo-project-backend.vercel.app/api/v1/user/wallet/addWallet";
    public void addWallet(int bal)
    {
        // url = url + DataSaver.Instance.contestIdJoined;
        StartCoroutine(Registrations(url,bal));
    }

    IEnumerator Registrations(string url,int balance)
    {
        string jsonData = $"{{\"balance\": \"{balance}\"}}";
        Debug.Log(jsonData);
        // Validate the data fields before sending the request
        if (!string.IsNullOrEmpty(jsonData))
        {
            using (UnityWebRequest request = UnityWebRequest.Post(url, ""))
            {
                byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
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
                        print("Successfully addwallet ");
                        var json = request.downloadHandler.text;
                        Debug.Log(json.ToString());

                        //myData val = JsonConvert.DeserializeObject<myData>(json.ToString());

                    }
                }
                catch (Exception e)
                {
                    print(e);
                }
                finally
                {

                }
            }
        }
    }
}

