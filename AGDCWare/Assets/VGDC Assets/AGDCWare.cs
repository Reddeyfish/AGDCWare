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
    /// The maximum number of lives. currentLives starts at this number.
    /// </summary>
    private static int maxLives;

    /// <summary>
    /// The current number of lives. If this reaches 0, the game ends.
    /// </summary>
    private static int currentLives;

    /// <summary>
    /// True if max lives and current lives have been set, False otherwise.
    /// </summary>
    private static bool livesInit = false;

    /// <summary>
    /// Number of games won by the player.
    /// </summary>
    private static int gamesWon = 0;

    /// <summary>
    /// Returns the amount of time remaining on the timer.
    /// </summary>
    /// <returns>Amount of time.</returns>
    public static float timeRemaining()
    {
        return endTime - Time.realtimeSinceStartup;
    }

    /// <summary>
    /// Set the maximum number of lives. 
    /// </summary>
    /// <param name="num">maxLives is set to num. Default value is 4.</param>
    public static void setMaxLives(int num = 4)
    {
        maxLives = num;
        currentLives = maxLives;

        livesInit = true;
    }

    public static int getCurrentLives()
    {
        return currentLives;
    }

    /// <summary>
    /// Modify the current number of lives by an offset (e.g. 1 for adding a life, -1 for losing a life).
    /// </summary>
    /// <param name="offset"> Offset is added to current lives (Offset can be negative).</param>
    /// <returns>Returns True when the player is dead (i.e. at 0 lives), False otherwise.</returns>
    private static bool offsetLives(int offset)
    {
        currentLives += offset;

        if(currentLives <= 0)
        {
            return true;
        }

        else
        {
            return false;
        }
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

        setMaxLives();

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


    public static void LoadNextGame(bool victory)
    {
        if(victory)
        {
            ++gamesWon;
        }

        else
        {
            offsetLives(-1);
        }
        
        LoadNextGame();
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
