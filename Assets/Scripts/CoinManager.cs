using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int coinCount = 0;

    public void AddCoin(int amount)
    {
        coinCount += amount;
        Debug.Log("Coins: " + coinCount);
    }
}
