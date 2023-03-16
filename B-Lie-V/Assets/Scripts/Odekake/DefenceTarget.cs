using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceTarget : MonoBehaviour
{
    public enum MoveMode
    {
        Idle = 1,
        Follow = 2
    }


    public float move_speed = 3f;

    protected Rigidbody rb;
    protected Transform followTarget;
    public GameObject centerPos;
    protected MoveMode currentMoveMode;
    public Transform SphereSub;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        currentMoveMode = MoveMode.Idle;
    }

    void Update()
    {
        DoAutoMovement();
    }

    private void DoAutoMovement()
    {
        switch (currentMoveMode) {
            case MoveMode.Idle:
                rb.velocity = new Vector3(0, 0, 0);
                break;
            case MoveMode.Follow:
                if (Vector3.Distance(SphereSub.position, centerPos.transform.position) <= 0.2) {
                    rb.velocity = new Vector3(0, 0, 0);
                    break;
                }
                if (followTarget != null) {
                    Quaternion move_rotation = Quaternion.LookRotation(SphereSub.position - centerPos.transform.position, Vector3.up);
                    transform.rotation = Quaternion.Lerp(transform.rotation, move_rotation, 0.1f);
                    rb.velocity = transform.forward * move_speed;
                }

                break;
        }
    }

    public void OnEnterFollowTarget()
    {
        followTarget = null;

        if (currentMoveMode == MoveMode.Follow) {
            currentMoveMode = MoveMode.Idle;
        }
    }

    public void OnExitFollowTarget(Transform target)
    {
        followTarget = target;

        if (currentMoveMode == MoveMode.Idle) {
            currentMoveMode = MoveMode.Follow;
        }
    
    }

}

