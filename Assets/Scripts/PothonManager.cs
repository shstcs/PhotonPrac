using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PothonManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text _connectStateText;
    [SerializeField] private Button _connectButton;
    private PillarSpawn _pillarSpawn = new();
    private PlayerSpawn _playerSpawn = new();

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        _connectButton.interactable = false;
    }
    void Start()
    {
        _connectStateText.text = "Connecting";
        Connect();
    }
    
    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void Enter()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        _connectStateText.text = "Connected to Master";
        _connectButton.interactable = true;
    }

    public override void OnJoinedRoom()
    {
        _connectStateText.text = "Entered Room";
        Debug.Log("Entered Room");
        PhotonNetwork.LoadLevel("MainScene");
        //_playerSpawn.CreatePlayer();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        _connectStateText.text = "Not Entered Room. Create New Room";
        Debug.Log("Not Entered Room. Create New Room");
        PhotonNetwork.CreateRoom(null, new RoomOptions());
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"Disconnect to Server : {cause}");
    }
}
