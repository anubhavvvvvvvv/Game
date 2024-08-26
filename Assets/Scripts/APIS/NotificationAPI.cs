
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

public class NotificationAPI : MonoBehaviour
{
    public string contestUrl = "https://ludo-project-backend.vercel.app/api/v1/user/notificationList";

    public GameObject contestPrefab;
    public GameObject contestParent;

    private void Start()
    {
        GetContest(contestUrl);
    }


    [Serializable]
    public class Notificationn
    {
        public string _id { get; set; }
        public string userId { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public string status { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public int __v { get; set; }
    }
    [Serializable]
    public class NotificationResponse
    {
        public int status { get; set; }
        public string message { get; set; }
        public List<Notificationn> data { get; set; }
    }
    NotificationResponse val;
    public void GetContest(string url)
    {
        StartCoroutine(Registrations(url));
    }

    IEnumerator Registrations(string url)
    {
        //yield return www; Debug.Log(www.text +"jkj");
        // Root val = JsonConvert.DeserializeObject<Root>(www.text);
        //Debug.Log(www.text + val.data.otp);

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

                    val = JsonConvert.DeserializeObject<NotificationResponse>(json.ToString());
                    Debug.Log("noti" + val.data[0]._id+ val.data[0].title+ val.data[0].body);
                    ShowContest();
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


    public Transform contestTransform;
    public void ShowContest()
    {
        Debug.Log(val.data.Count);
        for (int j = 0; j < val.data.Count; j++)
        {
            GameObject contestButton = Instantiate(contestPrefab);
            contestButton.transform.SetParent(contestParent.transform);
            contestButton.transform.localScale = new Vector3(1, 1, 1);
            contestButton.GetComponent<NotificationsPage>().userId = val.data[j].userId;
            contestButton.GetComponent<NotificationsPage>().Id = val.data[j]._id; Debug.Log("not1 " + val.data[j].body);
            contestButton.GetComponent<NotificationsPage>().title = val.data[j].title;
            contestButton.GetComponent<NotificationsPage>().body = val.data[j].body; 
            contestButton.GetComponent<NotificationsPage>().createdAt = val.data[j].createdAt;
            contestButton.GetComponent<NotificationsPage>().updatedAt = val.data[j].updatedAt; 
            contestButton.GetComponent<NotificationsPage>().SetNoti(val.data[j].body);
        }
    }
}

