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

    //Photon�̃R�[���o�b�N
    public override void OnConnectedToMaster()
    {
        Debug.Log("�}�X�^�[�Ɍq���܂����B");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"{cause}�̗��R�Ōq���܂���ł����B");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("���[�����쐬���܂��B");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = MaxPlayerPerRoom });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("���[���ɎQ�����܂���");
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
        // �}�b�`���O���[���ɎQ�������v���C���[�̏����Z�b�g
        ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();
        customProperties.Add("PlayerInfo", playerInfo);
        Debug.Log(customProperties);
        PhotonNetwork.LocalPlayer.SetCustomProperties(customProperties);

        // ���[���������ɂȂ�����o�g���V�[���Ɉړ�
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            statusText.text = "�ΐ푊�肪�����܂����B�o�g���V�[���Ɉړ����܂��B";
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
