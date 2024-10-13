using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public int countdownTime = 3;
    public Text countdownText;
    public GameObject[] players;

    private void Start()
    {
        DisablePlayerMovement();
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        countdownText.text = "Go!";
        yield return new WaitForSeconds(1f);
        countdownText.gameObject.SetActive(false);
        EnablePlayerMovement();
    }

    private void DisablePlayerMovement()
    {
        foreach (GameObject player in players)
        {
            player.GetComponent<AI>().enabled = false;
        }
    }

    private void EnablePlayerMovement()
    {
        foreach (GameObject player in players)
        {
            player.GetComponent<AI>().enabled = true;
        }
    }
}
