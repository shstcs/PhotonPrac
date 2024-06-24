using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    private GameObject _map;
    private GameObject[] _spawnList;

    public void CreatePlayer()
    {
        _map = GameObject.Find("Map");
        _spawnList = _map.GetComponent<PlayerSpawnPosition>().spawnPositions;

        GameObject player = PhotonNetwork.Instantiate("Player", _spawnList[Random.Range(0, 5)].transform.position,Quaternion.identity);
        player.transform.SetParent(_map.transform);

        SpriteRenderer playerSprite = player.GetComponent<SpriteRenderer>();
        float[] randomColor = {Random.Range(0,255), Random.Range(0, 255), Random.Range(0, 255)};
        playerSprite.color = new Color(randomColor[0] / 256f, randomColor[1] / 256f, randomColor[2] / 256f);

        Debug.Log("Player Created");
    }
}
