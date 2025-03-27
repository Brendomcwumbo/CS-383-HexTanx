using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject healthBarPrefab;
    private Image healthBar; // The part of the health bar we want to adjust
    private PlayerTank player;

    private PlayerTankSpawner playerTankSpawner;

    void Start()
    {
        playerTankSpawner = FindObjectOfType<PlayerTankSpawner>();

        if (playerTankSpawner == null)
        {
            Debug.Log("PlayerTankSpawner not found in the scene.");
            return;
        }

        // Instantiate the health bar UI from the prefab
        GameObject healthBarInstance = Instantiate(healthBarPrefab, transform);
        healthBar = healthBarInstance.transform.Find("TotalHealth").GetComponent<Image>();

        if (healthBar == null)
        {
            Debug.Log("Health bar Image component not found.");
            return;
        }
    }

    void Update()
    {
        if (player == null)
        {
            GameObject playerTank = GameObject.Find("PlayerTank");

            if (playerTank != null)
            {
                player = playerTank.GetComponent<PlayerTank>();

                if (player == null)
                {
                    Debug.Log("The found player tank does not have a PlayerTank component.");
                    return;
                }
            }
        }

        if (player != null)
        {
            // Update the health bar based on the player's health
            UpdateHealthBar();
        }
    }

    private void UpdateHealthBar()
    {
        // Get the player's health as a percentage
        float healthPercent = player.GetHealth() / 100f;

        // Update the health bar's fill amount based on the player's health
        healthBar.fillAmount = healthPercent;
    }
}
