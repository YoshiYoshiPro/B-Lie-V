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


    public float move_speed;

    protected Rigidbody rb;
    protected Transform followTarget;
    public GameObject centerPos;
    protected MoveMode currentMoveMode;
    public Transform avatorTargetPos;

    [SerializeField] private Animator animator;

    private bool isArrival = false;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        currentMoveMode = MoveMode.Idle;
    }

    void Update()
    {
        if(isArrival) currentMoveMode = MoveMode.Idle;
        DoAutoMovement();
    }

    private void DoAutoMovement()
    {
        switch (currentMoveMode) {
            case MoveMode.Idle:
                rb.velocity = new Vector3(0, 0, 0);
                animator.SetTrigger("Idle");
                //Debug.Log("11111111111");
                break;
            case MoveMode.Follow:
                if (Vector3.Distance(avatorTargetPos.position, centerPos.transform.position) <= 0.2) {
                    rb.velocity = new Vector3(0, 0, 0);
                    animator.SetTrigger("Idle");
                    //Debug.Log("22222222222");

                    break;
                }

                if (followTarget != null) {
                    Quaternion move_rotation = Quaternion.LookRotation(avatorTargetPos.position - centerPos.transform.position, Vector3.up);
                    transform.rotation = Quaternion.Lerp(transform.rotation, move_rotation, 0.1f);
                    transform.rotation = new Quaternion(0, transform.rotation.y,0, transform.rotation.w);

                    if (Vector3.Distance(avatorTargetPos.position, centerPos.transform.position) <= 0.5)
                    {
                        rb.velocity = transform.forward * (move_speed - 0.3f);
                    }
                    else
                    {
                        rb.velocity = transform.forward * move_speed;
                    }
                    
                    animator.SetTrigger("Move");
                    //Debug.Log("33333333333333");

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

    public void ArriveDestinaiton()
    {
        isArrival = true;
    }

}

