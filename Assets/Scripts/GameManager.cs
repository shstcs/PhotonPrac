using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    private PillarSpawn _pillarSpawn = new();
    private PlayerSpawn _playerSpawn = new();

    private void Start()
    {
        SceneManager.sceneLoaded += CreateObject;
        DontDestroyOnLoad(gameObject);
    }

    private void CreateObject(Scene arg0, LoadSceneMode arg1)
    {
        if(arg0.name == "MainScene")
        {
            if(PhotonNetwork.IsMasterClient)
            {
                _pillarSpawn.CreatePillars();
            }
            _playerSpawn.CreatePlayer();
        }
    }
      
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= CreateObject;
    }
}
