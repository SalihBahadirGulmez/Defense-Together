using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LaunchManager : MonoBehaviourPunCallbacks
{

    public GameObject enterGamePanel;
    public GameObject lobbyPanel;
    public GameObject loadingScreen;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    void Start()
    {
        enterGamePanel.SetActive(true);
        lobbyPanel.SetActive(false);
        loadingScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ConnnectToPhotonServer()
    {
        if (!PhotonNetwork.IsConnected && !string.IsNullOrEmpty(PhotonNetwork.NickName))
        {
            loadingScreen.SetActive(true);
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.NickName + " Connected to the photon server");
        lobbyPanel.SetActive(true);
        enterGamePanel.SetActive(false);
        loadingScreen.SetActive(false);
    }

    public override void OnConnected()
    {
        Debug.Log("connected");
    }

    public void JoinRandomRoom()
    {
        loadingScreen.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);
        CreateRoom();
    }
    public override void OnJoinedRoom()
    {
        loadingScreen.SetActive(false);
        Debug.Log(PhotonNetwork.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name + "'s room");
        PhotonNetwork.LoadLevel("Game Scene");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name + "'s room " + "Player count: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }
    void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(PhotonNetwork.NickName, roomOptions);
    }
}
