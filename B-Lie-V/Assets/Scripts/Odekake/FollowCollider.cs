using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCollider : MonoBehaviour
{
    public DefenceTarget defenceTarget;

    public void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player") {
            defenceTarget.OnExitFollowTarget(c.transform);
        }     
    }

    public void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player") {
            defenceTarget.OnEnterFollowTarget();
        }
    }
}

