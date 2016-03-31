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

    /// <summary>
    /// True if the framework has been initialized, False otherwise.
    /// </summary>
    private static bool initialized = false;

    /// <summary>
    /// Collection of the sceneNames of all mini-games.
    /// </summary>
    private static List<string> sceneNames;

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
    }
}
