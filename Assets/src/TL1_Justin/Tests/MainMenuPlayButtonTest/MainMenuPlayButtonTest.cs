using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class MainMenuPlayButtonTest
{
    private MainMenu mainMenu;

    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // // A Test behaves as an ordinary method
    // [Test]
    // public void MainMenuPlayButtonTestSimplePasses()
    // {
    //     // Use the Assert class to test conditions
    // }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator MainMenuPlayButtonTestWithEnumeratorPasses()
    {
        // Find the MainMenu script in the scene
        mainMenu = GameObject.FindObjectOfType<MainMenu>();
        Assert.NotNull(mainMenu, "MainMenu script not found in the scene");

        // Call the PlayButton method to trigger scene loading
        mainMenu.PlayButton();

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return new WaitForSeconds(1f);

        // Check if the correct game scene has been loaded
        Assert.AreEqual("Assets/src/TL2_Malik/Assets/Scenes/SampleScene.unity", SceneManager.GetActiveScene().path, "Scene did not change correctly");
    }
}
