using UnityEngine;
using System.Collections;

public class ResetPosition : MonoBehaviour {

    public Transform startingPoint;

    // Use this for initialization
    void Start () {
        ResetObjectPosition();
	}
	
	public void ResetObjectPosition()
    {
        transform.position = new Vector3(startingPoint.position.x, 0.75f, startingPoint.position.z);
    }
}
