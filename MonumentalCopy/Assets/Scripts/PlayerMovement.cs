using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    private LayerMask floorLayer; //The name of the layer affected by raycast
    private bool moveOn = false; //When this variable is on the player starts moving
    Vector3 targetPosition; //positoin is determined by raycast and the player walks towards this ^position

    void Start()
    {
        floorLayer = LayerMask.NameToLayer("Floor");
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
                    moveOn = true;
                    Debug.Log(hit.point);
                    
                }
            }
        }

        if(moveOn)
        {
            MovePlayer();
        }

     }

    private void MovePlayer()
    {
        transform.LookAt(targetPosition);
        
    }
}

