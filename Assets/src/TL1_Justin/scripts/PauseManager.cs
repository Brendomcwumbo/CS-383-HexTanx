using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseManager : MonoBehaviour
{
    public GameObject PauseMenuCanvas;
    public static bool isPaused;
    private GameObject pauseMenuInstance;
    public PlayerMovement playerMovement;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPaused = false;
        Time.timeScale = 1f;

        if (pauseMenuInstance == null)
        {
            pauseMenuInstance = Instantiate(PauseMenuCanvas);
            pauseMenuInstance.SetActive(false);
        }

        StartCoroutine(FindPlayerTank()); // Use coroutine to wait until PlayerTank is instantiated

        // Find buttons and assign onClick listeners
        Button resumeButton = pauseMenuInstance.transform.Find("PausePanel/ResumeButton").GetComponent<Button>();
        Button mainMenuButton = pauseMenuInstance.transform.Find("PausePanel/MainMenuButton").GetComponent<Button>();
        Button quitButton = pauseMenuInstance.transform.Find("PausePanel/QuitButton").GetComponent<Button>();

        resumeButton.onClick.AddListener(ResumeGame);
        mainMenuButton.onClick.AddListener(MainMenu);
        quitButton.onClick.AddListener(QuitGame);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key pressed");
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        Debug.Log("Resume button pressed");
        pauseMenuInstance.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;

        if (playerMovement != null)
        {
            Debug.Log("Player movement is enabled");
            playerMovement.enabled = true;
        }
    }

    public void PauseGame()
    {
        Debug.Log("Pause button pressed");
        pauseMenuInstance.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        if (playerMovement != null)
        {
            Debug.Log("Player movement is disabled");
            playerMovement.enabled = false;
        }
    }

    public void MainMenu()
    {
        Debug.Log("Main menu button pressed");
        // UnityEngine.SceneManagement.SceneManager.LoadScene("GameLevel1");
        Time.timeScale = 1.0f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Assets/src/TL1_Justin/scenes/MainMenu.unity");
    }

    public void QuitGame()
    {
        Debug.Log("Quit button pressed");
        Application.Quit();
    }

    // Coroutine to wait for PlayerTank to be instantiated
    IEnumerator FindPlayerTank()
    {
        // Wait until the PlayerTank is available
        while (GameObject.Find("PlayerTank") == null)
        {
            yield return null;
        }

        // Once the PlayerTank is found, get the PlayerMovement component
        GameObject playerTank = GameObject.Find("PlayerTank");
        if (playerTank != null)
        {
            playerMovement = playerTank.GetComponent<PlayerMovement>();
            Debug.Log("PlayerTank found and PlayerMovement initialized.");
        }
        else
        {
            Debug.LogError("PlayerTank not found after waiting.");
        }
    }
}
