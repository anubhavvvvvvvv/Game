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

public class ResultList : MonoBehaviour
{
    private string url = "https://ludo-project-backend.vercel.app/api/v1/user/winner/Contestlist";
    private string url1 = "https://ludo-project-backend.vercel.app/api/v1/user/lossContestlist";


    public TextMeshProUGUI totalGame;
    public TextMeshProUGUI totalWin;
    public TextMeshProUGUI totalLoss;

    public int game = 0, win = 0, loos = 0;
    public class UserData
    {
        public string _id { get; set; }
        public string contestId { get; set; }
        public List<string> users { get; set; }
        public int firstPrize { get; set; }
        public int secondPrize { get; set; }
        public int thirdPrize { get; set; }
        public int entryFee { get; set; }
        public int noOfuser { get; set; }
        public int joined { get; set; }
        public string status { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public int __v { get; set; }
        public string winner { get; set; }
        public string IInd { get; set; }
    }

    public class Root
    {
        public int status { get; set; }
        public string message { get; set; }
        public List<UserData> data { get; set; }
    }
    public class Data { }

    private void Start()
    {
        winGame();
        LossGame();
    }
    private void Update()
    {
        totalGame.text = (win + loos).ToString();
        totalLoss.text = loos.ToString();
        totalWin.text = win.ToString();
    }
    public void winGame()
    {
        // url = url + DataSaver.Instance.contestIdJoined;
        StartCoroutine(Registrations(url,1));
    }
    public void LossGame()
    {
        // url = url + DataSaver.Instance.contestIdJoined;
        StartCoroutine(Registrations(url1,0));
    }

    IEnumerator Registrations(string url,int v)
    {
        // string jsonData = $"{{\"refferalCode\": \"{referal.text.ToString()}\"}}";
        // Debug.Log(jsonData);
        // Validate the data fields before sending the request
        //if (!string.IsNullOrEmpty(jsonData))
        // {
        Debug.Log("token" + DataSaver.Instance.token);
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            // byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

            // request.uploadHandler = new UploadHandlerRaw(bodyRaw);
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

                Root val = JsonConvert.DeserializeObject<Root>(json.ToString());
                if (v == 1) win = val.data.Count;
                else if (v == 0) loos = val.data.Count;
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
        //}
    }
}
