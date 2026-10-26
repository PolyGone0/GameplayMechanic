using Unity.VisualScripting;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            GetComponentInParent<Attack>().SetHitCooldown();
            Destroy(other.gameObject);
            gameObject.SetActive(false);
        }
        
    }
}
