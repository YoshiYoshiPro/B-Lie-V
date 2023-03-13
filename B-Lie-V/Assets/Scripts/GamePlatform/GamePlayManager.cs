using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField] private PlayerJumping playerJumping;

    private void Start() 
    {
        playerJumping.ChangeRayCastLength();
    }
}
