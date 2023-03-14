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
    private ContinuousMoveProviderBase moveProvider;
    private ContinuousTurnProviderBase turnProvider;
    private bool isRelayPoint = false;
    
    private void Start() 
    {
        playerRigidBody = player.GetComponent<Rigidbody>();
        moveProvider = GetComponentInChildren<ContinuousMoveProviderBase>();
        turnProvider  = GetComponentInChildren<ContinuousTurnProviderBase>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            currentCanvas.SetActive(true);
            StartCoroutine(LadderAction());
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            currentCanvas.SetActive(false);
            StopCoroutine(LadderAction());
        }
    }

    IEnumerator LadderAction()
    {
        while(true)
        {
            if(OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
            {
                playerRigidBody.constraints = RigidbodyConstraints.FreezePosition;
                moveProvider.enabled = false;
                turnProvider.enabled = false;
                player.transform.position = relayPoint.transform.position;
                isRelayPoint = true;
            }

            if(OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickUp, OVRInput.Controller.RTouch) && isRelayPoint)
            {
                isRelayPoint = false;
                player.transform.position = aboveTorus.transform.position;
                playerRigidBody.constraints = RigidbodyConstraints.None;
                moveProvider.enabled = true;
                turnProvider.enabled = true;
                yield break;
            }

            if(OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickDown, OVRInput.Controller.LTouch) && isRelayPoint)
            {
                isRelayPoint = false;
                player.transform.position = underTorus.transform.position;
                playerRigidBody.constraints = RigidbodyConstraints.None;
                moveProvider.enabled = true;
                turnProvider.enabled = true;
                yield break;
            }

            yield return new WaitForSeconds(0.01f);
        }
    }
}
