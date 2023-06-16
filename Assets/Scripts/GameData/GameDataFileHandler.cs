using System;
using System.IO;
using UnityEngine;

public class GameDataFileHandler
{
    private string _dataPathDir;
    private string _fileName;

    public GameDataFileHandler(string dataPathDir, string fileName = "Game_data.game")
    {
        _dataPathDir = dataPathDir;
        _fileName = fileName;
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(_dataPathDir, _fileName);
        GameData loadedData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad;

                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream)) 
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.Log($"Error occurred then try to load data from file {fullPath}\n{e.Message}");
            }
        }

        return loadedData;
    }

    public void Save(GameData data) 
    {
        string fullPath = Path.Combine(_dataPathDir, _fileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string dataToStore = JsonUtility.ToJson(data, true);

            using(FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using(StreamWriter writer = new StreamWriter(stream)) 
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log($"Error occurred then try to save data to file {fullPath}\n{e.Message}");
        }
    }
}