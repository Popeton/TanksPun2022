using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform spawn1;
    [SerializeField] private Transform spawn2;
    void Start()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("No existe el juegador ");
        }
        else
        {
            Transform spawnPoint = (PhotonNetwork.IsMasterClient) ? spawn1 : spawn2;
            object[] initData = new object[1];
            initData[0] = "Data instanciacion ";

            PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, Quaternion.identity,0,initData);
        }

       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
