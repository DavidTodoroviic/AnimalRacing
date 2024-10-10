using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public bool inventoryfull = false;
    public GameObject bananaPrefab;
    public GameObject player;
    private GameObject placedBanana;
    private float thunderStrikeDuration = 5f;
    private float thunderStrikeTimer = 0f;
    private float stunDuration = 3f;
    private float magnetRange = 10f;
    private float magnetDuration = 8f;
    private float magnetTimer = 0f;

    public enum FSMState
    {
        None,
        MissileLauncher,
        Shield,
        SpeedReducer,
        Banana,
        Magnet,
        Thunderstrikes,
        Unknown,
        Teleportation,
    }

    public FSMState curState;

    void Start()
    {
        curState = FSMState.None;
    }

    void Update()
    {
        switch (curState)
        {
            case FSMState.Unknown: UpdateUnknownPowerup(); break;
            case FSMState.Banana: PlaceObjectBehindPlayer(player, 5f); break;
            case FSMState.Magnet: MagnetPowerup(); break;
            case FSMState.Thunderstrikes: ThunderstrikesPowerup(); break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Banana") && inventoryfull == false)
        {
            inventoryfull = true;
            curState = FSMState.Banana;
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Unknown") && inventoryfull == false)
        {
            inventoryfull = true;
            curState = FSMState.Unknown;
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Magnet") && inventoryfull == false)
        {
            inventoryfull = true;
            curState = FSMState.Magnet;
            magnetTimer = magnetDuration; // Start the magnet timer
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Thunderstrikes") && inventoryfull == false)
        {
            inventoryfull = true;
            curState = FSMState.Thunderstrikes;
            thunderStrikeTimer = thunderStrikeDuration; // Start the thunderstrike timer
            Destroy(other.gameObject);
        }
    }

    protected void UpdateUnknownPowerup()
    {
        FSMState[] states = {
            FSMState.MissileLauncher,
            FSMState.Shield,
            FSMState.SpeedReducer,
            FSMState.Banana,
            FSMState.Magnet,
            FSMState.Thunderstrikes,
            FSMState.Teleportation
        };

        curState = states[Random.Range(0, states.Length)];
    }

protected void PlaceObjectBehindPlayer(GameObject player, float distance)
{
    if (placedBanana == null)
    {
        float heightOffset = 1.0f;
        placedBanana = Instantiate(bananaPrefab);
        Vector3 playerPosition = player.transform.position;
        Vector3 bananaPosition = playerPosition - player.transform.forward * distance;
        bananaPosition.y = playerPosition.y + heightOffset;
        placedBanana.transform.position = bananaPosition;
        curState = FSMState.None;
        inventoryfull = false;
    }
}

        protected void ThunderstrikesPowerup()
    {
        GameObject[] opponents = GameObject.FindGameObjectsWithTag("Opponent");

        foreach (GameObject opponent in opponents)
        {
            // Temporarily change the color of the opponent to indicate they are "stunned"
            StartCoroutine(StunOpponent(opponent));
        }

        thunderStrikeTimer -= Time.deltaTime;
        if (thunderStrikeTimer <= 0f)
        {
            curState = FSMState.None; // Return to normal state after duration ends
            inventoryfull = false; // Allow picking up more power-ups
        }
    }

    private IEnumerator StunOpponent(GameObject opponent)
    {
        Renderer opponentRenderer = opponent.GetComponent<Renderer>();
        if (opponentRenderer != null)
        {
            // Change color to red to indicate "stunned" state
            Color originalColor = opponentRenderer.material.color;
            opponentRenderer.material.color = Color.red;
            Debug.Log(opponent.name + " is stunned!");

            yield return new WaitForSeconds(stunDuration);

            // Revert back to original color
            opponentRenderer.material.color = originalColor;
            Debug.Log(opponent.name + " is no longer stunned!");
        }
    }
     protected void MagnetPowerup()
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");

        foreach (GameObject coin in coins)
        {
            float distance = Vector3.Distance(transform.position, coin.transform.position);

            if (distance < magnetRange)
            {
                Vector3 direction = (transform.position - coin.transform.position).normalized;
                coin.transform.position += direction * Time.deltaTime * 5f; // Adjust speed as needed
            }
        }
        magnetTimer -= Time.deltaTime;
        if (magnetTimer <= 0f)
        {
            curState = FSMState.None; // Return to normal state after duration ends
            inventoryfull = false; // Allow picking up more power-ups
        }
    }
}
