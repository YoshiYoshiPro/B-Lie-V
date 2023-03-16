using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField] private PlayerJumping playerJumping;
    [SerializeField] private Timer timer;
    [SerializeField] private TextMeshProUGUI GMText;
    [SerializeField] private TextMeshProUGUI playerText;
    [SerializeField] private GameObject[] backHubButtonCanvases = new GameObject[2];
    [SerializeField] private AudioClip gameEndBGM;

    private AudioSource audioSource;

    private void Start() 
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        playerJumping.ChangeRayCastLength();
    }

    public void PlayerVictory()
    {
        timer.GameEnd();
        BackHubButtonActive();
        PlayGameEndMusic();
        GMText.text = "Player Wins!!";
        playerText.text = "Player Wins!!";
    }

    public void GameMasterVictory()
    {
        timer.GameEnd();
        BackHubButtonActive();
        PlayGameEndMusic();
        GMText.text = "GameMaster Wins!!";
        playerText.text = "GameMaster Wins!!";
    }

    private void BackHubButtonActive()
    {
        foreach(GameObject button in backHubButtonCanvases)
        {
            button.SetActive(true);
        }
    }

    private void PlayGameEndMusic()
    {
        audioSource.Stop();
        audioSource.clip = gameEndBGM;
        audioSource.Play();
    }
}
