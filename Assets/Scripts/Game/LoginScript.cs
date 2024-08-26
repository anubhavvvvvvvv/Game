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


public class LoginScript : MonoBehaviour
{

    [SerializeField] private TMP_InputField phoneNumber;
    [SerializeField] private TMP_InputField emailId;
    [SerializeField] private TMP_InputField OTPid;
    [SerializeField] private GameObject current;
    [SerializeField] private GameObject next;

    [SerializeField] private TextMeshProUGUI debugText;
    [SerializeField] private GameObject popup;


    [Serializable]
    public class Root
    {
        public int status { get; set; }
        public string message { get; set; }
        public UserDataDetails data { get; set; }
    }

    public class UserDataDetails
    {
        public bool refferalCodeUsed { get; set; }
        public List<object> joinUser { get; set; }
        public bool music { get; set; }
        public bool sound { get; set; }
        public string _id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string mobileNumber { get; set; }
        public string email { get; set; }
        public string socialId { get; set; }
        public string socialType { get; set; }
        public bool accountVerification { get; set; }
        public object profilePic { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        public int __v { get; set; }
        public string otp { get; set; }
        public int deposite { get; set; }
        public string userType { get; set; }
        public int bonus { get; set; }
        public int wallet { get; set; }
        public int winning { get; set; }
    }

    public void PhoneAuthentication(string url)
    {
        string mn = phoneNumber.text;
        if(mn==null || mn=="" || mn.Length != 10)
        {
            showToast("Invalid Phone Number"+mn);
        }
        else
        {
            url = url + "?mobileNumber" + phoneNumber.text;
            WWW www = new WWW(url);
            //StartCoroutine(Registrations(www));
            StartCoroutine(Registrations(url));
        }
        //StartCoroutine(Registrations(url));
    }

    IEnumerator Registrations(string url)
    {
        //yield return www; Debug.Log(www.text +"jkj");
       // Root val = JsonConvert.DeserializeObject<Root>(www.text);
        //Debug.Log(www.text + val.data.otp);

        string jsonData = $"{{\"mobileNumber\": \"{phoneNumber.text}\"}}";
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
                        
                        Root val = JsonConvert.DeserializeObject<Root>(json.ToString());
                        Debug.Log("val.data.otp" + val.data.otp);
                        //Debug.Log("val.data.otp" + JsonConvert.DeserializeObject<Root>(json.ToString()));
                        showToast(val.data.otp);
                        DataSaver.Instance._id = val.data.otp;
                        DataSaver.Instance.firstName = val.data.firstName;
                        DataSaver.Instance.lastName = val.data.lastName;
                        PlayerPrefs.SetString(DataSaver.Instance.prefabid, val.data._id);
                        PlayerPrefs.SetString(DataSaver.Instance.prefabfirstName, val.data.firstName);
                        PlayerPrefs.SetString(DataSaver.Instance.prefablastName, val.data.lastName);
                        MoveOn(current, next);
                    }
                }
                catch (Exception e)
                {
                    string ss = UnityEngine.Random.Range(112345, 987654).ToString();
                    DataSaver.Instance._id = ss;
                    showToast(ss);
                    MoveOn(current, next);
                    print(e);
                }
                finally
                {
                    
                }
            }
        }
    }










    public class LoginData
    {
        public string userId { get; set; }
        public string otp { get; set; }
        public string token { get; set; }
    }

    public class LoginResponse
    {
        public int status { get; set; }
        public string message { get; set; }
        public LoginData data { get; set; }
    }

    public void VerifyOTP(string url)
    {
        Debug.Log("OTPid.text " + OTPid.text + " DataSaver.Instance._id " + DataSaver.Instance._id);
        if (OTPid.text != DataSaver.Instance._id)
        {
           showToast("wrong otp");
        }
        else
        {
            url += DataSaver.Instance._id;
            StartCoroutine(Verifications(url));
            //MoveOn(current, next);
        }
        //url += DataSaver.Instance._id;
        //StartCoroutine(Verifications(url));
    }

    IEnumerator Verifications(string url)
    {


        string jsonData = $"{{\"otp\": \"{OTPid.text}\"}}";
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

                yield return request.SendWebRequest();
                var response = request.result;
                try
                {
                    if (request.result != UnityWebRequest.Result.Success)
                    {
                        Debug.Log(request.error + request.downloadHandler.text); showToast("Server Error..");
                    }
                    else if (request.result == UnityWebRequest.Result.Success)
                    {
                        print("Successfully registered ");
                        var json = request.downloadHandler.text;
                        Debug.Log(json);
                        LoginResponse val = JsonConvert.DeserializeObject<LoginResponse>(json);
                        Debug.Log("message " + val.message);
                        DataSaver.Instance.token = val.data.token;
                        PlayerPrefs.SetString(DataSaver.Instance.prefabToken, val.data.token);
                        //showToast(val.message);
                        MoveOn(current, next);
                    }
                }
                catch (Exception e)
                {
                    print(e);
                }
            }
        }
    }













    public class LoginDataa
    {
        public string _Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string socialId { get; set; }
        public string userType { get; set; }
        public string token { get; set; }
    }

    public class LoginResponsee
    {
        public int status { get; set; }
        public string message { get; set; }
        public LoginDataa data { get; set; }
    }

    public void EmailAuthentication(string url)
    {
        StartCoroutine(EmailVerifications(url));
    }
    IEnumerator EmailVerifications(string url)
    {


        string jsonData = $"{{\"email\": \" \"}}";
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

                yield return request.SendWebRequest();
                var response = request.result;
                try
                {
                    if (request.result != UnityWebRequest.Result.Success) Debug.Log(request.error);
                    else if (request.result == UnityWebRequest.Result.Success)
                    {
                        print("Successfully registered ");
                        var json = request.downloadHandler.text;
                        Debug.Log(json);
                        LoginResponsee val = JsonConvert.DeserializeObject<LoginResponsee>(json);
                        DataSaver.Instance.token = val.data.token;
                        DataSaver.Instance._id = val.data._Id;
                        DataSaver.Instance.firstName = val.data.firstName;
                        DataSaver.Instance.lastName = val.data.lastName;
                        PlayerPrefs.SetString(DataSaver.Instance.prefabid, val.data._Id);
                        PlayerPrefs.SetString(DataSaver.Instance.prefabfirstName, val.data.firstName);
                        PlayerPrefs.SetString(DataSaver.Instance.prefablastName, val.data.lastName);
                        PlayerPrefs.SetString(DataSaver.Instance.prefabToken, val.data.token);
                        Debug.Log("tokenmmm " + PlayerPrefs.GetString(DataSaver.Instance.prefabToken));
                        showToast("Login successfully");
                        StartCoroutine(move());
                    }
                }
                catch (Exception e)
                {
                    print(e);
                }
            }
        }
    }

    IEnumerator move()
    {
        yield return new WaitForSeconds(3);
        MoveOn(current ,next);
    }

    public void MoveOn(GameObject current,GameObject next)
    {
        current.SetActive(false);
        next.SetActive(true);
    }









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
