using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameSceneManager : MonoBehaviour
{

    private GameObject Master;
    private GameObject Player;
    [SerializeField] private Transform MasterTransform;
    [SerializeField] private Transform PlayerTransform;


    private void Start()
    {
        PhotonNetwork.IsMessageQueueRunning = true;



        if (PhotonNetwork.IsMasterClient)
        {
            var v = MasterTransform.position;
            Debug.Log("Master");
            Master = PhotonNetwork.Instantiate("Master", v, Quaternion.identity);
        }
        if (!PhotonNetwork.IsMasterClient)
        {
            var v = PlayerTransform.position;
            Debug.Log("Player");
            Player = PhotonNetwork.Instantiate("Player", v, Quaternion.identity);
        }
    }
}
