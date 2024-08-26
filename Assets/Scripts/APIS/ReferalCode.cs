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

public class ReferalCode : MonoBehaviour
{
    private string url = "https://ludo-project-backend.vercel.app/api/v1/user/used/RefferCode";
    public TMP_InputField referal;
    public class myData
    {
        public int status;
        public string message;
        public Data data;
    }
    public class Data { }

    public void Referal()
    {
        // url = url + DataSaver.Instance.contestIdJoined;
        StartCoroutine(Registrations(url));
    }

    IEnumerator Registrations(string url)
    {
        string jsonData = $"{{\"refferalCode\": \"{referal.text.ToString()}\"}}";
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
                    print("Successfully refered ");
                    var json = request.downloadHandler.text;
                    Debug.Log(json.ToString());

                    myData val = JsonConvert.DeserializeObject<myData>(json.ToString());
                    showToast(val.message);
                    if (request.result != UnityWebRequest.Result.Success) Debug.Log(request.error);
                    else if (request.result == UnityWebRequest.Result.Success)
                    {
                       

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

    public GameObject popup;
    public TextMeshProUGUI debugText;
    public void showToast(string message)
    {
        StartCoroutine(ShowingToast(message));
    }

    IEnumerator ShowingToast(string message)
    {
        popup.SetActive(true);
        debugText.text = message;
        yield return new WaitForSeconds(3);
        debugText.text = "";
        popup.SetActive(false);
        //MoveOn(current, next);
    }
}
