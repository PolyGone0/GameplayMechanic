using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public PlayerController CharacterController;

    public Attack Attack;

    private InputAction moveAction, lookAction, attackAction;

    private bool sameInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");
        attackAction = InputSystem.actions.FindAction("Attack");

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        sameInput = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movementVector = moveAction.ReadValue<Vector2>();
        CharacterController.Move(movementVector);

        Vector2 lookVector = lookAction.ReadValue<Vector2>();
        CharacterController.Rotate(lookVector);

        // Starts Attack -- Replace with Action Call
        float attackInput = attackAction.ReadValue<float>();
        CallAttack(attackInput);
    }

    private void CallAttack(float pInput)
    {
        if (sameInput == false)
        {
            Attack.SummonAttack(pInput);
            sameInput = true;
        }
        if (pInput == 0)
        {
            sameInput = false;
        }

    }
}
