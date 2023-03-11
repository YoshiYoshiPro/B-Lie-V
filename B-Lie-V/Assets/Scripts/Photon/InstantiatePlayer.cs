using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

//PUN�̃R�[���o�b�N���󂯎���悤�ɂ���ׂ�MonoBehaviourPunCallbacks
public class InstantiatePlayer : MonoBehaviourPunCallbacks
{
    void Start()
    {
        //�}�X�^�[�T�[�o�[�ɐڑ�
        PhotonNetwork.ConnectUsingSettings();
    }

    //�}�X�^�[�T�[�o�[�ɐڑ������������ɌĂ΂��
    public override void OnConnectedToMaster()
    {
        //Room�Ƃ������O�̃��[�����쐬����A�����̏ꍇ�͎Q������
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
    }

    //���[���ւ̐ڑ�������������Ă΂��



    public override void OnJoinedRoom()
    {
        //Player�𐶐�������ʂ������_���Ɍ��߂�
        var position = new Vector3(Random.Range(-3f, 3f), 0.5f, Random.Range(-3f, 3f));

        //Resources�t�H���_����"Player"��T���Ă��Ă���𐶐�
        PhotonNetwork.Instantiate("Player", position, Quaternion.identity);
    }
}