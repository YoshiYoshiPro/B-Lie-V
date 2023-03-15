using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField] private PlayerJumping playerJumping;
    [SerializeField] private TextMeshProUGUI GMText;

    private void Start() 
    {
        playerJumping.ChangeRayCastLength();
    }

    public void PlayerTouchGameMaster()
    {
        GMText.text = "Player Wins!!";
    }
}
