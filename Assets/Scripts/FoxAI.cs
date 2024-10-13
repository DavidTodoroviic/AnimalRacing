using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxAI : MonoBehaviour
{
    // Start is called before the first frame update
    private BearRage bearRage;
    public float detectionRadius = 5f;        
    public float runSpeedBoost = 10f;          
    public float runDuration = 4f;              
    public float normalSpeedBoostDuration = 3f; 
    public float speedBoost = 7f;               
    public float agilityMultiplier = 1.2f;      
    public float boostCooldown = 10f;           

    private bool isBoosting = false;           
    private bool isOnCooldown = false;          
    private bool isRunningFromBear = false;     
    private float originalSpeed;               
    private float originalAgility;              

    public List<GameObject> competitors;        
    private AI foxMovement;
    void Start()
    {
        foxMovement = GetComponent<AI>();
        originalSpeed = foxMovement.speed;
        originalAgility = foxMovement.turnSpeed;
        bearRage = GetComponent<BearRage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bearRage.isRaging == true)
        {
            StartCoroutine(RunFromBear());
        }
        else if (!isBoosting && !isOnCooldown && !isRunningFromBear)
        {
            // Normal boost logic, if no bear rage is detected
            foreach (GameObject competitor in competitors)
            {
                if (competitor != null && Vector3.Distance(transform.position, competitor.transform.position) <= detectionRadius)
                {
                    StartCoroutine(TriggerBoostMode());
                    break;  // Only need one competitor within range to trigger the boost
                }
            }
        }
    }
    IEnumerator RunFromBear()
    {
        isRunningFromBear = true;

        // Increase speed significantly to simulate running away from the bear
        foxMovement.speed += runSpeedBoost;

        // Run for a limited time
        yield return new WaitForSeconds(runDuration);

        EndRunFromBear();
    }
    void EndRunFromBear()
    {
        isRunningFromBear = false;

        foxMovement.speed = originalSpeed;
    }

    IEnumerator TriggerBoostMode()
    {
        isBoosting = true;

        // Increase speed and agility for normal boost
        foxMovement.speed += speedBoost;
        foxMovement.turnSpeed *= agilityMultiplier;

        // Speed and agility boost lasts for a limited time
        yield return new WaitForSeconds(normalSpeedBoostDuration);

        EndBoostMode();
    }

    void EndBoostMode()
    {
        isBoosting = false;

        // Reset speed and agility to original values
        foxMovement.speed = originalSpeed;
        foxMovement.turnSpeed = originalAgility;

        // Start cooldown period
        StartCoroutine(BoostCooldown());
    }

    IEnumerator BoostCooldown()
    {
        isOnCooldown = true;

        // Wait for the cooldown duration
        yield return new WaitForSeconds(boostCooldown);

        isOnCooldown = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
