using UnityEngine;
using System.Collections;
using System;

public class PlayerMovementCube : MonoBehaviour {

    private enum Direction { up, down, left, right };
    private Direction dir = Direction.up;
    public Rigidbody rigidbody;
    public float moveSpeed = 2.0f;
    public float rotationSpeed = 2.0f;
    public float fallingSpeed = 2.0f;
    private bool movementOn = false;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private bool falling = false;
    
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!movementOn && !falling)
        {
            CheckControls();
            CheckFalling();
        }
        else if(!movementOn && falling)
        {
            FallingCube();
        }

        if (movementOn)
        {
            MoveCube();
            CheckPosition();
        }
	}

    void CheckControls()
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

    void CheckFalling()
    {
        Vector3 down = transform.TransformDirection(Vector3.down);
        if (!Physics.Raycast(transform.position, down, 3))
        {
            falling = true;
        }
    }

    void CalculateMovement()
    {
        float posX = 0;
        float posZ = 0;
        float posY = rigidbody.position.y;
        float rotX = 0;
        float rotZ = 0;
        
        switch(dir)
        {
            case Direction.up: posZ = 1; rotX = 90; break;
            case Direction.down: posZ = -1; rotX = -90; break;
            case Direction.left: posX = -1; rotZ = 90; break;
            case Direction.right: posX = 1; rotZ = -90; break;
            default: Debug.Log("No direction detected!"); break;
        }
        targetRotation = rigidbody.rotation * Quaternion.Euler(rotX, 0, rotZ);
        targetPosition = rigidbody.position + new Vector3(posX, posY, posZ);
        movementOn = true;
    }

    void MoveCube()
    {
        rigidbody.position = Vector3.MoveTowards(rigidbody.position, targetPosition, moveSpeed);
        rigidbody.rotation = Quaternion.Lerp(rigidbody.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    void FallingCube()
    {
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    //CHeck the position of the cube end enable movement as soon as arrived
    void CheckPosition()
    {
        Vector3 currentPosition = rigidbody.position;
        if ((currentPosition.x > targetPosition.x - 0.08f && currentPosition.x < targetPosition.x + 0.08f) && (currentPosition.z > targetPosition.z - 0.08f && currentPosition.z < targetPosition.z + 0.08f))
        {
            Vector3 roundedPosition = new Vector3((int)Math.Round(targetPosition.x,0), targetPosition.y, (int)Math.Round(targetPosition.z, 0));
            rigidbody.position = roundedPosition;
            rigidbody.rotation = Quaternion.Euler(0,0,0);
            movementOn = false;
            rigidbody.velocity = Vector3.zero;
        }
    }
}
