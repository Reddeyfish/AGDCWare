using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/// <summary>
/// A static class to handle scene transitions between AGDCWare game scenes.
/// </summary>
public static class AGDCWareFramework {

    private const string SCENENAMESTXT = "SceneNames.txt";

    private const float timePerScene = 30f;

    /// <summary>
    /// True if the framework has been initialized, False otherwise.
    /// </summary>
    private static bool initialized = false;

    /// <summary>
    /// Collection of the sceneNames of all mini-games.
    /// </summary>
    private static List<string> sceneNames;

    /// <summary>
    /// Time when the current scene is supposed to end.
    /// </summary>
    private static float endTime;

    /// <summary>
    /// Returns the amount of time remaining on the timer.
    /// </summary>
    /// <returns>Amount of time.</returns>
    public static float timeRemaining()
    {
        return endTime - Time.realtimeSinceStartup;
    }

    private static void init()
    {
        Assert.IsFalse(initialized);

        //load sceneNames
        sceneNames = new List<string>();
        StreamReader inputStream = new StreamReader(SCENENAMESTXT);
        while (!inputStream.EndOfStream)
        {
            sceneNames.Add(inputStream.ReadLine());
        }
        inputStream.Close();

        Assert.IsTrue(sceneNames.Count > 0);

        initialized = true;
    }

    /// <summary>
    /// Loads the next game scene.
    /// </summary>
    public static void LoadNextGame()
    {
        if (!initialized)
            init();
        SceneManager.LoadScene(sceneNames[Random.Range(0, sceneNames.Count)]);
        resetTimer();
    }

    private static void resetTimer()
    {
        resetTimer(timePerScene);
    }

    /// <summary>
    /// Reset the timer to have timeTilEnd seconds left.
    /// </summary>
    /// <param name="timeTilEnd">New time remaining on the timer.</param>
    /// <returns>End time of the timer in terms of real time since startup.</returns>
    public static float resetTimer(float timeTilEnd)
    {
        Assert.IsTrue(timeTilEnd > 0);
        float pauseStartTime = Time.realtimeSinceStartup;
        endTime = pauseStartTime + timeTilEnd;
        return endTime;
    }
}
