using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricPlayerMovementController : MonoBehaviour
{

    public float movementSpeed = 1f;
    IsometricCharacterRenderer isoRenderer;
    Rigidbody2D rbody;
    private Vector2 currentDirection;

    [SerializeField] private FixedJoystick joystick;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
        currentDirection = new Vector2(0, 1);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 currentPos = rbody.position;

        // input from keyboard
        float horizontalInputKeyboard = Input.GetAxis("Horizontal");
        float verticalInputKeyboard = Input.GetAxis("Vertical");
        Vector2 inputKeyboard = new Vector2(horizontalInputKeyboard, verticalInputKeyboard);

        // input from joystick
        float horizontalInputJoystick = joystick.Horizontal;
        float verticalInputJoystick = joystick.Vertical;
        Vector2 inputJoystick = new Vector2(horizontalInputJoystick, verticalInputJoystick);

        // Decide if joystick move then choose joystick
        Vector2 inputVector = (inputJoystick.magnitude > 0.01f) ? inputJoystick : inputKeyboard;

        // move character
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * movementSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        isoRenderer.SetDirection(movement);
        rbody.MovePosition(newPos);

        // Save Direction
        if (inputVector.magnitude > 0.05f) currentDirection = inputVector;
    }

    public Vector2 getCurrentDirection()
    {
        return currentDirection.normalized;
    }
}
