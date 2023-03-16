using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class offsetposition : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
 
    void Update()
    {
       offset = new Vector3(2, 2, 2);
       this.transform.position = target.position + offset;
    }
}
