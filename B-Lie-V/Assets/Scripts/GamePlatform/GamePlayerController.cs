using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayerController : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Rigidbody playerRigidbody;
    private float depthThreshold = 20.0f;

    private void OnCollisionEnter(Collision other) 
    {
        if(gameObject.tag != "GameMaster")
        {
            if (other.gameObject.CompareTag("Barrel"))
            {
                BackToStartPoint();
            }
        }

    }

    private void LateUpdate() 
    {
        //現在のY座標を取得
        float currentPositionY = transform.position.y;

        if (currentPositionY < depthThreshold)
        {
            // Reset any velocity from falling or moving when respawning to original location
            if (playerRigidbody != null)
            {
                playerRigidbody.velocity = Vector3.zero;
                playerRigidbody.angularVelocity = Vector3.zero;
            }

            BackToStartPoint();
        }
    }

    private void BackToStartPoint()
    {
        transform.position = startPoint.position;
        transform.rotation = Quaternion.identity;
    }

}
