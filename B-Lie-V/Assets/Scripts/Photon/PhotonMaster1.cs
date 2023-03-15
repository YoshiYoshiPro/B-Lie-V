using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class PhotonMaster1 : MonoBehaviourPunCallbacks
{
    public TextMeshPro statusText;
    private const int MaxPlayerPerRoom = 2;

    private string playerName;
    private string playerInfo;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Start is called before the first frame update
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void FindOpponent(string info)
    {
        playerInfo = info;
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    //Photonのコールバック
    public override void OnConnectedToMaster()
    {
        Debug.Log("マスターに繋ぎました。");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"{cause}の理由で繋げませんでした。");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("ルームを作成します。");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = MaxPlayerPerRoom });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("ルームに参加しました");
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        if (playerCount != MaxPlayerPerRoom)
        {
            statusText.text = "Wait Matching";
        }
        else
        {
            statusText.text = "Success Matching. Play Game";
            StartCoroutine(LoadGameScene());
        }
    }

    IEnumerator LoadGameScene()
    {
        // マッチングルームに参加したプレイヤーの情報をセット
        ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();
        customProperties.Add("PlayerInfo", playerInfo);
        Debug.Log(customProperties);
        PhotonNetwork.LocalPlayer.SetCustomProperties(customProperties);

        // ルームが満員になったらバトルシーンに移動
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            statusText.text = "対戦相手が揃いました。バトルシーンに移動します。";
            PhotonNetwork.LoadLevel("GameScene");
        }

        yield break;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == MaxPlayerPerRoom)
        {
            StartCoroutine(LoadGameScene());
        }
    }
}
