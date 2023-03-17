using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRespawnController : MonoBehaviour
{
    public LayerMask groundLayer;

    [SerializeField] private Transform centerPos;
    [SerializeField] private Transform playerPos;

    private Vector3 latestRespawnPosition;
    private Quaternion latestRespawnRotation;
    private Rigidbody rigidBody;
    private float depthThreshold = -5.0f;
    private float distanceThreshold = 10.0f;

     private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        //地面にいる場合、最新の現在地を更新
        RaycastHit hit;
        if (Physics.Raycast(centerPos.transform.position, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            latestRespawnPosition = hit.point + new Vector3(0, 0.8f, 0);
            latestRespawnRotation = transform.rotation;
        }

        //現在のY座標を取得
        float currentPositionY = transform.position.y;

        if (currentPositionY < depthThreshold)
        {
            // Reset any velocity from falling or moving when respawning to original location
            if (rigidBody != null)
            {
                rigidBody.velocity = Vector3.zero;
                rigidBody.angularVelocity = Vector3.zero;
            }

            transform.position = latestRespawnPosition;
            transform.rotation = latestRespawnRotation;
        }

        if(Vector3.Distance(playerPos.position, centerPos.position) > distanceThreshold)
        {
            transform.position = playerPos.position;
            transform.rotation = playerPos.rotation;
        }
    }

}
