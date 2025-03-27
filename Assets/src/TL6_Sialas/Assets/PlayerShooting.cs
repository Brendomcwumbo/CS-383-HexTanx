using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private AudioClip shootSoundOverride; // Optional override for shoot sound
    private Camera mainCamera;
    private PlayerTank playerTank;

    void Start()
    {
        mainCamera = Camera.main;
        playerTank = GetComponent<PlayerTank>();
    }

    void Update()
    {
        // Right-click to shoot
        if (Input.GetMouseButtonDown(1))
        {
            if (playerTank.UseActionPoint() && playerTank.GetAmmoCount() > 0)  // Check action point and ammo
            {
                Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                mouseWorldPos.z = -1;
                Shoot();
                playerTank.SetAmmoCount(playerTank.GetAmmoCount() - 1);  // Deduct ammo
                Debug.Log("Shot fired toward " + mouseWorldPos);
                FindObjectOfType<BattleSystem>().PlayerActionTaken();
            }
            else if (playerTank.GetAmmoCount() <= 0)
            {
                Debug.Log("No ammo remaining!");
            }
        }
    }

    void Shoot()
    {
        // Play shoot sound via SoundManager
        if (shootSoundOverride != null)
        {
            SoundManager.Instance.Play(shootSoundOverride); // Use override if assigned
        }
        else
        {
            SoundManager.Instance.ShootSound(); // Use default from SoundManager
        }
        Debug.Log("Shoot sound triggered via SoundManager");

        // Get mouse position and convert to world coordinates
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        // Instantiate projectile at player position
        GameObject bullet = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // Move projectile toward target
        Projectile projectileScript = bullet.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.SetTarget(mousePosition);
        }
    }
}