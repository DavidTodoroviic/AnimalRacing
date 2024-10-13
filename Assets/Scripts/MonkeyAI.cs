using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyAI : MonoBehaviour
{
    public Transform gorillaTransform;
    public GameObject barrelPrefab; 
    public Transform throwPoint; 
    public int maxBananas = 3;
    public float bananaMeter = 0;
    
    public float throwForce = 20f; 
    public float throwDistance = 10f; 
    
    public float knockbackRadius = 5f; 

    public GameObject[] players; 

    private bool canThrowBarrel = false;

    void Update()
    {
        if (bananaMeter >= maxBananas)
        {
            canThrowBarrel = true; 
        }
        else
        {
            canThrowBarrel = false; 
        }

        if (canThrowBarrel && Input.GetKeyDown(KeyCode.Return))
        {
            ThrowBarrel(); 
        }

        if (bananaMeter >= maxBananas)
        {
            KnockbackPlayers(); 
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Banana"))
        {
            bananaMeter += 1;
            Destroy(other.gameObject);
        }
    }

    void ThrowBarrel()
    {
        GameObject barrel = Instantiate(barrelPrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody barrelRb = barrel.GetComponent<Rigidbody>();
        if (barrelRb != null)
        {
            barrelRb.AddForce(gorillaTransform.forward * throwForce, ForceMode.Impulse);
            barrelRb.velocity = gorillaTransform.forward * throwDistance; 
        }
    }

    void KnockbackPlayers()
    {
        Collider[] hitPlayers = Physics.OverlapSphere(gorillaTransform.position, knockbackRadius);

        foreach (Collider player in hitPlayers)
        {
            if (IsPlayerInArray(player.gameObject))
            {
                Rigidbody playerRb = player.GetComponent<Rigidbody>();
                if (playerRb != null)
                {
                    Vector3 knockbackDirection = (player.transform.position - gorillaTransform.position).normalized;
                    playerRb.AddForce(knockbackDirection * throwForce, ForceMode.Impulse);
                    bananaMeter = 0; 
                }
            }
        }
    }

    bool IsPlayerInArray(GameObject other)
    {
        foreach (GameObject player in players)
        {
            if (player == other)
            {
                return true;
            }
        }
        return false;
    }
}
