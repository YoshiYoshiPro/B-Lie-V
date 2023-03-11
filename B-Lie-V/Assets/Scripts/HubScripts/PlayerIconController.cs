using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerIconController : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private Vector3 StandardScale = new Vector3(20, 0.5f, 20);
    private Vector3 newStandardScale = new Vector3(20, 0.5f, 20);
    private void Start() 
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.DOFade(0, 1.5f).SetEase(Ease.InQuart).SetLoops(-1, LoopType.Yoyo);                     
    }

    private void Update() 
    {
        this.transform.localScale = newStandardScale;
    }

    public void StandardScaleChange(float fieldOfViewValue)
    {
        Vector3 AddScaleValue = Vector3.zero;
        if(fieldOfViewValue == 30) AddScaleValue = new Vector3(-10, 0, -10);
        if(fieldOfViewValue > 30) AddScaleValue = new Vector3(-5, 0, -5);
        if(fieldOfViewValue > 50) AddScaleValue = Vector3.zero;
        if(fieldOfViewValue > 70) AddScaleValue = new Vector3(5, 0, 5);
        if(fieldOfViewValue > 90) AddScaleValue = new Vector3(10, 0, 10);
        if(fieldOfViewValue == 110) AddScaleValue = new Vector3(15, 0, 15);
        
        newStandardScale = StandardScale + AddScaleValue;
    }

}
