using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;

    public float MovementSpeed = 10f, RotationSpeed = 5f;

    public float rotationY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Vector2 movementVector)
    {
        Vector3 move = transform.forward * movementVector.y + transform.right * movementVector.x;
        move = move * MovementSpeed * Time.deltaTime;
        characterController.Move(move);
    }

    public void Rotate(Vector2 rotationVector)
    {
        rotationY += rotationVector.x * RotationSpeed * Time.deltaTime * 4;
        transform.localRotation = Quaternion.Euler(0, rotationY, 0);
    }
}
