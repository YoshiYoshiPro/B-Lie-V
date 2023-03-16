using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private Camera mapCamera;
    [SerializeField] private PlayerIconController playerIconController;
    [SerializeField] private GameObject canvas;
    [SerializeField] private RectTransform menuPlate;
    [SerializeField] private RectTransform mapPlate;
    [SerializeField] private GameObject rightController;
    private bool canvasActive = false;


    private void Update() 
    {
        //マップなどのメニューバーの表示・非表示をする。(Bボタン)
        if(OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        {
            canvasActive = !canvasActive;
            canvas.SetActive(canvasActive);

            if (mainCamera != null)
            {
                canvas.transform.position = mainCamera.transform.position + mainCamera.transform.forward * 2.0f;
                canvas.transform.rotation = Quaternion.Euler(0, mainCamera.transform.rotation.eulerAngles.y, 0);
            }
        }

        //マップの移動操作。長押しを検知する。(左右トリガー)
        if(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch) || OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            //コントローラーの向いている方向にレイを飛ばす
            Ray ray = new Ray(rightController.transform.position, rightController.transform.forward);

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 7.0f))
            {
                //ヒットして、ボタンであったら操作
                if(hit.collider.gameObject.CompareTag("LeftMapButton"))
                {
                    mapCamera.transform.localPosition += new Vector3(-5, 0, 0);
                }

                if(hit.collider.gameObject.CompareTag("RightMapButton"))
                {
                    mapCamera.transform.localPosition += new Vector3(5, 0, 0);
                }

                if(hit.collider.gameObject.CompareTag("UpMapButton"))
                {
                    mapCamera.transform.localPosition += new Vector3(0, 0, 5);
                }

                if(hit.collider.gameObject.CompareTag("DownMapButton"))
                {
                    mapCamera.transform.localPosition += new Vector3(0, 0, -5);
                }
            }

        }
    }

    /// <summary>
    /// スタート地点にリスポーンする際にメニューバーを非表示させる
    /// </summary>
    public void SetCanvasActiveFalse()
    {
        canvas.SetActive(false);
        canvasActive = !canvasActive;
    }

    /// <summary>
    /// Display A Mapボタンが押されたとき
    /// </summary>
    public void OnDisplayMapButton()
    {
        menuPlate.DOAnchorPos(new Vector2(-400, 0), 1.0f).SetEase(Ease.OutCubic);

        mapPlate.gameObject.SetActive(true);
        mapPlate.DOAnchorPos(new Vector2(345, 0), 1.0f).SetEase(Ease.OutCubic);
    }

    /// <summary>
    /// マップで拡大ボタンが押されたとき
    /// </summary>
    public void OnExpansionButton()
    {
        float newValue = mapCamera.fieldOfView + 10.0f;
        if(newValue > 110.0f)
        {
            newValue = 110.0f;
        }
        mapCamera.fieldOfView = newValue;

        playerIconController.StandardScaleChange(newValue);
    }

    /// <summary>
    /// マップで縮小ボタンが押されたとき
    /// </summary>
    public void OnContractionButton()
    {
        float newValue = mapCamera.fieldOfView - 10.0f;
        if(newValue < 30.0f)
        {
            newValue = 30.0f;
        }
        mapCamera.fieldOfView = newValue;

        playerIconController.StandardScaleChange(newValue);
    }

    /// <summary>
    /// 戻るボタンが押されたとき
    /// </summary>
    /// <param name="otherPlate"></param>
    public void OnBackButton(RectTransform otherPlate)
    {
        menuPlate.DOAnchorPos(new Vector2(0, 0), 0.4f).SetEase(Ease.OutCubic);

        otherPlate.DOAnchorPos(new Vector2(0, 0), 0.4f).SetEase(Ease.OutCubic)
        .OnComplete(() => otherPlate.gameObject.SetActive(false));
    }

}

