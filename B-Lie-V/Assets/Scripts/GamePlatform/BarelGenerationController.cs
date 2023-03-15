using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BarelGenerationController : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject barrelPrefab; 

    private float barrelRadius = 0.01f;

    private float timeOut = 5.0f;
    private float timeElapsed;

    private void Start() 
    {
        //StartCoroutine(GenerateBarrel());
        if (!photonView.IsMine)
        {
            return;
        }

        photonView.RPC("SpawnBarrel", RpcTarget.All);
        
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        timeElapsed += Time.deltaTime;

        if (timeElapsed >= timeOut)
        {
            RaycastHit hit;
            //樽が下になかったら追加でスポーン
            if (Physics.SphereCast(transform.position, barrelRadius, Vector3.down, out hit))
            {
                if (!hit.collider.transform.gameObject.CompareTag("Barrel"))
                {
                    photonView.RPC("SpawnBarrel", RpcTarget.All);
                }
            }
            timeElapsed = 0.0f;
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

    [PunRPC]
    void SpawnBarrel()
    {
        GameObject barrel = Instantiate(barrelPrefab, transform.position, Quaternion.identity) as GameObject;
        barrel.transform.localScale = new Vector3(105, 105, 105);
    }
}
