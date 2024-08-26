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

public class HelpDesk : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text1;
    [SerializeField] private TextMeshProUGUI text2;
    [SerializeField] private TextMeshProUGUI text3;

    private string url = "https://ludo-project-backend.vercel.app/api/v1/admin/getHelpDesk";
    public class MyData
    {
        public string message { get; set; }
        public int status { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public string _id { get; set; }
        public string email { get; set; }
        public string mobileNumber { get; set; }
        public string whatApp { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public int __v { get; set; }
    }
    private void Start()
    {
        ContestJoined();
    }

    public void ContestJoined()
    {
        url = url + DataSaver.Instance.contestIdJoined;
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

                    MyData val = JsonConvert.DeserializeObject<MyData>(json.ToString());
                    text1.text = "Email: "+val.data.email;
                    text2.text = "Mobile No: "+val.data.mobileNumber;
                    text3.text = "Whatsapp No: "+val.data.whatApp;
                    Debug.Log("rules" + val.message);
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
