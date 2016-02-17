using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    private LayerMask floorLayer; //The name of the layer affected by raycast
    Vector3 targetPosition; //positoin is determined by raycast and the player walks towards this ^position
    NavMeshAgent navAgent;

    void Start()
    {
        floorLayer = LayerMask.NameToLayer("Floor");
        navAgent = GetComponent<NavMeshAgent>();
    }

	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.layer == floorLayer)
                {
                    targetPosition = hit.point;
                    Debug.Log(targetPosition);
                    MovePlayer();
                }
            }
        }


     }

    private void MovePlayer()
    {
        transform.LookAt(targetPosition);
        float tempY = transform.localEulerAngles.y;
        transform.localEulerAngles = new Vector3(0, tempY, 0);
        navAgent.destination = targetPosition;     
        
    }
}

