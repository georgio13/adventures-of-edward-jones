/**----------------------------------------------------------------
 *  Author:         Yorgos Chatziparaskevas
 *  Written:        14/9/2017
 *  Last updated:   14/9/2017
 *
 *  File:           DataHandler.cs
 *
 *  This class impements the functions with which we handle our
 *  saved data.
 *
 *----------------------------------------------------------------*/

using System.IO;
using UnityEngine;

public class DataHandler : MonoBehaviour
{
    public static DataHandler instance;     // We create an instance of Data Handler.
    public GameData data;                   // This is the game data that we will save.
    private string gameDataFileName;        // The name of file where we will save our data.

    /// <summary>
    /// On the initialization of data handler we create the Game Data object,
    /// set the name of file and load our data.
    /// </summary>
    private void Awake()
    {
        instance = this;
        data = new GameData();
        gameDataFileName = "gameData.json";
        LoadData();
    }

    /// <summary>
    /// This function saves the Game Data to the file that we want.
    /// </summary>
    public void SaveData()
    {
        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.persistentDataPath + "/gameData.json", jsonData);
    }

    /// <summary>
    /// This function loads the Game Data from the file that we saved them.
    /// </summary>
    public void LoadData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, gameDataFileName);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<GameData>(dataAsJson);
        }
        else
        {
            Debug.Log("Error");
        }
    }
}
