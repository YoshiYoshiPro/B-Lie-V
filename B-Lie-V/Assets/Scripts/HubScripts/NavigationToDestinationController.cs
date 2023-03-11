using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationToDestinationController : MonoBehaviour
{
    public Transform destination_GFO;

    [SerializeField] private GameObject targetBuilding;
    [SerializeField] private Material hightlightMaterial;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private Transform cursor;

    private NavMeshAgent navMeshAgent;
    private float distanceThreshold = 5.0f;

    private void Start() 
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updatePosition = false;
        navMeshAgent.updateRotation = false;    
    }

    public void StartNavigation()
    {
        //ナビゲーション開始、カーソルをアクティブにする
        navMeshAgent.SetDestination(destination_GFO.position);
        cursor.gameObject.SetActive(true);
        //向かう先の建物のマテリアルをハイライトさせる
        targetBuilding.GetComponent<Renderer>().material = hightlightMaterial;

        StartCoroutine(NavigationToDestination());
    }

    IEnumerator NavigationToDestination()
    {
        while(true)
        {
            cursor.transform.position = new Vector3(mainCamera.position.x, mainCamera.position.y - 0.3f, mainCamera.position.z) + mainCamera.transform.forward * 1.2f;

            float distance = (this.transform.position - destination_GFO.position).sqrMagnitude;
            if(distance < distanceThreshold * distanceThreshold)
            {
                cursor.gameObject.SetActive(false);
                yield break;
            }

            if(!navMeshAgent.pathPending)
            {
                cursor.rotation = Quaternion.LookRotation(navMeshAgent.steeringTarget - transform.position, Vector3.up);
                navMeshAgent.nextPosition = transform.position;
                navMeshAgent.SetDestination(destination_GFO.position);
            }

            yield return new WaitForSeconds(0.2f);
        }
    }
}
