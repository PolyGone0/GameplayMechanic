using System.Collections;
using UnityEngine;

public class BeamAttack : MonoBehaviour
{
    private Transform character;

    public float speed = 20.0f;

    private bool notCasted = true;

    private Vector3 characterDirection;

    private float lifetime = 1.0f;

    [SerializeField] private GameObject Beam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Beam.SetActive(false);

        character = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += MoveDirection() * speed * Time.deltaTime;

        if (Input.GetMouseButtonUp(1) && notCasted)
        {
            Destroy(gameObject);
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            notCasted = false;
            MaterializeBeam();
        }
    }

    private Vector3 MoveDirection()
    {
        if (notCasted)
        {
            characterDirection = character.forward;
            return characterDirection;
        } else
        {
            return characterDirection;
        }

    }

    private void MaterializeBeam()
    {
        speed += 10;
        Beam.SetActive(true);
        StartCoroutine(BeamLifetime(lifetime));
    }

    private IEnumerator BeamLifetime(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
