using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;

    private Vector2 movementVector, lookVector; 

    public float MovementSpeed = 10f, RotationSpeed = 5f;

    public float rotationY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Move(movementVector);

        Rotate(lookVector);
    }

    // ------------------------- PLAYER MOVEMENT INPUTS / METHODS ---------------------------------
    //Both are plugged into Player Input component
    public void InputMove(InputAction.CallbackContext context)
    {
        movementVector = context.ReadValue<Vector2>();
    }
    public void InputLook(InputAction.CallbackContext context)
    {
        lookVector = context.ReadValue<Vector2>();
    }

    // Calculates players movement
    public void Move(Vector2 movementVector)
    {
        Vector3 move = transform.forward * movementVector.y + transform.right * movementVector.x;
        move = move * MovementSpeed * Time.deltaTime;
        characterController.Move(move);
    }

    // Calculates players look rotation
    public void Rotate(Vector2 rotationVector)
    {
        rotationY += rotationVector.x * RotationSpeed * Time.deltaTime * 4;
        transform.localRotation = Quaternion.Euler(0, rotationY, 0);
    }
}
