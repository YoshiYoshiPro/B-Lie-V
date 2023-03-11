using UnityEngine;
using Photon.Pun;

public class BallLauncher : MonoBehaviourPunCallbacks
{
    public GameObject ballPrefab;

    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            photonView.RPC("LaunchBall", RpcTarget.All);
        }
    }

    [PunRPC]
    void LaunchBall()
    {
        GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        ball.GetComponent<Rigidbody>().AddForce(transform.right * 500f);
        
    }
}
