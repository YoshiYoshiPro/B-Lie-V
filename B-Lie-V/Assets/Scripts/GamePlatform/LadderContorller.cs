using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LadderContorller : MonoBehaviour
{
    [SerializeField] private GameObject currentCanvas;
    [SerializeField] private GameObject underTorus;
    [SerializeField] private GameObject aboveTorus;
    [SerializeField] private GameObject player; 
    [SerializeField] private GameObject relayPoint;

    private Rigidbody playerRigidBody;
    private float longPressTime = 0;
    
    private void Start() 
    {
        playerRigidBody = player.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            currentCanvas.SetActive(true);
        }
    }
    
    private void OnTriggerStay(Collider other) 
    {
        if(OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickUp, OVRInput.Controller.RTouch))
        {
            player.transform.position = aboveTorus.transform.position;
        }

        if(OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickDown, OVRInput.Controller.RTouch))
        {
            player.transform.position = underTorus.transform.position;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            currentCanvas.SetActive(false);
        }
    }
}
