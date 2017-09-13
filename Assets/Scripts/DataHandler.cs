using System.IO;
using UnityEngine;

public class DataHandler : MonoBehaviour
{
    public static DataHandler instance;
    public GameData data;
    private string gameDataFileName = "gameData.json";

    private void Awake()
    {
        instance = this;
        data = new GameData();
        LoadData();
    }

    public void SaveData()
    {
        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.persistentDataPath + "/gameData.json", jsonData);
    }

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
