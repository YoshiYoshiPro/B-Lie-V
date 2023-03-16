using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatGPTConversationUIController : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject conversationCanvas;
    private bool canvasActive = false;

    private void Update() 
    {
        if(OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch))
        {
            canvasActive = !canvasActive;
            conversationCanvas.SetActive(canvasActive);

            if (mainCamera != null)
            {
                conversationCanvas.transform.position = mainCamera.transform.position + mainCamera.transform.forward * 2.0f;
                conversationCanvas.transform.rotation = Quaternion.Euler(0, mainCamera.transform.rotation.eulerAngles.y, 0);
            }
        }
    }
}
