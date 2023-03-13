using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarelGenerationController : MonoBehaviour
{
    [SerializeField] private GameObject barrelPrefab; 

    private float barrelRadius = 0.01f;
    private void Start() 
    {
        StartCoroutine(GenerateBarrel());
    }

    /// <summary>
    /// 5秒ごとに樽をスポーンさせる
    /// </summary>
    /// <returns></returns>
    IEnumerator GenerateBarrel()
    {
        RaycastHit hit;
        while(true)
        {
            //樽が下になかったら追加でスポーン
            if(Physics.SphereCast(transform.position, barrelRadius, Vector3.down, out hit))
            {
                if(!hit.collider.transform.gameObject.CompareTag("Barrel"))
                {
                    GameObject barrel = Instantiate(barrelPrefab, transform.position, Quaternion.identity) as GameObject;
                    barrel.transform.localScale = new Vector3(105, 105, 105);
                }
            }

            yield return new WaitForSeconds(5.0f);
        }
    }
}
