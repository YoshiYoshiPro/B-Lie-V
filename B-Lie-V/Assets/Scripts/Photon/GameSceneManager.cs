using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameSceneManager : MonoBehaviourPunCallbacks
{

    private GameObject Master;
    private GameObject Player;

    private string playerName;

    [SerializeField] private Transform cameraRig;
    [SerializeField] private Transform[] playerPositions;

    [SerializeField] private Transform MasterTransform;
    [SerializeField] private Transform PlayerTransform;
    [SerializeField] private GameObject MasterController;

    [SerializeField] private Transform SpawnPosiiton;
    [SerializeField] private GameObject Barrel;

    

    private string testInfo;
    private void Start()
    {
        PhotonNetwork.IsMessageQueueRunning = true;


        // ローカルプレイヤーの情報を取得
        ExitGames.Client.Photon.Hashtable localPlayerCustomProperties = PhotonNetwork.LocalPlayer.CustomProperties;
        string localPlayerInfo = (string)localPlayerCustomProperties["PlayerInfo"];

        // 他プレイヤーの情報を取得
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (!player.Equals(PhotonNetwork.LocalPlayer))
            {
                ExitGames.Client.Photon.Hashtable opponentCustomProperties = player.CustomProperties;
                string opponentInfo = (string)opponentCustomProperties["PlayerInfo"];
                Debug.Log(opponentInfo);
            }
        }

        if (PhotonNetwork.IsMasterClient)
        {
            MasterController.GetComponent<GamePlayerController>().enabled = false;
            MasterController.GetComponent<PlayerJumping>().enabled = false;
            MasterController.tag = "GameMaster";
            MasterController.layer = 0;
            var v = MasterTransform.position;
            Debug.Log("Master");
            //playerName = PhotonNetwork.NickName;
            //testInfo = (string)PhotonNetwork.LocalPlayer.CustomProperties["TestInfo"];
            //Debug.Log("Player Name: " + playerName);
            //Debug.Log("aaaaa: " + testInfo);
            Master = PhotonNetwork.Instantiate("NetworkedController", v, Quaternion.identity);
            //testInfo = PhotonNetwork.LocalPlayer.CustomProperties["PlayerInfo"].ToString();
            //Debug.Log("Test Info :" + testInfo);

            cameraRig.position = playerPositions[0].position;

            //Master.gameObject.AddComponent<BarelGenerationController>();
            //Master.gameObject.GetComponent<BarelGenerationController>().barrelPrefab = Barrel;
            //Master.gameObject.GetComponent<BarelGenerationController>().SpawnPositon = SpawnPosiiton;
        }
        if (!PhotonNetwork.IsMasterClient)
        {
            MasterController.tag = "Player";
            MasterController.layer = 9;
            var v = PlayerTransform.position;
            Debug.Log("Player");
            Player = PhotonNetwork.Instantiate("NetworkedController", v, Quaternion.identity);
            cameraRig.position = playerPositions[1].position;

        }
    }
}
