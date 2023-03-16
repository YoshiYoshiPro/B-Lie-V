using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BarelGenerationController : MonoBehaviourPunCallbacks
{
    public GameObject barrelPrefab; 

    private float barrelRadius = 0.01f;

    private float timeOut = 5.0f;
    private float timeElapsed;

    public Transform SpawnPositon;
    private void Start() 
    {
        //StartCoroutine(GenerateBarrel());
        if (!photonView.IsMine)
        {
            return;
        }


    }

    public void SpawnButton()
    {

        if (PhotonNetwork.IsMasterClient)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= timeOut)
            {
                RaycastHit hit;
                //樽が下になかったら追加でスポーン
                if (Physics.SphereCast(transform.position, barrelRadius, Vector3.down, out hit))
                {
                    Debug.Log(hit.collider.transform.gameObject.name);
                    if (!hit.collider.transform.gameObject.CompareTag("Barrel"))
                    {
                        SpawnBarrel();
                    }
                }
                timeElapsed = 0.0f;
            }
        }
    }
    /// <summary>
    /// 5秒ごとに樽をスポーンさせる
    /// </summary>
    /// <returns></returns>
/*    IEnumerator GenerateBarrel()
    {
        RaycastHit hit;
        while(true)
        {
            //樽が下になかったら追加でスポーン
            if(Physics.SphereCast(transform.position, barrelRadius, Vector3.down, out hit))
            {
                if(!hit.collider.transform.gameObject.CompareTag("Barrel"))
                {
                    photonView.RPC("SpawnBarrel", RpcTarget.All);
                }
            }

            yield return new WaitForSeconds(5.0f);
        }
    }*/

    public void SpawnBarrel()
    {
        GameObject barrel = PhotonNetwork.Instantiate(barrelPrefab.name, SpawnPositon.position, Quaternion.identity) as GameObject;
        barrel.transform.localScale = new Vector3(105, 105, 105);
    }
}
