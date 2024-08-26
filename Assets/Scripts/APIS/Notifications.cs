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

public class NotificationsPage : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI noti;


    public string userId { get; set; }
    public string Id { get; set; }
    public string title { get; set; }
    public string body { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }



    public void SetNoti(string prize)
    {
        noti.text =title+"\n"+ prize.ToString();
    }
    
}
