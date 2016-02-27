using UnityEngine;
using System.Collections;

public class PlayerMovementCube : MonoBehaviour {

    private enum Direction { up, down, left, right };
    private Direction dir = Direction.up;
    public Rigidbody rigidbody;
    public float moveSpeed = 2.0f;
    public float rotationSpeed = 2.0f;
    private bool movementOn = false;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!movementOn)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                dir = Direction.up;
                CalculateMovement();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                dir = Direction.down;
                CalculateMovement();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                dir = Direction.right;
                CalculateMovement();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                dir = Direction.left;
                CalculateMovement();
            }
            else if (Input.GetButtonDown("Fire1"))
            {
                CalculateMovement();
            }
        }

        if (movementOn)
        {
            MoveCube();
        }
	}

    void CalculateMovement()
    {
        float posX = 0;
        float posZ = 0;
        float posY = rigidbody.position.y;
        float rotX = 0;
        float rotY = 0;
        float rotZ = 0;
        
        switch(dir)
        {
            case Direction.up: posZ = 1; rotX = 90; break;
            case Direction.down: posZ = -1; rotX = -90; break;
            case Direction.left: posX = -1; rotZ = 90; break;
            case Direction.right: posX = 1; rotZ = -90; break;
            default: Debug.Log("No direction detected!"); break;
        }
        Vector3 currentRotation = transform.eulerAngles;
        Debug.Log("Current rotation: " + currentRotation);
        targetRotation = rigidbody.rotation * Quaternion.Euler(rotX, rotY, rotZ);
        targetPosition = rigidbody.position + new Vector3(posX, posY, posZ);
        movementOn = true;
    }

    void MoveCube()
    {
        rigidbody.position = Vector3.MoveTowards(rigidbody.position, targetPosition, moveSpeed);
        rigidbody.rotation = Quaternion.Lerp(rigidbody.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
