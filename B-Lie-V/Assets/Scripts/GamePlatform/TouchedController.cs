using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchedController : MonoBehaviour
{
    [SerializeField] private GamePlayManager gamePlayManager;

    private void OnCollisitonEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            gamePlayManager.PlayerVictory();
        }
    }
}
