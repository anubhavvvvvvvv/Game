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

public class ContestJoining : MonoBehaviour
{

    private string url = "https://ludo-project-backend.vercel.app/api/v1/user/joinContest/";
    public class Data
    {
        public int status;
        public string message;
    }

    public void ContestJoined()
    {
        Debug.Log("contest " + DataSaver.Instance.contestIdJoined);
        url = url + DataSaver.Instance.contestIdJoined ;
        StartCoroutine(Registrations(url));
    }

    IEnumerator Registrations(string url)
    {

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            /* byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

             request.uploadHandler = new UploadHandlerRaw(bodyRaw);*/
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

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

                    Data val = JsonConvert.DeserializeObject<Data>(json.ToString());
                    Debug.Log("val.data.otp" + val.message);
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
