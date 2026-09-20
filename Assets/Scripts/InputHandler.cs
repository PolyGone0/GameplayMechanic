using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public PlayerController CharacterController;

    public Attack Attack;

    private InputAction moveAction, lookAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movementVector = moveAction.ReadValue<Vector2>();
        CharacterController.Move(movementVector);

        Vector2 lookVector = lookAction.ReadValue<Vector2>();
        CharacterController.Rotate(lookVector);

        if (Input.GetMouseButtonDown(1))
        {
            Attack.SummonAttack();
        }
    }
}
