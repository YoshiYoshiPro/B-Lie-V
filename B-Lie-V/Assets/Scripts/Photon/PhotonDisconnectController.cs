using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonDisconnectController : MonoBehaviourPunCallbacks
{
    private bool isLeaving = false;

    void Update()
    {
        if (isLeaving && !PhotonNetwork.IsMessageQueueRunning)
        {
            PhotonNetwork.Disconnect();
        }
    }

    public void LeaveRoom()
    {
        isLeaving = true;
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.LoadLevel("Hub");
        }
        else
        {
            SceneManager.LoadScene("Hub");
        }
    }
}
