using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEditor.Timeline.Actions;
using UnityEngine.InputSystem;
using System.Collections;
using System;

public class Attack : MonoBehaviour
{
    private GameObject player;
    private PlayerController PC;
    private PlayerInput PI;
    private Transform PlayerLocation;
    private Vector3 playerPosition;

    private float baseSpeed, lungeSpeed, cooldownSpeed; // baseSpeed = 10 in PlayerController
    private float cooldownTime;
    private float lungeDuration = 0.5f;
    private bool attackCooldown = false;
    private Func<bool> inLunge; 

    private GameObject summonedBeam;
    private BeamAttack beamScript;

    [SerializeField] private GameObject basicAttack;
    [SerializeField] private GameObject BeamAttack;
    private BasicAttack basicAttackScript;

    private void Awake()
    {
        player = gameObject;
        PC = GetComponent<PlayerController>();
        PI = GetComponent<PlayerInput>();
        basicAttackScript = basicAttack.GetComponent<BasicAttack>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (player != null)
        {
            PlayerLocation = player.transform;
            baseSpeed = PC.MovementSpeed;

            // Player speeds set
            lungeSpeed = baseSpeed * 1.5f;
            cooldownSpeed = baseSpeed * 0.25f;
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

        inLunge = () => !(PI.actions["PrimaryAttack"].IsInProgress());
    }

    // ------------------------- PRIMARY ATTACK INPUTS / METHODS ---------------------------------
    public void BasicAttackAction(InputAction.CallbackContext context)
    {
        if (context.started && !(attackCooldown))
        {
            cooldownTime = 1f;
            PC.MovementSpeed = lungeSpeed;
            
            StartCoroutine(Lunge(inLunge, lungeDuration));
        }
    }

    private IEnumerator Lunge(System.Func<bool> condition, float timeout)
    {
        float timer = 0f;
        while (!condition() && timer < timeout)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(BasicAttackSteps());
    }

    // Called in basicAttackScript on attack
    public void SetHitCooldown()
    {
        cooldownTime = 2f;
        Debug.Log("TargetHit");
    }

    private IEnumerator BasicAttackSteps()
    {
        // Swing
        attackCooldown = true;

        basicAttack.SetActive(true);    
        yield return new WaitForSeconds(0.1f);
        basicAttack.SetActive(false);

        // Cooldown
        PC.MovementSpeed = cooldownSpeed;
        yield return new WaitForSeconds(cooldownTime);
        PC.MovementSpeed = baseSpeed;
        attackCooldown = false; 
    }

    // ------------------------- SPECIAL ATTACK INPUTS / METHODS ---------------------------------
    public void SummonBeamAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Quaternion spawnRotation = Quaternion.identity;
            summonedBeam = Instantiate(BeamAttack, playerPosition, spawnRotation);
            beamScript = summonedBeam.GetComponent<BeamAttack>();
        }

        if (context.canceled && beamScript.notCasted)
            Destroy(summonedBeam);
    }

    public void CastBeamAction(InputAction.CallbackContext context)
    {
        if (summonedBeam != null)
        {
            if (context.started && beamScript.notCasted)
            {
                beamScript.MaterializeBeam();
            }
        }
    }
}
