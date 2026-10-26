using System.Collections;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;

public class BeamAttack : MonoBehaviour
{
    private Transform character;
    private Vector3 characterDirection;

    public float speed = 20.0f;
    private float lifetime = 1.0f;
    public bool notCasted;

    [SerializeField] private GameObject Beam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Beam.SetActive(false);
        notCasted = true;

        character = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Moves Beam in direction player is facing
        transform.position += MoveDirection() * speed * Time.deltaTime;
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
    public void MaterializeBeam()
    {
        notCasted = false;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Target"))
        {
            Destroy(other.gameObject);
        }
    }
}
