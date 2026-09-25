using System.Collections;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;

public class BeamAttack : MonoBehaviour
{
    private Transform character;

    public float speed = 20.0f;

    private bool notCasted = true;

    private Vector3 characterDirection;

    private float lifetime = 1.0f;

    [SerializeField] private GameObject Beam;

    private InputAction attackAction, castAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Beam.SetActive(false);

        character = GameObject.FindWithTag("Player").transform;

        attackAction = InputSystem.actions.FindAction("Attack");
        castAction = InputSystem.actions.FindAction("Cast");
    }

    // Update is called once per frame
    void Update()
    {
        // Moves Beam in direction player is facing
        transform.position += MoveDirection() * speed * Time.deltaTime;

        // Checks if RMB is held, if not, destroys gameObject
        if (notCasted)
        {
            heldInput(attackAction.ReadValue<float>());
        }

        // Looks for LMB input, then casts and updates bool
        if (castAction.ReadValue<float>() == 1)
        {
            if (notCasted)
            {
                MaterializeBeam();
            }
            notCasted = false;
        }
    }

    private void heldInput(float pInput)
    {
        if (pInput == 0)
        {
            Destroy(gameObject);
        }
    }

    // Updates characterDirection until Beam is cast
    private Vector3 MoveDirection()
    {
        if (notCasted)
        {
            characterDirection = character.forward;
            return characterDirection;
        }
        else
        {
            return characterDirection;
        }

    }

    // Casts Beam, increasing speed and starts lifetime countdown
    private void MaterializeBeam()
    {
        speed += 10;
        Beam.SetActive(true);
        StartCoroutine(BeamLifetime(lifetime));
    }

    // Starts timer for lifetime duration then destroys Beam
    private IEnumerator BeamLifetime(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
