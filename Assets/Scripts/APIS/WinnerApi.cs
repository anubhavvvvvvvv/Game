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

public class WinnerApi : MonoBehaviour
{
    [SerializeField] AddWallet addWallet;
    public class myData
    {
        public int status;
        public string message;
    }

    public void winner(string url)
    {
        // url = url + DataSaver.Instance.contestIdJoined;
        StartCoroutine(Registrations(url,1));
    }
    public void Secondwinner(string url)
    {
        // url = url + DataSaver.Instance.contestIdJoined;
        StartCoroutine(Registrations(url,2));
    }
    public void Thirdwinner(string url)
    {
        // url = url + DataSaver.Instance.contestIdJoined;
        StartCoroutine(Registrations(url,3));
    }

    IEnumerator Registrations(string url,int pos)
    {
        string jsonData = $"{{\"contestId\": \"{DataSaver.Instance.contestIdJoined}\", \"userId\": \"{DataSaver.Instance._id}\"}}";
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
                        print("Successfully win "+pos);
                        var json = request.downloadHandler.text;
                        Debug.Log(json.ToString());
                        if (pos == 1) addWallet.addWallet(DataSaver.Instance.firstPrize);
                        else if (pos == 2) addWallet.addWallet(DataSaver.Instance.secondPrize);
                        else if (pos == 3) addWallet.addWallet(DataSaver.Instance.thirdPrize);
                        myData val = JsonConvert.DeserializeObject<myData>(json.ToString());
                       
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
