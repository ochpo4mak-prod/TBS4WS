using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class SavingScript : MonoBehaviour
{
    public static List<Character> characters = new List<Character>();
    private static int [,] _mapArray = MapGenerator.map;
    
    public static void TriggerSave(Vector2Int castleCoords)
    {
        int[] tempArray = new int[(_mapArray.GetUpperBound(0) + 1) * (_mapArray.GetUpperBound(1) + 1)];

        int i = 0;
        for (int x = 0; x < _mapArray.GetUpperBound(0) + 1; x++)
        {
            for (int y = 0; y < _mapArray.GetUpperBound(1) + 1; y++)
            {
                tempArray[i] = _mapArray[x, y];
                ++i;
            }
        }

        SaveData saveData = new SaveData
        {
            mapArray = tempArray,
            castleCoords = castleCoords,
            characters = characters,
        };

        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(Application.dataPath + "/Saves/save.json", json);
    }

    public static void TriggerLoad()
    {
        int[,] tempArray = new int[(_mapArray.GetUpperBound(0) + 1), (_mapArray.GetUpperBound(1) + 1)];;

        string saveString = File.ReadAllText(Application.dataPath + "/Saves/save.json");
        SaveData saveData = JsonUtility.FromJson<SaveData>(saveString);

        for (int i = 0; i < saveData.mapArray.Length; ++i)
        {
            tempArray[(i / (_mapArray.GetUpperBound(0) + 1)), (i % (_mapArray.GetUpperBound(1) + 1))] = saveData.mapArray[i];
        }

        MapGenerator.map = tempArray;
        Spawner.castleCoords = saveData.castleCoords;
        Spawner.characters = saveData.characters;
    }
}

[Serializable] class SaveData
{
    public Vector2Int castleCoords;
    public List<Character> characters;
    public int[] mapArray;
}