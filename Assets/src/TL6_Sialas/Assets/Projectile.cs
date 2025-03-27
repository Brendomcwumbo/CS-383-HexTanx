using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 targetPosition;
    public float speed = 5f;
    public int damage = 10;
    [SerializeField] private AudioClip explosionSoundOverride; // Optional override

    public void SetTarget(Vector3 target)
    {
        targetPosition = target;
    }

    void Update()
    {
        // Move shell towards target
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Destroy projectile when it reaches target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        TankType enemyTank = other.GetComponent<TankType>();

        if (enemyTank != null)
        {
            enemyTank.health -= damage;
            Debug.Log("Enemy tank hit! Remaining HP: " + enemyTank.health);

            if (enemyTank.health <= 0)
            {
                // Play explosion sound via SoundManager
                if (explosionSoundOverride != null)
                {
                    SoundManager.Instance.Play(explosionSoundOverride); // Use override if assigned
                }
                else
                {
                    SoundManager.Instance.ExplodeSound(); // Use default from SoundManager
                }
                Debug.Log("Explosion sound triggered via SoundManager at position: " + transform.position);

                Destroy(enemyTank.gameObject);
                Destroy(gameObject);  // No delay needed
                Debug.Log("Enemy tank destroyed!");
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}