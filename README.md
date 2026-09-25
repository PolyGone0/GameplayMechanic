# GameplayMechanic

# My mechanic for project 1 was a recreation of an attack called 'Divine Light' used by a character called 'The Judgment' from the game 'Dead By Daylight'. 
# The attack involves holding an input (RMB), which spawns a circular indicator that travels along the ground in the direction the player is looking. The player
# then can use another input (LMB) to summon a pillar of damaging light on the indicator, which also locks the direction its moving, dealing damage as it travels.

# For utilizing an Interface, there wasn't anywhere to implement one, since each script was completely unique in terms of methods, so using an Interface wouldn't have helped any.

# For deltaTime, I used it a lot when moving the attack indicator and making the player movement and look inputs.
# BeamAttack moving using deltaTime:
transform.position += MoveDirection() * speed * Time.deltaTime;

# Player Movement and Look Inputs using deltaTime:
// Calculates players movement then repeats
public void Move(Vector2 movementVector)
{
    Vector3 move = transform.forward * movementVector.y + transform.right * movementVector.x;
    move = move * MovementSpeed * Time.deltaTime;
    characterController.Move(move);
}
// Calculates players look rotation then repeats
public void Rotate(Vector2 rotationVector)
{
    rotationY += rotationVector.x * RotationSpeed * Time.deltaTime * 4;
    transform.localRotation = Quaternion.Euler(0, rotationY, 0);
}

# Instead of hard coding inputs, I used the built in Input system for Unity:
moveAction = InputSystem.actions.FindAction("Move");
lookAction = InputSystem.actions.FindAction("Look");
attackAction = InputSystem.actions.FindAction("Attack");

# For my scope, I fortunately didn't have to shrink it any. My project is whitebox and very rudimentary, but it still shows the basic mechanic.

# Video is in a folder
