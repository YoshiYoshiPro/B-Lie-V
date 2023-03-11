using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
public class HitController : MonoBehaviourPunCallbacks 
{ 
    public int HitCount = 0;
    [SerializeField] TextMeshProUGUI text;
    private void FixedUpdate()
    {
        text.text = HitCount.ToString();
    }
}
