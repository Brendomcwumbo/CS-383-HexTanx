using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Camera mainCamera;
    private GameObject playerTank;
    private PlayerTank playerTankComponent;
    private PlaceTile placeTile;

    private Vector3 targetPosition; // The tile position to move towards
    private bool isMoving = false;  // Flag to track if the tank is currently moving
    [SerializeField] private float moveSpeed = 5f; // Speed of the tank movement (adjust in Inspector)

    void Start()
    {
        mainCamera = Camera.main;
        playerTank = gameObject;
        playerTankComponent = playerTank.GetComponent<PlayerTank>();
        placeTile = FindObjectOfType<PlaceTile>();
        targetPosition = transform.position; // Initialize target to current position
    }

    void Update()
    {
        // Handle movement towards the target position
        if (isMoving)
        {
            // Move towards the target position at moveSpeed
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Check if the tank has reached the target
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                transform.position = targetPosition; // Snap to target to avoid floating-point issues
                isMoving = false; // Stop moving
                playerTankComponent.SetTankLocation(targetPosition); // Update PlayerTank's location
                SoundManager.Instance.StopMovementSound(); // Stop the movement sound
                Debug.Log("Tank reached target: " + transform.position);
            }
        }

        // Left-click to initiate movement
        if (Input.GetMouseButtonDown(0) && !isMoving) // Only allow new movement if not currently moving
        {
            if (playerTankComponent.UseActionPoint()) // Check and deduct action point
            {
                Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                mouseWorldPos.z = -1;
                targetPosition = FindNearestTile(mouseWorldPos);
                if (targetPosition != transform.position) // Only move if the target is different
                {
                    isMoving = true; // Start moving
                    SoundManager.Instance.PlayerMoveSound(); // Play movement sound
                    Debug.Log("Tank moving to " + targetPosition);
                    Debug.Log("Mouse position " + mouseWorldPos);
                    FindObjectOfType<BattleSystem>().PlayerActionTaken();

                }
            }
        }
    }

    public Vector3 FindNearestTile(Vector3 position)
    {
        if (placeTile == null || placeTile.Grid == null)
        {
            Debug.LogError("PlaceTile or Grid not available");
            return transform.position;
        }

        Vector3 nearestPos = placeTile.Grid[0, 0];
        float minDistance = Vector3.Distance(position, nearestPos);

        for (int x = 0; x < placeTile.Grid.GetLength(0); x++)
        {
            for (int y = 0; y < placeTile.Grid.GetLength(1); y++)
            {
                float distance = Vector3.Distance(position, placeTile.Grid[x, y]);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestPos = placeTile.Grid[x, y];
                }
            }
        }

        return new Vector3(nearestPos.x, nearestPos.y, -1);
    }
}