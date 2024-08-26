
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

public class GetWallet : MonoBehaviour
{
    // Start is called before the first frame update
    private string contestUrl = "https://ludo-project-backend.vercel.app/api/v1/user/wallet/getwallet";

    



    private void Start()
    {
        DataSaver.Instance._id = PlayerPrefs.GetString(DataSaver.Instance.prefabid);
        DataSaver.Instance.firstName = PlayerPrefs.GetString(DataSaver.Instance.prefabfirstName);
        DataSaver.Instance.lastName = PlayerPrefs.GetString(DataSaver.Instance.prefablastName);
        DataSaver.Instance.token = PlayerPrefs.GetString(DataSaver.Instance.prefabToken);
        GetContest(contestUrl);
    }


    [Serializable]
    public class MyData
    {
        public string message;
        public Data data;
    }
    public class Data
    {
        public int deposite;
        public int bonus;
    }
    MyData val;
    public void GetContest(string url)
    {
        StartCoroutine(Registrations(url));
    }

    IEnumerator Registrations(string url)
    {
        Debug.Log("DataSaver.Instance.token" + DataSaver.Instance.token);
        using (UnityWebRequest request = UnityWebRequest.Get(url))
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
                    var json = request.downloadHandler.text;
                    Debug.Log(json.ToString());

                    val = JsonConvert.DeserializeObject<MyData>(json.ToString());
                    DataSaver.Instance.deposite = val.data.deposite;
                    DataSaver.Instance.bonus = val.data.bonus;

                    Debug.Log("noti" +val.message );
                    
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
