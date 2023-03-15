using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchedController : MonoBehaviour
{
    [SerializeField] private GamePlayManager gamePlayManager;

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Controller"))
        {
            gamePlayManager.PlayerTouchGameMaster();
        }
    }
}
