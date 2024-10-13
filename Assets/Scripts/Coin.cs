using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject[] players;
    public CoinManager coinManager;

    void OnTriggerEnter(Collider other)
    {
        foreach (GameObject player in players)
        {
            if (other.gameObject == player)
            {
                coinManager.AddCoin(1);
                Destroy(gameObject);
                return;
            }
        }
    }
}
