using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class ParticleAnimation : MonoBehaviour
{
    public GameObject particleObject;
 
    private void OnCollisionEnter(Collision collision)
    {  
        if (collision.gameObject.tag == "Bottom") //Objectタグの付いたゲームオブジェクトと衝突したか判別
        {
            Instantiate(particleObject, this.transform.position, Quaternion.identity); //パーティクル用ゲームオブジェクト生成
            Destroy(this.gameObject); //衝突したゲームオブジェクトを削除
        }
    }
}
