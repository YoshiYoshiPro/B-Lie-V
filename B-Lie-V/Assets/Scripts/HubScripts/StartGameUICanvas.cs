using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameUICanvas : MonoBehaviour
{
    [SerializeField] private GameObject confirmationPlate;
    [SerializeField] private GameObject choiceRolePlate;
    [SerializeField] private GameObject YesButton;
    [SerializeField] private GameObject NoButton;

    public void IsCanvasActive(bool isActive)
    {
        confirmationPlate.SetActive(isActive);
    }

    public void OnYesButton()
    {
        //confirmationPlate.SetActive(false);
        YesButton.SetActive(false);
        NoButton.SetActive(false);
    }

    public void OnNoButton()
    {

    }

    public void OnChoicePlayerButton()
    {
        confirmationPlate.SetActive(false);
        choiceRolePlate.SetActive(false);
    }

    public void OnChoiceGameMasterButton()
    {
        confirmationPlate.SetActive(false);
        choiceRolePlate.SetActive(false);
    }

    public void OnBackButton()
    {
        choiceRolePlate.SetActive(false);
        confirmationPlate.SetActive(true);
    }
}
