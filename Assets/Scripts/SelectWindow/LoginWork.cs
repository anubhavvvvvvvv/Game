using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Photon.Pun;
using Unity.VisualScripting;

public class LoginWork : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        Debug.Log("Connecting to Photon Master Server");
        PhotonNetwork.ConnectUsingSettings();
    }

     public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Master Server");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Connected to Photon lobby");
        PhotonNetwork.JoinRandomRoom();
        //PhotonNetwork.CreateRoom("rishav");
        base.OnJoinedLobby();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed");
        PhotonNetwork.CreateRoom("rishav");
        base.OnJoinRandomFailed(returnCode, message);
    }



    public override void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom()");
        PhotonNetwork.JoinRandomRoom();
        base.OnCreatedRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom()"+PhotonNetwork.CurrentRoom.Name);
       // SceneManager.LoadScene("Game");
        base.OnJoinedRoom();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRoomFailed");
        base.OnJoinRoomFailed(returnCode, message);
    }


    


}
