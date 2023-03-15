using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTriggerContorller : MonoBehaviour
{
    [SerializeField] private StartGameUICanvas startGameUICanvas;

    private void OnTriggerStay(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            startGameUICanvas.IsCanvasActive(true);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            startGameUICanvas.IsCanvasActive(false);
        }
    }
}
