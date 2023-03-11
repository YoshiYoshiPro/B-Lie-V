using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationUIController : MonoBehaviour
{
    [SerializeField] private NavigationToDestinationController navDestinationController;
    [SerializeField] private GameObject navigationUICanvas;
    [SerializeField] private GameObject confirmationPanel;
    [SerializeField] private Transform mainCamera;

    private bool canvasActive = false;

    private void Update() 
    {
        if(OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            canvasActive = !canvasActive;
            navigationUICanvas.SetActive(canvasActive);

            if(mainCamera != null)
            {
                navigationUICanvas.transform.position = mainCamera.transform.position + mainCamera.transform.forward * 1.5f;
                navigationUICanvas.transform.localRotation = Quaternion.Euler(0, mainCamera.transform.rotation.eulerAngles.y, 0);
            }
        }
    }

    public void OnToGrandFrontOSAKAButton()
    {
        confirmationPanel.SetActive(true);
    }

    public void OnOKButton()
    {
        confirmationPanel.SetActive(false);
        navigationUICanvas.SetActive(false);
        navDestinationController.StartNavigation();
    }

    public void OnBackButton(GameObject panel)
    {
        panel.SetActive(false);
    }
}
