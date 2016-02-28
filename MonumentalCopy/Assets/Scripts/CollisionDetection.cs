using UnityEngine;
using System.Collections;

public class CollisionDetection : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "FallCollider")
        {
            GetComponent<ResetPosition>().ResetObjectPosition();
            GetComponent<PlayerMovementCube>().ResetSettings();
        }
    }
}
