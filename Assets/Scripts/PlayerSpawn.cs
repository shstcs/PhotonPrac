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

        GameObject player = Instantiate(Resources.Load<GameObject>("Player"));
        player.transform.SetParent(_map.transform);
        player.transform.position = _spawnList[Random.Range(0, 5)].transform.position;

        Debug.Log("Player Created");
    }
}
