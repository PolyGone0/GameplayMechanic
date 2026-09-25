using UnityEngine;

public class Attack : MonoBehaviour
{
    public CharacterController characterController;

    private GameObject player;

    private Transform PlayerLocation;

    private Vector3 playerPosition;

    [SerializeField] private GameObject BeamAttack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            PlayerLocation = player.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Updaates PlayerLocation
        if (PlayerLocation != null)
        {
            playerPosition = new Vector3(PlayerLocation.position.x, PlayerLocation.position.y - 1.5f, PlayerLocation.position.z);
        }
    }

    // Instantiates Beam object
    public void SummonAttack(float pInput)
    {
        Quaternion spawnRotation = Quaternion.identity;
        GameObject attackObj = Instantiate(BeamAttack, playerPosition, spawnRotation);
    }
}
