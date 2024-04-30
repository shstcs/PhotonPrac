using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private PillarSpawn _pillarSpawn = new PillarSpawn();
    private PlayerSpawn _playerSpawn = new PlayerSpawn();

    private void Start()
    {
        SceneManager.sceneLoaded += CreatePillar;
        DontDestroyOnLoad(gameObject);
    }

    private void CreatePillar(Scene arg0, LoadSceneMode arg1)
    {
        if(arg0.name == "MainScene")
        {
            Debug.Log("Main Scene");
            _pillarSpawn.CreatePillars();
            _playerSpawn.CreatePlayer();
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= CreatePillar;
    }
}
