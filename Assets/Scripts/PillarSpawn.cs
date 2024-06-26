using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarSpawn : MonoBehaviour
{
    private List<GameObject> _pillarList = new List<GameObject>();
    private GameObject _map;
    public void CreatePillars()
    {
        int count = Random.Range(20, 25);
        float[] xPosLimit = { -39, 39 };
        float[] yPosLimit = {-29, 29};

        _map = GameObject.Find("Map");

        if(_map.transform.Find("Pillar(Clone)")  == null)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject go = PhotonNetwork.Instantiate("Pillar",
                    new Vector3(Random.Range(xPosLimit[0], xPosLimit[1]), Random.Range(yPosLimit[0], yPosLimit[1]), 0),
                    Quaternion.identity);
                go.transform.SetParent(_map.transform);
                _pillarList.Add(go);  
            }
        }
        
        Debug.Log("Create Pillars");
    }
}
