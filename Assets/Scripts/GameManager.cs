using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Player References")]
    public Transform player;
    public PlayerHealth playerHealth;
    public Transform startPosition;

    public void ResetGame()
    {
        Debug.Log("Player died. Resetting to start...");

        // If your character uses a CharacterController, you must disable it before teleporting
        CharacterController cc = player.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;

        // Teleport player back to the start
        player.position = startPosition.position;

        // Re-enable CharacterController
        if (cc != null) cc.enabled = true;

        // Reset player's health
        playerHealth.ResetHealth();
    }

    public void WinGame()
    {
        Debug.Log("Corridor cleared! You win!");
    }
}