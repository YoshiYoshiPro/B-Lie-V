using UnityEngine;
using Photon.Pun;

public class Ball : MonoBehaviourPunCallbacks, IPunObservable
{
    private Vector3 networkPosition;
    private Quaternion networkRotation;

    public int HitCount = 0;
    private HitController hitController;

    private void Start()
    {
        hitController = GameObject.Find("HitController").GetComponent<HitController>();
        HitCount = hitController.HitCount;

    }
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            networkPosition = (Vector3)stream.ReceiveNext();
            networkRotation = (Quaternion)stream.ReceiveNext();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.root.tag == "Challenger")
        {
            Destroy(gameObject);
            HitCount++;
            Debug.Log(HitCount);
            hitController.HitCount = HitCount;
        }
    }

    /*    void FixedUpdate()
        {
            if (!photonView.IsMine)
            {
                transform.position = Vector3.Lerp(transform.position, networkPosition, Time.deltaTime * 10f);
                transform.rotation = Quaternion.Lerp(transform.rotation, networkRotation, Time.deltaTime * 10f);
            }
        }*/
}


