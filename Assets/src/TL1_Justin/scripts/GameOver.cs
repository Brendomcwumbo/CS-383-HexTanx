using UnityEngine;

public class GameOver : MonoBehaviour
{
    // Game Over
    public CanvasGroup gameOverCanvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowGameOverScreen()
    {
        gameOverCanvas.alpha = 1f;
        gameOverCanvas.interactable = true;
        gameOverCanvas.blocksRaycasts = true;
    }

    public void RestartButton()
    {
        // Play button has been pressed, initialize first game level
        // FIXME: load Malik's first scene
        Debug.Log("Restart button pressed");
        // UnityEngine.SceneManagement.SceneManager.LoadScene("GameLevel1");
        // UnityEngine.SceneManagement.SceneManager.LoadScene("Assets/src/TL2_Malik/Assets/Scenes/SampleScene.unity");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Assets/src/TL1_Justin/scenes/MainMenu.unity");
    }

    public void QuitButton()
    {
        Debug.Log("Quit button pressed");
        Application.Quit();
    }
}
